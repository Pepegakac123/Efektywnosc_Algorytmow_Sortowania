using BenchmarkDotNet.Running;
using System;

namespace Efektywnosc_Algorytmow_Sortowania
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SortingBenchmarks>();
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}
