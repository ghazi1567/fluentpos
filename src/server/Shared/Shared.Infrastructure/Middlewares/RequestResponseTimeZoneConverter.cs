using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace FluentPOS.Shared.Infrastructure.Middlewares
{
    // followed this article > https://www.tallan.com/blog/2022/03/28/implementing-time-zone-support-in-angular-and-asp-net-core-applications/
    public class RequestResponseTimeZoneConverter
    {
        private readonly TimeZoneSettings _appConfig;
        private readonly RequestDelegate _next;

        public RequestResponseTimeZoneConverter(
            IOptions<TimeZoneSettings> appConfig,
            RequestDelegate next)
        {
            this._appConfig = appConfig.Value;
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Request content and parameters won't be modified if disableTimeZoneConversion=true is specified
            // as a query string parameter in the URI
            var disableConversion =
              context.Request.Query.ContainsKey("disableTimeZoneConversion") &&
              context.Request.Query["disableTimeZoneConversion"] == "true";

            // Get the local time zone for UTC conversion
            var localTimeZone = this.GetLocalTimeZone(context);

            // If conversion isn't disabled, and the local time zone can be detected (and isn't UTC),
            // modify the request content (convert local to UTC)
            if (!disableConversion && localTimeZone != null && localTimeZone.Id != "UTC")
            {
                // Modify the date/time request parameters in the URI
                this.ModifyRequestParameters(context, localTimeZone);

                // Don't modify the request content unless the Content-Type is application/json
                var isJsonContent =
                  context.Request.Headers.ContainsKey("Content-Type") &&
                  context.Request.Headers["Content-Type"] == "application/json";

                if (isJsonContent)
                {
                    // Modify the date/time properties in the request content
                    await this.ModifyRequestContent(context, localTimeZone);
                }
            }

            // Prepare for modifying the response body
            var responseStream = context.Response.Body;
            // var modifiedResponseStream = new MemoryStream();
            // context.Response.Body = modifiedResponseStream;

            try
            {
                await this._next(context).ConfigureAwait(false);
            }
            finally
            {
                context.Response.Body = responseStream;
            }

            //// Modify the response content (convert UTC to local)
            // modifiedResponseStream = this.ModifyResponseContent(context, disableConversion, localTimeZone, modifiedResponseStream);
            // await modifiedResponseStream.CopyToAsync(responseStream).ConfigureAwait(false);
        }

        private TimeZoneInfo GetLocalTimeZone(HttpContext context)
        {
            // If the app config doesn't permit multiple time zones, then treat every user as if
            // they were in the same "site" time zone
            if (!this._appConfig.SupportMultipleTimeZones)
            {
                return TimeZoneInfo.FindSystemTimeZoneById(this._appConfig.SiteTimeZoneId);
            }

            // If the request headers include the user's local time zone (IANA name, injected by client-side HTTP interceptor),
            // use that time zone
            //if (context.Request.Headers.TryGetValue("MyApp-Local-Time-Zone-Iana", out StringValues localTimeZoneIana))
            //{
            //    return TZConvert.GetTimeZoneInfo(localTimeZoneIana);
            //}

            // The app config permits multiple time zones, but the user request doesn't specify the time zone
            return null;
        }

        private void ModifyRequestParameters(HttpContext context, TimeZoneInfo localTimeZone)
        {
            // Get all the query parameters from the URI
            var queryParameters = context.Request.Query
              .SelectMany(kvp => kvp.Value, (col, value) =>
                  new KeyValuePair<string, string>(col.Key, value))
              .ToList();

            // Nothing to do if there aren't any
            if (queryParameters.Count == 0)
            {
                return;
            }

            // Build a new list of query parameters, converting date/time values
            var modifiedQueryParameters = new List<KeyValuePair<string, string>>();

            var modified = false;
            foreach (var item in queryParameters)
            {
                var value = item.Value;
                if (value.FromDateTimeIsoString(out DateTime local))
                {
                    var utc = TimeZoneInfo.ConvertTimeToUtc(local, localTimeZone);
                    value = utc.ToDateTimeIsoString();
                    var modifiedQueryParameter = new KeyValuePair<string, string>(item.Key, value);
                    modifiedQueryParameters.Add(modifiedQueryParameter);
                    modified = true;
                }
                else
                {
                    var unmodifiedQueryParameter = new KeyValuePair<string, string>(item.Key, value);
                    modifiedQueryParameters.Add(unmodifiedQueryParameter);
                }
            }

            if (modified)
            {
                var qb = new QueryBuilder(modifiedQueryParameters);
                context.Request.QueryString = qb.ToQueryString();
            }
        }


        private async Task<TimeZoneInfo> ModifyRequestContent(HttpContext context, TimeZoneInfo localTimeZone)
        {
            // Read the request content from the request body stream; if it's a JSON object, we'll process it
            var requestStream = context.Request.Body;
            var originalRequestContent = await new StreamReader(requestStream).ReadToEndAsync();

            // Try to get the JSON object from the request content
            var jobj = originalRequestContent.TryDeserializeToJToken();

            // If the request content is a JSON object, convert all of it's date/time properties from local time to UTC
            var modified = false;
            if (jobj != null)
            {
                modified = jobj.ConvertLocalToUtc(localTimeZone);
            }

            if (modified)
            {
                // Replace the stream with the updated request content
                var json = JsonConvert.SerializeObject(jobj);
                var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
                requestStream = await requestContent.ReadAsStreamAsync();
            }
            else
            {
                // Replace the stream with the original request content
                requestStream = new MemoryStream(Encoding.UTF8.GetBytes(originalRequestContent));
            }

            // Replace the request body stream
            context.Request.Body = requestStream;

            // Return the time zone info for the reverse conversion on the response
            return localTimeZone;
        }

        private MemoryStream ModifyResponseContent(
  HttpContext context,
  bool disableConversion,
  TimeZoneInfo localTimeZone,
  MemoryStream responseStream)
        {
            // Rewind the unmodified response stream
            responseStream.Position = 0;
            var modified = false;

            // Will capture the unmodified response for time zone conversion
            var responseContent = default(string);

            // Only attempt to modify the response if time zone conversion is not disabled
            // and we have a local time zone that was used to modify the request
            if (!disableConversion && localTimeZone != null)
            {
                // Capture the unmodified response
                responseContent = new StreamReader(responseStream).ReadToEnd();

                // Try to get the JSON object from the response content
                var jobj = responseContent.TryDeserializeToJToken();

                // If the response content is a JSON object, convert all of it's date/time properties from local time to UTC
                //if (jobj != null && jobj.ConvertUtcToLocal(localTimeZone))
                //{
                //    responseContent = JsonConvert.SerializeObject(jobj);
                //    modified = true;
                //}
            }

            // If no changes were made (i.e., there were no converted date/time properties),
            // use the original unmodified response
            if (!modified)
            {
                responseStream.Position = 0;
                context.Response.ContentLength = responseStream.Length;
                return responseStream;
            }

            // Write the changed response content to a new modified response stream
            var modifiedResponseStream = new MemoryStream();
            var sw = new StreamWriter(modifiedResponseStream);
            sw.Write(responseContent);
            sw.Flush();
            modifiedResponseStream.Position = 0;

            // Use the new modified response
            context.Response.ContentLength = modifiedResponseStream.Length;
            return modifiedResponseStream;
        }
    }
}