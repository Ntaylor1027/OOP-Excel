using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace MergeSort
{
    class Program
    {
        static void merge(int[] list, int l, int m, int r)
        {
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;
            int [] tempL = new int[n1];
            int[] tempR = new int[n2];
            for (i = 0; i < n1; i++)
            {
                tempL[i] = list[l + i];
            }
            for (j = 0; j < n2; j++)
            {
                tempR[j]=list[m + 1 + j];
            }

            i = 0;
            j = 0;
            k = l;
            while(i < n1 && j < n2)
            {
                if (tempL[i] < tempR[j])
                {
                    list[k] = tempL[i];
                    i++;
                }
                else
                {
                    list[k] = tempR[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                list[k] = tempL[i];
                i++;
                k++;
            }
            while(j < n2)
            {
                list[k] = tempR[j];
                j++;
                k++;
            }
        }

        static void mergeSort(int[] list, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                int middleIndex = (leftIndex + (rightIndex - 1)) / 2;
                mergeSort( list, leftIndex, middleIndex);
                mergeSort( list, middleIndex + 1, rightIndex);
                merge(list, leftIndex, middleIndex, rightIndex);
            }
        }

        static void threadedmergeSort(int[] list, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                int middleIndex = (leftIndex + (rightIndex - 1)) / 2;
                Thread thread1 = new Thread(new ThreadStart(() => threadedmergeSort( list, leftIndex, middleIndex)));
                Thread thread2 = new Thread(new ThreadStart(() =>threadedmergeSort( list, middleIndex + 1, rightIndex)));
                thread1.Start();
                thread2.Start();
                thread1.Join();
                thread2.Join();
                merge( list, leftIndex, middleIndex, rightIndex);
            }
        }

        static int[] arrayRandom(int count)
        {
            Random rdm = new Random();
            int[] ret = new int[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = rdm.Next(0, Int32.MaxValue);
            }
            return ret;
        }

        static void Main(string[] args)
        {
            int[] testVals = new int[] {8,64,256,1024};
            //int[] testVals = new int[] { 1024 };
            Console.WriteLine("Starting tests for merge sort vs. threaded merge sort");
            Console.WriteLine(" Array sizes under test: [8, 64, 256, 1024]");
            for (int i = 0; i < testVals.Length; i++)
            {
                int[] test = arrayRandom(testVals[i]);
                int[] mergeList = new int[test.Length];
                int[] threadedmergeList = new int[test.Length];
                Array.Copy(test, mergeList, test.Length);
                Array.Copy(test, threadedmergeList, test.Length);
                //Stopwatch threaded = new Stopwatch();
                //Stopwatch normal = new Stopwatch();
                //normal.Start();
                long millisecondsStartNormal = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                mergeSort(mergeList, 0, mergeList.Length - 1);
                long millisecondsEndNormal = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                //normal.Stop();
                //threaded.Start();
                long millisecondsStartThread= DateTimeOffset.Now.ToUnixTimeMilliseconds();
                threadedmergeSort(threadedmergeList, 0, threadedmergeList.Length - 1);
                long millisecondsEndThreaded = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                //threaded.Stop();
                long normaltime = millisecondsEndNormal - millisecondsStartNormal;
                long threadtime = millisecondsEndThreaded - millisecondsStartThread;
                Console.WriteLine("Starting test for size " + test[i].ToString() + " Test completed:");
                Console.WriteLine(" Normal Sort time (ms):            " + normaltime.ToString());
                Console.WriteLine(" Threaded Sort time (ms):          " + threadtime.ToString());

            }
            Console.WriteLine("Program complete, press key to continue");
        }
    }
}
