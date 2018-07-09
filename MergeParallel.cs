using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace sorts
{
    class MergeParallel
    {
        private int[] array;
        private int[] supplementary;

        public MergeParallel(int[] arr)
        {
            array = arr;
            supplementary = new int[arr.Length];
        }

        /*private void merging(int left, int right)
        {
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
        }*/

        public void sort(int left, int right, int depth)
        {
            if (left < right)
            {
                if (depth < 2)
                {
                    sort(left, left + ((right - left) / 2), depth);
                    sort(left + ((right - left) / 2) + 1, right, depth);
                }
                else
                {
                    depth -= 2;
                    Parallel.Invoke(
                        () => sort(left, left + ((right - left) / 2), depth),
                        () => sort(left + ((right - left) / 2) + 1, right, depth)
                    );
                }


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
