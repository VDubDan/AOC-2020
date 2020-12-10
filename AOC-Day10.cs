using System;
using System.Collections.Generic;
using System.IO;

namespace AOC_2020_Day10
{
    class Program
    {

        static void Main(string[] args)
        {
            int startJolts = 0;
            int[] adapterRatings = new int[] {1, 2, 3};

            string[] input = File.ReadAllLines("AOC-2020-Day10-Input.txt");

            // Convert input to list of int
            List<int> adapterList = new List<int>();

            foreach (string adapter in input)
            {
                adapterList.Add(Convert.ToInt32(adapter));
            }

            int currentJolts = startJolts;

            // Keep track of our ordering
            List<int> orderedAdapters = new List<int>();

            int diffThree = 0;
            int diffOne = 0;

            while (adapterList.Count > 0)
            {

                foreach (int rating in adapterRatings)
                {
                    int index = adapterList.IndexOf(currentJolts + rating );

                    // Search succesful
                    if (index > -1)
                    {
                        if (rating == 1) diffOne++;
                        if (rating == 3) diffThree++;

                        Console.WriteLine(adapterList[index]);

                        orderedAdapters.Add(adapterList[index]);
                        currentJolts = adapterList[index];
                        adapterList.RemoveAt(index);
                        break;
                    }

                }

            }

            // Account for the device
            diffThree++;
            int answer = diffOne * diffThree;

            Console.WriteLine(diffOne + " differences of 1");
            Console.WriteLine(diffThree + " differences of 3");

            Console.WriteLine("Answer: " + answer);
            Console.ReadLine();

        }

    }
}
