using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sorts
{
    class QuickParallel
    {
        private int[] arrayParallel;
        private int avaiableMaxLenght;

        public QuickParallel(int[] arr)
        {
            arrayParallel = arr;
            avaiableMaxLenght = arrayParallel.Length / (Environment.ProcessorCount*4);
        }

        public void sort(int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int supplementary;

            supplementary = arrayParallel[left];
            arrayParallel[left] = arrayParallel[(left + right) / 2];
            arrayParallel[(left + right) / 2] = supplementary;

            int last = left;
            for (int current = left + 1; current <= right; ++current)
            {
                if (arrayParallel[current].CompareTo(arrayParallel[left]) < 0)
                {
                    ++last;

                    supplementary = arrayParallel[last];
                    arrayParallel[last] = arrayParallel[current];
                    arrayParallel[current] = supplementary;
                }
            }
            
            supplementary = arrayParallel[left];
            arrayParallel[left] = arrayParallel[last];
            arrayParallel[last] = supplementary;

            if ((right - left) < avaiableMaxLenght)
            {
                sort( left, last - 1);
                sort( last + 1, right);
            }
            else
            {
                Parallel.Invoke(
                    () => sort( left, last - 1),
                    () => sort( last + 1, right)
                );
            }
        }

        public int[] getArray
        {
            get
            {
                return arrayParallel;
            }
        }
    }
}
