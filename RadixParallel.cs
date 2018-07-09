using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace sorts
{
    class RadixParallel
    {
        private int[] mainArray;
        private int[] pointers;

        public RadixParallel(int[] arr)
        {
            this.mainArray = arr;
        }

        public void sort()
        {
            pointers = new int[Environment.ProcessorCount];
            pointers[Environment.ProcessorCount - 1] = mainArray.Length;

            PreSortEnhanced();

            List<Thread> threads = new List<Thread>();

            Thread t = new Thread(() => { radixWG(0, pointers[0]); });
            threads.Add(t);

            for (int i = 1; i < Environment.ProcessorCount; i++)
            {
                int a = i;
                Thread thr = new Thread(() => { radixWG(pointers[a - 1], pointers[a]); });
                threads.Add(thr);
                thr.Start();
            }
            t.Start();

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        private void PreSortEnhanced()
        {
            int i, j;
            int[] tmp = new int[mainArray.Length];
            int[] biggest = new int[mainArray.Length];
            int shift;
            int arrayPos = 0;
            int index = 1;
            int available = Environment.ProcessorCount - 1;
            bool found;

            while (available > 0)
            {
                found = false;
                int preSorted = arrayPos;
                shift = index;
                j = 0;
                for (i = 0; i < mainArray.Length - preSorted; ++i)
                {
                    bool move = (mainArray[i] << shift) >= 0;

                    if (shift == 0 ? !move : move)
                    {
                        mainArray[i - j] = mainArray[i];
                    }
                    else
                    {
                        arrayPos++;
                        tmp[j++] = mainArray[i];
                        found = true;
                    }
                }
                index++;

                if (found)
                {
                    Array.Copy(tmp, 0, mainArray, mainArray.Length - arrayPos, j);
                    pointers[available - 1] = mainArray.Length - arrayPos;
                    available--;
                }
                else if (index >= 31)
                {
                    break;
                }
            }
        }

        private void radixWG(int min, int max)
        {
            int avaiableLength = max - min;
            int bitGroupLength = 1;
            int[] counting = new int[1 << bitGroupLength];
            int[] prefixes = new int[1 << bitGroupLength];

            int identifyingMask = (1 << bitGroupLength) - 1;

            int csIntLength = 32;

            int[] auxilarArray = new int[avaiableLength];
            int[] tempMain = new int[avaiableLength];
            Array.Copy(mainArray, min, tempMain, 0, avaiableLength);

            int nOofGroups = (int)Math.Ceiling(csIntLength / (double)bitGroupLength);

            for (int position = 0, shift = 0; position < nOofGroups; position++, shift += bitGroupLength)
            {
                for (int j = 0; j < counting.Length; j++)
                    counting[j] = 0;

                for (int i = 0; i < tempMain.Length; i++)
                    counting[(tempMain[i] >> shift) & identifyingMask]++;

                prefixes[0] = 0;
                for (int i = 1; i < counting.Length; i++)
                    prefixes[i] = prefixes[i - 1] + counting[i - 1];

                for (int i = 0; i < tempMain.Length; i++)
                {
                    auxilarArray[prefixes[(tempMain[i] >> shift) & identifyingMask]++] = tempMain[i];
                }

                auxilarArray.CopyTo(tempMain, 0);

            }

            Array.Copy(tempMain, 0, mainArray, min, tempMain.Length);
        }

        public int[] getArray
        {
            get
            {
                return mainArray;
            }
        }

    }
}
