using System.Globalization;

namespace Project.Utils.Extensions
{
    public static class IntExtensions
    {
        public static string ToFormatString(this int value)
        {
            if (value < 1000)
                return value.ToString();

            if (value < 100000)
                return (value / 1000f).ToString("F1", CultureInfo.InvariantCulture) + "K";

            else
                return (value / 1000000f).ToString("F1") + "M";
        }
    }
}