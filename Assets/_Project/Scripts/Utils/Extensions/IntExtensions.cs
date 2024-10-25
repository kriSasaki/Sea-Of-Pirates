using System.Globalization;
using Lean.Localization;

namespace Scripts.Utils.Extensions
{
    public static class IntExtensions
    {
        private const float Thousand = 1000;
        private const float Million = 1000000;
        private const string KToken = "Misc/thousandToken";
        private const string MToken = "Misc/millionToken";

        public static string ToNumericalString(this int value)
        {
            if (value < Thousand)
                return value.ToString();
            else if (value < Million)
                return GetFormattedNumber(value / Thousand, KToken);
            else
                return GetFormattedNumber(value / Million, MToken);
        }

        private static string GetFormattedNumber (float value, string token)
        {
            return value.ToString("f1", CultureInfo.InvariantCulture) + GetRadix(token);
        }

        private static string GetRadix(string token)
        {
            return LeanLocalization.GetTranslationText(token);
        }
    }
}