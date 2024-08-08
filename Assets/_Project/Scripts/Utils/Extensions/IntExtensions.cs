using Lean.Localization;
using System.Globalization;

namespace Project.Utils.Extensions
{
    public static class IntExtensions
    {
        private const float Thousand = 1000;
        private const float Million = 1000000;
        private const string ThousandToken = "Misc/thousandToken";
        private const string MillionToken = "Misc/millionToken";

        public static string ToNumericalString(this int value)
        {
            if (value < Thousand)
                return value.ToString();

            if (value < Million)
                return (value / Thousand).ToString("f1", CultureInfo.InvariantCulture) + LeanLocalization.GetTranslationText(ThousandToken);

            else
                return (value / Million).ToString("f1", CultureInfo.InvariantCulture) + LeanLocalization.GetTranslationText(MillionToken);
        }
    }
}