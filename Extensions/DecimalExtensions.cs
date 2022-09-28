namespace ECO_Farming_Buddy.Extensions
{
    internal static class DecimalExtensions
    {
        public static bool Between(this decimal sourceDecimal, decimal min, decimal max)
        {
            bool above = sourceDecimal >= min;
            bool below = sourceDecimal <= max;
            return above && below;
        }

        public static decimal DifferenceFromRange(this decimal sourceDecimal, decimal min, decimal max)
        {
            if(sourceDecimal < min)
            {
                return sourceDecimal - min;
            }

            if(sourceDecimal > max)
            {
                return sourceDecimal - max;
            }

            return 0;
        }
    }
}
