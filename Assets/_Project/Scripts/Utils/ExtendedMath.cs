namespace Scripts.Utils
{
    public static class ExtendedMath
    {
        public static float Remap(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
        {
            return targetFrom + (targetTo - targetFrom) * ((source - sourceFrom) / (sourceTo - sourceFrom));
        }
    }
}