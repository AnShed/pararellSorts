using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sorts
{
    class SortChecker
    {
        private int[] checkedArray;

        public SortChecker() { }

        public bool checkSort(int[] array)
        {
            checkedArray = array;

            int act, prvs;
            prvs = 0;
            act = 1;

            while (act <= checkedArray.Length - 1)
            {
                if (checkedArray[prvs] > checkedArray[act])
                {
                    return false;
                }
                prvs++;
                act++;
            }
            return true;
        }
    }
}
