using System;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        static readonly string DateTimePattern = "yyyy-MM-dd HH:mm:ss";
        public static string ToDefaultString(this DateTime dt)
        {
            return dt.ToString(DateTimePattern);
        }
    }

}
