using System;

namespace NSA.WPF.Views.Rendering
{
    public class Randomizer
    {
        private static readonly Random _random = new Random();

        public static double Next()
        {
            return _random.NextDouble();
        }

        public static double Next(double from, double to)
        {
            return Next() * (to - from) + from;
        }
    }
}
