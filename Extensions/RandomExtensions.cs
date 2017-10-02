using System;

namespace EndesaBot.Extensions
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static int Nextint(this Random random, int minValue, int maxValue)
        {
            return Convert.ToInt32(random.NextDouble(minValue, maxValue));
        }
    }
}