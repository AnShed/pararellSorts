using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace sorts
{
    class RadixSequential
    {
        private int[] mainArray;

        public RadixSequential(int[] arr)
        {
            this.mainArray = arr;
        }

        public void sort()
        {
            int bitGroupLength = 1;
            int[] counting = new int[1 << bitGroupLength];
            int[] prefixes = new int[1 << bitGroupLength];

            int identifyingMask = (1 << bitGroupLength) - 1;

            int csIntLength = 32;

            int[] auxilarArray = new int[mainArray.Length];

            int nOofGroups = (int)Math.Ceiling(csIntLength / (double)bitGroupLength);

            for (int position = 0, shift = 0; position < nOofGroups; position++, shift += bitGroupLength)
            {
                for (int j = 0; j < counting.Length; j++)
                    counting[j] = 0;

                for (int i = 0; i < mainArray.Length; i++)
                    counting[(mainArray[i] >> shift) & identifyingMask]++;

                prefixes[0] = 0;
                for (int i = 1; i < counting.Length; i++)
                    prefixes[i] = prefixes[i - 1] + counting[i - 1];

                for (int i = 0; i < mainArray.Length; i++)
                    auxilarArray[prefixes[(mainArray[i] >> shift) & identifyingMask]++] = mainArray[i];

                auxilarArray.CopyTo(mainArray, 0);
            }
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
