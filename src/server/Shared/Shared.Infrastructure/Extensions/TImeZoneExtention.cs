using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace FluentPOS.Shared.Infrastructure.Extensions
{
    public static class TImeZoneExtention
    {
        public static JToken TryDeserializeToJToken(this string json)
        {
            if (json == null || (!json.StartsWith("[") && !json.StartsWith("{")))
            {
                return null;
            }

            // Try to get the JSON object from the request content
            var jToken = default(JToken);
            try
            {
                jToken = JsonConvert.DeserializeObject<JToken>(json);
            }
            catch
            {
                // Ignore the exception, returning null to indicate bad JSON
            }

            return jToken;
        }

        public static bool ConvertLocalToUtc(this JToken token, TimeZoneInfo localTimeZone, bool wasModified = false)
        {
            bool modified = wasModified;
            if (token.Type == JTokenType.Object)
            {
                modified = ConvertLocalToUtcForObject(token, localTimeZone, wasModified, modified);
            }
            else if (token.Type == JTokenType.Array)
            {
                modified = ConvertLocalToUtcForArray(token, localTimeZone, wasModified, modified);
            }
            return modified;
        }

        private static bool ConvertLocalToUtcForObject(JToken token, TimeZoneInfo localTimeZone, bool wasModified, bool modified)
        {
            foreach (var prop in token.Children<JProperty>())
            {
                var child = prop.Value;
                if (child is JValue jValue)
                {
                    object value = ParseJsonValueForDateTime(jValue.Value);
                    if (value is DateTime)
                    {
                        var local = (DateTime)value;
                        var dt = new DateTime(local.Year,local.Month,local.Day,local.Hour,local.Minute,local.Second);
                        var utc = TimeZoneInfo.ConvertTimeFromUtc(dt, localTimeZone);
                        jValue.Value = utc;
                        modified = true;
                    }
                }
                else if (child.HasValues)
                {
                    modified = child.ConvertLocalToUtc(localTimeZone, wasModified) || modified;
                }
            }

            return modified;
        }

        private static object ParseJsonValueForDateTime(object value)
        {
            // If a date/time value includes seconds, it will be cast as a DateTime automatically
            // But if it's missing seconds, it will be treated as a string that we'll have to convert to a DateTime

            if (value is string)
            {
                string stringValue = value.ToString();

                if (stringValue.FromDateTimeIsoString(out DateTime dateTimeValue))
                {
                    value = dateTimeValue;
                }
            }

            return value;
        }

        private static bool ConvertLocalToUtcForArray(JToken token, TimeZoneInfo localTimeZone, bool wasModified, bool modified)
        {
            foreach (var item in token.Children())
            {
                var child = item;
                if (child.HasValues)
                {
                    modified = child.ConvertLocalToUtc(localTimeZone, wasModified) || modified;
                }
            }

            return modified;
        }

        public static bool FromDateTimeIsoString(this string value, out DateTime dateTime)
        {
            if (
                (value.Length == 16 || (value.Length == 19 && value[16] == ':')) &&
                value[4] == '-' &&
                value[7] == '-' &&
                value[10] == 'T' &&
                value[13] == ':' &&
                DateTime.TryParse(value, out DateTime parsedDateTime)  // calls DateTime.TryParse only after passing the smell test
               )
            {
                dateTime = parsedDateTime;
                return true;
            }

            dateTime = DateTime.MinValue;
            return false;
        }

        public static string ToDateTimeIsoString(this DateTime value) =>
          value.ToString("yyyy-MM-ddTHH:mm:ss");


    }
}