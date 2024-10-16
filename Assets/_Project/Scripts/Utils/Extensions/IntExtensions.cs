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

            if (value < Million)
                return (value / Thousand)
                    .ToString("f1", CultureInfo.InvariantCulture) + LeanLocalization.GetTranslationText(KToken);
            else
                return (value / Million)
                    .ToString("f1", CultureInfo.InvariantCulture) + LeanLocalization.GetTranslationText(MToken);
        }
    }
}