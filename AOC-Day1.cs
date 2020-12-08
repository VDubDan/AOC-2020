using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2020_Day1
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] input = File.ReadAllLines("AOC-2020-Day1-Input.txt");

            for (int firstNumberLine = 0; firstNumberLine < input.Length; firstNumberLine++)
            {
                int firstNumber = Convert.ToInt32(input[firstNumberLine]);

                for (int secondNumberLine = 0; secondNumberLine < input.Length; secondNumberLine++)
                {

                    int secondNumber = Convert.ToInt32(input[secondNumberLine]);

                    // Don't add same number twice
                    if (firstNumberLine == secondNumberLine) continue;

                    for (int thirdNumberLine = 0; thirdNumberLine < input.Length; thirdNumberLine++)
                    {
                        if (firstNumberLine == thirdNumberLine) continue;
                        if (secondNumberLine == thirdNumberLine) continue;

                        int thirdNumber = Convert.ToInt32(input[thirdNumberLine]);

                        int totalThree = firstNumber + secondNumber + thirdNumber;

                        if (totalThree == 2020)
                        {
                            int productThree = firstNumber * secondNumber * thirdNumber;

                            Console.WriteLine("Lines " + firstNumberLine + " and " + secondNumberLine + " and " +thirdNumberLine + " make 2020");
                            Console.WriteLine("Product: " + productThree);

                        }

                    }

                    int total = firstNumber + secondNumber;

                    if (total == 2020)
                    {
                        // Found our target total
                        int product = firstNumber * secondNumber;

                        Console.WriteLine("Lines " + firstNumberLine + " and " + secondNumberLine + "make 2020");
                        Console.WriteLine("Product: " + product);
           
                    }




                }
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        
    }
}
