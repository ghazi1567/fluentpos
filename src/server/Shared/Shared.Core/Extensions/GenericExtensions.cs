using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace FluentPOS.Shared.Core.Extensions
{
    public static class GenericExtensions
    {
        public static IEnumerable<List<T>> SplitList<T>(this List<T> list, int nSize = 10)
        {
            for (int i = 0; i < list.Count; i += nSize)
            {
                yield return list.GetRange(i, Math.Min(nSize, list.Count - i));
            }
        }

        public static DateTime PST(this DateTime dateTime)
        {
            TimeZoneInfo targetTimeZone = TZConvert.GetTimeZoneInfo("Pakistan Standard Time");
            var targetDAteTime = TimeZoneInfo.ConvertTime(dateTime, targetTimeZone);
            return targetDAteTime;
        }

        public static DateTime StartDate(this DateTime dateTime)
        {
            DateTime dt = dateTime.PST();
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        public static DateTime EndDate(this DateTime dateTime)
        {
            DateTime dt = dateTime.PST();
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }
    }
}
