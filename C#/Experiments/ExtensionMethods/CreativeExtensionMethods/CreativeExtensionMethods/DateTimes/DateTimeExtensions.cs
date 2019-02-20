using System;

namespace CreativeExtensionMethods.DateTimes
{
    public static class DateTimeExtensions
    {
        public static string ToW3CDate(this DateTime dt)
        {
            return dt.ToUniversalTime().ToString("s") + "Z";
        }

        public static int GetQuarter(this DateTime fromDate)
        {
            return (fromDate.Month - 1) / 3 + 1;
        }
    }
}