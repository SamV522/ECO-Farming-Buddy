namespace ECO_Farming_Buddy.Utilities
{
    internal static class MathUtilities
    {
        public static decimal Middle(decimal low, decimal high)
        {
            return low + ((high - low) / 2.00M);
        }

        /// <summary>
        /// Inverse Lerp between A and B with value V
        /// </summary>
        /// <param name="a">Minimum = 0</param>
        /// <param name="b">Maximum = 1</param>
        /// <param name="v">Value</param>
        /// <returns></returns>
        public static decimal InverseLerp(decimal a, decimal b, decimal v)
        {
            return (v - a) / (b - a);
        }
    }
}
