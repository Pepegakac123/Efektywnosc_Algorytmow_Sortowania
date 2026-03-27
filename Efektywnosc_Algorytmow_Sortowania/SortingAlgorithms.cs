using System;

namespace Efektywnosc_Algorytmow_Sortowania
{
    public static class SortingAlgorithms
    {
        // 1. InsertionSort
        public static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        // 2. MergeSort
        public static void MergeSort(int[] arr)
        {
            SortMerge(arr, 0, arr.Length - 1);
        }

        private static void SortMerge(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;

                SortMerge(arr, l, m);
                SortMerge(arr, m + 1, r);

                Merge(arr, l, m, r);
            }
        }

        private static void Merge(int[] arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;

            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            i = 0;
            j = 0;
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        public static void QuickSortClassical(int[] arr)
        {
            SortQuick(arr, 0, arr.Length - 1);
        }

        private static void SortQuick(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);

                SortQuick(arr, low, pi - 1);
                SortQuick(arr, pi + 1, high);
            }
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }
        public static void QuickSortHeuristic(int[] arr)
        {
            Array.Sort(arr);
        }
    }
}
