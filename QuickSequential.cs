using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sorts
{
    class QuickSequential
    {
        int[] arraySequential;

        public QuickSequential(int[] arr)
        {
            arraySequential = arr;
        }

        public void sort(int left, int right)
        {
            if (left >= right)
            {
                return;
            }
            
            int supplementary;
            supplementary = arraySequential[left];
            arraySequential[left] = arraySequential[right];//(left + right) / 2];
            arraySequential[right] = supplementary;

            int last = left;

            for (int current = left + 1; current <= right; ++current)
            {
                if (arraySequential[current] < arraySequential[left])
                {
                    ++last;
                    supplementary = arraySequential[last];
                    arraySequential[last] = arraySequential[current];
                    arraySequential[current] = supplementary;
                }
            }
            
            supplementary = arraySequential[left];
            arraySequential[left] = arraySequential[last];
            arraySequential[last] = supplementary;

            sort(left, last - 1);
            sort(last + 1, right);
        }
        
        public int[] getArray
        {
            get
            {
                return arraySequential;
            }
        }
    }
}
