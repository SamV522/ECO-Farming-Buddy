using ECO_Farming_Buddy.Utilities;

namespace ECO_Farming_Buddy.Extensions
{
    internal static class DecimalExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceDecimal"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>Whether or not sourceDecimal is within min and max, inclusive</returns>
        public static bool Within(this decimal sourceDecimal, decimal min, decimal max)
        {
            bool above = sourceDecimal >= min;
            bool below = sourceDecimal <= max;
            return above && below;
        }

        /// <summary>
        /// Lerp between 0 and 1, 0 being closest to target, and 1 being closest to min or max.
        /// </summary>
        /// <param name="sourceDecimal"></param>
        /// <param name="target"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static decimal InverseLerpRangeUnclamped(this decimal sourceDecimal, decimal target, decimal min, decimal max)
        {
            if (sourceDecimal < target)
            {
                return MathUtilities.InverseLerp(target, min, sourceDecimal);
            }

            if (sourceDecimal > target)
            {
                return MathUtilities.InverseLerp(target, max, sourceDecimal);
            }

            return 0;
        }
    }
}
