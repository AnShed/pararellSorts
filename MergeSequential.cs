using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sorts
{
    class MergeSequential
    {

        private int[] array;
        private int[] supplementary;

        public MergeSequential(int[] arr)
        {
            array = arr;
            supplementary = new int[arr.Length];
        }
        
        public void sort(int left, int right)
        {
            if (left < right)
            {
                sort(left, left + ((right - left) / 2));
                sort(left + ((right - left) / 2) + 1, right);

                for (int i = left; i <= right; i++)
                {
                    supplementary[i] = array[i];
                }

                int p = left;
                int q = (left + right) / 2 + 1;
                int r = left;
                while (p <= (left + right) / 2 && q <= right)
                {
                    if (supplementary[p] < supplementary[q])
                    {
                        array[r] = supplementary[p];
                        r++;
                        p++;
                    }
                    else
                    {
                        array[r] = supplementary[q];
                        r++;
                        q++;
                    }
                }

                while (p <= (left + right) / 2)
                {
                    array[r] = supplementary[p];
                    r++;
                    p++;
                }
            }
        }

        public int[] getArray
        {
            get
            {
                return array;
            }
        }
    }
}
