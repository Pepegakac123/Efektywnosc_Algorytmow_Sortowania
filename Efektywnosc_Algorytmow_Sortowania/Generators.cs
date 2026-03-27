using System;
using System.Linq;

namespace Efektywnosc_Algorytmow_Sortowania
{
    public static class Generators
    {
        private static readonly Random _random = new Random();

        // Stałe definiujące domyślne parametry generatorów
        public const int DefaultMinVal = 0;
        public const int DefaultMaxVal = 1000000;
        public const double DefaultShufflePercent = 0.05; // 5%
        public const int DefaultUniquePoolSize = 10;

        // 1. random
        public static int[] GenerateRandom(int size, int minVal = DefaultMinVal, int maxVal = DefaultMaxVal)
        {
            int[] a = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = _random.Next(minVal, maxVal);
            }
            return a;
        }

        // 2. sorted
        public static int[] GenerateSorted(int size, int minVal = DefaultMinVal, int maxVal = DefaultMaxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }

        // 3. reversed
        public static int[] GenerateReversed(int size, int minVal = DefaultMinVal, int maxVal = DefaultMaxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }

        // 4. almost sorted
        public static int[] GenerateAlmostSorted(int size, int minVal = DefaultMinVal, int maxVal = DefaultMaxVal, double shufflePercent = DefaultShufflePercent)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);

            // ile zamian wykonać
            int swaps = (int)(size * shufflePercent);
            if (swaps == 0 && size > 1) swaps = 1;

            for (int i = 0; i < swaps; i++)
            {
                int idx1 = _random.Next(size);
                int idx2 = _random.Next(size);
                (a[idx1], a[idx2]) = (a[idx2], a[idx1]);
            }
            return a;
        }

        // 5. few unique
        public static int[] GenerateFewUnique(int size, int uniqueCount = DefaultUniquePoolSize, int minVal = DefaultMinVal, int maxVal = DefaultMaxVal)
        {
            int[] a = new int[size];
            int[] uniqueValues = new int[uniqueCount];
            for (int i = 0; i < uniqueCount; i++)
            {
                uniqueValues[i] = _random.Next(minVal, maxVal);
            }
            for (int i = 0; i < size; i++)
            {
                int randomIndexFromPool = _random.Next(uniqueCount);
                a[i] = uniqueValues[randomIndexFromPool];
            }
            return a;
        }
    }
}
