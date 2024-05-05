// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        Stopwatch sw; 
        // Tests: Sample size N = 100 000 randomly shuffled integers from 0 to 99 999
    
        int[] p1 = RandomUnique();
        sw = Stopwatch.StartNew();
        int[] b = SelectionSort(p1, 0, p1.Length-1);
        //Console.WriteLine("Selectionsort: [{0}]", string.Join(", ", b));
        Console.WriteLine($"SelectionSort: {sw.ElapsedMilliseconds} miliseconds");

        int[] p2 = RandomUnique();
        sw = Stopwatch.StartNew();
        int[] a = InsertionSort(p2, 0, p2.Length-1);
        //Console.WriteLine("InsertionSort: [{0}]", string.Join(", ", a));
        Console.WriteLine($"InsertionSort: {sw.ElapsedMilliseconds} miliseconds");

        int[] p3 = RandomUnique();
        sw = Stopwatch.StartNew();
        int[] c = QuickSort(p3, 0, p3.Length-1);
        //Console.WriteLine("Quicksort: [{0}]", string.Join(", ", c));
        Console.WriteLine($"Quicksort: {sw.ElapsedMilliseconds} miliseconds");
    }

    static int[] InsertionSort(int[] A, int a, int b)
    {
        for (int i = a+1; i<=b; i++) // Loop through array.
        {
            int x = A[i]; // Take current key to sort into subarray A[0;i].
            int j = i-1;
            while (j>=0 && A[j] > x) // Compare against all values in subarray. 
            {
                A[j+1] = A[j]; // Shift values of subarray. 
                j--;
            }
        A[j+1] = x;
        }
        return A;
    }

    static int[] SelectionSort(int[] A, int a, int b)
    {
        for (int i=a; i<b; i++) // Loop through array
        {
            int j_min = i; // Take base value of min
            for (int j = i+1; j<=b; j++) //  Search for the smallest value of subarray A[i;n]
            {
                if (A[j] < A[j_min]) j_min = j;
            }
            A = Swap(A, i, j_min); // Swap smallest value of subarray to the front. 
        }
        return A; 
    }

    static int[] Swap(int[] A, int i, int j) // Method to swap two elements of array. 
    {
        int sub = A[j]; 
        A[j] = A[i];
        A[i] = sub;
        return A;
    }

    static int[] QuickSort(int[] Arr, int a, int b)
    {
        if (b - a > 1) // Variant, done when a is next to or on b. 
        {
            if (b - a <= 32) // InsertionSort is faster for small arrays.
            {
                Arr = InsertionSort(Arr, a, b); 
                return Arr;
            }
            Random rand = new Random(); // Create random pivot
            int pivot = rand.Next(a, b+1);

            int br = Split(Arr, a, b, pivot); // Sort array around pivot value
            QuickSort(Arr, a, br); // Recursion step for the split arrays. 
            QuickSort(Arr, br+1, b);
        }
        return Arr;
    }

    static int Split(int[] Arr, int a, int b, int pivot) // Split part of Quicksort function. 
    {
        Arr = Swap(Arr, a, pivot); //First, move pivot to the front. 
        int r = a; // convenient naming to show we aren't working with original a and b anymore. 
        int s = b;
        while (r<s)
        {
            if (Arr[r+1] <= Arr[r]) //If neighbour of pivot (r) is smaller, swap them and move r forward. 
            {
                Arr = Swap(Arr, r, r+1); 
                r++;
            }
            else // Else, swap in a new neighbour to compare. Move the swapables back one space. 
            {
                Arr = Swap(Arr, s, r+1);
                s--;
            }
        }
        return r; 
    }

    // Methods for testing purposes: 

    static int[] RandomUnique() // Create random array of numbers
    {
        int[] intArray = new int[100000];
        for (int i = 0; i < 100000; i++)
        {
            intArray[i] = i;
        }
        intArray = Shuffle(intArray);

        return intArray;
    }

    static int[] Shuffle(int[] obj) // Helper function to randomly shuffle our array. 
    {
        for (int i = 0; i < obj.Length; i++)
        {
            int temp = obj[i];
            Random r = new Random();
            int objIndex = r.Next(0, obj.Length);
            obj[i] = obj[objIndex];
            obj[objIndex] = temp;
        }
        return obj;
    }
}
