# Sprawozdanie: Badanie efektywności algorytmów sortowania

**Autor:** [imie_nazwisko]  
**Numer albumu:** [numer_albumu]  
**Grupa laboratoryjna:** [grupa]  
**Data:** 27 marca 2026 r.

## 1. Opis eksperymentu
Celem eksperymentu było empiryczne zbadanie wydajności czterech algorytmów sortowania w różnych warunkach wejściowych. Badaniu poddano:
*   **Insertion Sort** (Sortowanie przez wstawianie)
*   **Merge Sort** (Sortowanie przez scalanie)
*   **Quick Sort Classical** (Klasyczny QuickSort z partycjonowaniem Lomuto)
*   **Quick Sort Heuristic** (Biblioteczna metoda `Array.Sort()`)

### Kryteria testowe:
*   **Rozmiary danych:** 10 (mała), 1000 (średnia), 10 000 (duża).
*   **Rozkłady danych:**
    *   `Random`: wartości losowe.
    *   `Sorted`: dane już posortowane rosnąco.
    *   `Reversed`: dane posortowane malejąco.
    *   `AlmostSorted`: dane prawie posortowane (5% zamienionych miejscami).
    *   `FewUnique`: tablica z dużą liczbą powtórzeń (tylko 10 unikalnych wartości).

## 2. Opis implementacji

### Konfiguracja środowiska
Eksperyment został przeprowadzony przy użyciu frameworka **BenchmarkDotNet v0.15.8** na platformie **.NET 10**. 
*   **Procesor:** AMD Ryzen 5 5600 (12 logicznych rdzeni)
*   **Tryb kompilacji:** Release (z pełnymi optymalizacjami JIT)

### Implementacja algorytmów i generatorów
*   Algorytmy zostały zaimplementowane w klasie `SortingAlgorithms` zgodnie z logiką serwisu GeeksForGeeks.
*   Klasa `Generators` zapewnia powtarzalność danych dzięki użyciu stałego ziarna (seed) dla generatora liczb losowych w ramach serii.
*   W klasie `SortingBenchmarks` wykorzystano atrybut `[IterationSetup]`, aby każdy algorytm otrzymywał identyczną, nieposortowaną kopię tablicy bazowej (`Clone()`), co zapobiega zafałszowaniu wyników przez sortowanie już przetworzonych danych.

## 3. Wyniki pomiarów

Poniżej przedstawiono skróconą tabelę wyników dla największego rozmiaru tablicy (10 000 elementów), gdzie różnice w złożoności są najbardziej widoczne.

| Metoda | DataType | Rozmiar | Czas (Mean) | Alokacja RAM |
| :--- | :--- | :--- | :--- | :--- |
| **InsertionSort** | Sorted | 10 000 | **14.9 μs** | 0 B |
| **QuickSortHeuristic** | Random | 10 000 | **326.6 μs** | 0 B |
| **QuickSortClassical**| Random | 10 000 | **1.33 ms** | 0 B |
| **MergeSort** | Random | 10 000 | **1.97 ms** | 1.02 MB |
| **InsertionSort** | Reversed | 10 000 | **22.7 ms** | 0 B |
| **QuickSortClassical**| Sorted | 10 000 | **34.5 ms** | 0 B |

*(Pełne wyniki znajdują się w plikach artefaktów BenchmarkDotNet w folderze Markdown/HTML).*

## 4. Wnioski

1.  **Dominacja biblioteczna:** `Array.Sort()` (QuickSortHeuristic) okazał się najbardziej uniwersalnym algorytmem. Dzięki zastosowaniu hybrydowych technik (Introspective Sort), unika on pesymistycznej złożoności $O(n^2)$ dla danych posortowanych, w przeciwieństwie do klasycznego QuickSorta.
2.  **Fenomen Insertion Sort:** Algorytm ten jest ekstremalnie szybki dla danych prawie posortowanych lub o bardzo małym rozmiarze. Jednak przy danych odwrotnie posortowanych jego czas rośnie drastycznie (z 14 μs do 22 ms), co potwierdza złożoność kwadratową $O(n^2)$.
3.  **Problem klasycznego QuickSorta:** Implementacja z pivotem na końcu (Lomuto) wykazuje tragiczne wyniki dla danych już posortowanych (34 ms). Jest to klasyczny przypadek "najgorszego scenariusza", gdzie drzewo rekurencji staje się całkowicie niesymetryczne.
4.  **Stabilność Merge Sort:** Merge Sort wykazuje bardzo zbliżone czasy niezależnie od rozkładu danych ($O(n \log n)$), jednak jako jedyny wymaga znacznej alokacji dodatkowej pamięci (proporcjonalnej do rozmiaru tablicy).
5.  **Wybór algorytmu:** Dla ogólnych zastosowań w .NET należy zawsze polegać na `Array.Sort()`. W specyficznych przypadkach, gdy wiemy, że dane są "prawie posortowane" i jest ich niewiele, prosty `Insertion Sort` może być mikrosekundowo szybszy.

---
*Wygenerowano automatycznie przez system badawczy.*
