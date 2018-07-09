using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace sorts
{
    class Program
    {
        static SortChecker checking = new SortChecker();

        static int[] generateArray(int lenght, int vars)
        {
            int percents = 0;
            int marker = 0;
            var rand = new Random();
            var array = new int[lenght];

            Console.WriteLine("Generating " + lenght + " numbers.");
            Console.Write(percents + "%");

            if (lenght >= 1000)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rand.Next(vars);
                    marker++;
                    if (marker >= lenght / 100)
                    {
                        marker = 0;
                        percents++;
                        Console.Write("\r" + percents + "%");
                    }
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rand.Next(vars);
                }
                Console.Write("\r100%");
            }

            return array;
        }

        static void printArray(int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("[{0}] = {1}", i, arr[i]);
            }
        }

        static void arrayPreparer(int[] arr, int[] toCpy)
        {
            for (int i = 0; i < toCpy.Length - 1; i++)
            {
                arr[i] = toCpy[i];
            }
        }

        static void parallelQuick(int[] arr)
        {
            int[] forParallel = new int[arr.Length];
            arrayPreparer(forParallel, arr);

            Stopwatch stoper1 = new Stopwatch();
            Console.Write("Parallel quicksorting. Please wait...");
            QuickParallel qpar = new QuickParallel(forParallel);

            stoper1.Start();
            qpar.sort(0, forParallel.Length - 1);
            stoper1.Stop();

            if (!checking.checkSort(qpar.getArray))
            {
                Console.WriteLine("\rQuicksort failed.                          ");
            }
            else
            {
                Console.WriteLine("\rQuicksort parallel done. Elapsed time: {0}", stoper1.Elapsed.TotalSeconds.ToString());
            }
        }

        static void sequentialQuick(int[] arr)
        {
            
            int[] forSequential = new int[arr.Length];
            arrayPreparer(forSequential, arr);

            Stopwatch stoper2 = new Stopwatch();
            Console.Write("Sequential quicksorting. Please wait...");
            QuickSequential qseq = new QuickSequential(forSequential);
            stoper2.Start();
            qseq.sort(0, forSequential.Length - 1);            
            stoper2.Stop();

            if (!checking.checkSort(qseq.getArray))
            {
                Console.WriteLine("\rQuicksort failed.                       ");
            }
            else
            {
                Console.WriteLine("\rQuicksort sequential done. Elapsed time: {0}", stoper2.Elapsed.TotalSeconds.ToString());
            }
        }

        static void parallelMerge(int[] arr)
        {
            int[] forParallel = new int[arr.Length];
            arrayPreparer(forParallel, arr);

            Stopwatch stoper2 = new Stopwatch();
            Console.Write("Parallel mergesorting. Please wait...");
            MergeParallel mpar = new MergeParallel(forParallel);
            stoper2.Start();
            mpar.sort(0, forParallel.Length, Environment.ProcessorCount);
            stoper2.Stop();
            if (!checking.checkSort(mpar.getArray))
            {
                Console.WriteLine("\rMergesort failed.                      ");
            }
            else
            {
                Console.WriteLine("\rMergesort parallel done. Elapsed time: {0}", stoper2.Elapsed.TotalSeconds.ToString());
            }
        }

        static void sequentialMerge(int[] arr)
        {
            int[] forSequential = new int[arr.Length];
            arrayPreparer(forSequential, arr);

            Stopwatch stoper2 = new Stopwatch();
            Console.Write("Sequential mergesorting. Please wait...");
            MergeSequential mseq = new MergeSequential(forSequential);
            stoper2.Start();
            mseq.sort(0, forSequential.Length - 1);
            stoper2.Stop();

            if (!checking.checkSort(mseq.getArray))
            { 
                Console.WriteLine("\rMergeksort failed.                      ");
            }
            else
            {
                Console.WriteLine("\rMergesort sequential done. Elapsed time: {0}", stoper2.Elapsed.TotalSeconds.ToString());
            }
        }

        static void parallelRadix(int[] arr)
        {
            int[] forParallel = new int[arr.Length];
            arrayPreparer(forParallel, arr);

            Stopwatch stoper2 = new Stopwatch();
            Console.Write("Parallel radixsorting. Please wait...");
            RadixParallel rpar = new RadixParallel(forParallel);
            stoper2.Start();
            rpar.sort();
            stoper2.Stop();

            if (!checking.checkSort(rpar.getArray))
            {
                Console.WriteLine("\rRadixsort failed.                           ");
            }
            else
            {
                Console.WriteLine("\rRadixsort parallel done. Elapsed time: {0}", stoper2.Elapsed.TotalSeconds.ToString());
            }
        }

        static void sequentialRadix(int[] arr)
        {
            int[] forSequential = new int[arr.Length];
            arrayPreparer(forSequential, arr);

            Stopwatch stoper2 = new Stopwatch();
            Console.Write("Sequential radixsorting. Please wait...");

            RadixSequential rseq = new RadixSequential(forSequential);
            stoper2.Start();
            rseq.sort();
            stoper2.Stop();

            if (!checking.checkSort(rseq.getArray))
            {
                Console.WriteLine("\rRadixsort failed.                           ");
            }
            else
            {
                Console.WriteLine("\rRadixsort sequential done. Elapsed time: {0}", stoper2.Elapsed.TotalSeconds.ToString());
            }            
        }

        static void Main(string[] args)
        {
            int[] scrambled = generateArray(50000000, 10000000);
            Console.WriteLine("\n\nAvaiable threads: {0}\n", Environment.ProcessorCount);

            /*sequentialQuick(scrambled);
            parallelQuick(scrambled);
            sequentialMerge(scrambled);
            parallelMerge(scrambled);*/
            sequentialRadix(scrambled);
            parallelRadix(scrambled);

            Console.ReadKey();
        }
    }
}
