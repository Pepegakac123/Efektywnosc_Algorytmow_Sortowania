using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;

namespace Efektywnosc_Algorytmow_Sortowania
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class SortingBenchmarks
    {
        private int[] _data = Array.Empty<int>();
        private int[] _dataToSort = Array.Empty<int>();

        // Rozmiary tablic
        [Params(10, 1000, 10000)]
        public int Size;

        [Params("Random", "Sorted", "Reversed", "AlmostSorted", "FewUnique")]
        public string DataType = string.Empty;

        [GlobalSetup]
        public void Setup()
        {
            _data = DataType switch
            {
                "Random" => Generators.GenerateRandom(Size),
                "Sorted" => Generators.GenerateSorted(Size),
                "Reversed" => Generators.GenerateReversed(Size),
                "AlmostSorted" => Generators.GenerateAlmostSorted(Size),
                "FewUnique" => Generators.GenerateFewUnique(Size),
                _ => throw new ArgumentException("Unknown data type")
            };
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _dataToSort = (int[])_data.Clone();
        }

        [Benchmark]
        public void InsertionSort() => SortingAlgorithms.InsertionSort(_dataToSort);

        [Benchmark]
        public void MergeSort() => SortingAlgorithms.MergeSort(_dataToSort);

        [Benchmark]
        public void QuickSortClassical() => SortingAlgorithms.QuickSortClassical(_dataToSort);

        [Benchmark]
        public void QuickSortHeuristic() => SortingAlgorithms.QuickSortHeuristic(_dataToSort);
    }
}
