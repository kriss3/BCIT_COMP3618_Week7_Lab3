using System;
using System.ComponentModel;
using System.Threading;

namespace WinAppMultithreading
{
    /// <summary>
    /// BCIT COMP 3618 
    /// Krzysztof Szczurowski Week 7 Lab 3
    /// Repo: https://github.com/kriss3/BCIT_COMP3618_Week7_Lab3.git
    /// Helper class to hold methods supporting main Application;
    /// </summary>
    public class Helper
    {
        //Method to mimic long running task;
        public static int TimeConsumingOperation(BackgroundWorker bw, int sleepPeriod)
        {
            int result = 0;

            Random rand = new Random();

            while (!bw.CancellationPending)
            {
                bool exit = false;

                switch (rand.Next(3))
                {
                    // Raise an exception.
                    case 0:
                        {
                            throw new Exception("An error condition occurred.");
                            break;
                        }

                    // Sleep for the number of milliseconds
                    // specified by the sleepPeriod parameter.
                    case 1:
                        {
                            Thread.Sleep(sleepPeriod);
                            break;
                        }

                    // Exit and return normally.
                    case 2:
                        {
                            result = 23;
                            exit = true;
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

                if (exit)
                {
                    break;
                }
            }
            return result;
        }
    }
}
