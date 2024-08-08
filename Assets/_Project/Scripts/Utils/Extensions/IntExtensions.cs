using System.Globalization;

namespace Project.Utils.Extensions
{
    public static class IntExtensions
    {
        public static string ToValueString(this int value)
        {
            if (value < 1000)
                return value.ToString();

            if (value < 100000)
                return (value / 1000f).ToString("f1", CultureInfo.InvariantCulture) + "k";

            else
                return (value / 1000000f).ToString("f1") + "m";
        }
    }
}