using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2020_Day8
{
    class Program
    {
        internal enum Operation
        {
            acc,
            jmp,
            nop
        }

        public static class Execute
        {
            public static bool TestForLoop(string[] input)
            {
                int accumulator = 0;

                // Execute Loop

                Console.WriteLine("Starting Program");

                bool executing = true;
                bool hadLoop = false;
                int currentInstruction = 0;

                List<int> executedInstructions = new List<int>();

                while (executing)
                {
                    Console.WriteLine("Executing Line " + currentInstruction + ". Accumulator Value: " + accumulator);

                    // Check we didn't drop off the end
                    if (currentInstruction >= input.Length)
                    {
                        Console.WriteLine("No instruction at Line " + currentInstruction + ". Ending Execution");
                        executing = false;
                        continue;
                    }

                    // Check this instruction hasn't been exectued
                    if (executedInstructions.Contains(currentInstruction))
                    {
                        Console.WriteLine("Loop Detected: Line " + currentInstruction + " executed twice");
                        executing = false;
                        hadLoop = true;
                        continue;
                    }
                    else
                    {
                        executedInstructions.Add(currentInstruction);
                    }



                    string instruction = input[currentInstruction];

                    string strOperation = instruction.Split(' ')[0];
                    Operation operation = (Operation)Enum.Parse(typeof(Operation), strOperation, true);

                    int argument = Convert.ToInt32(instruction.Split(' ')[1]);


                    switch (operation)
                    {
                        case Operation.acc:
                            accumulator = accumulator + argument;
                            currentInstruction++;
                            continue;

                        case Operation.jmp:
                            currentInstruction = currentInstruction + argument;
                            continue;

                        case Operation.nop:
                            currentInstruction++;
                            continue;
                    }

                    // Shouldn't ever get here
                    throw new Exception();
                }

                return hadLoop;
            }

        }


        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("AOC-2020-Day8-Input.txt");

            bool looped = Execute.TestForLoop(input);

            if (!looped)
            {
                // Input should have looped
                throw new Exception();
            }

            Console.WriteLine("Code caused a loop - attempting repair");

            for (int i = 0; i < input.Length; i++)
            {
                string[] modifiedInput = new string[input.Length];
                input.CopyTo(modifiedInput,0);

                string line = modifiedInput[i];

                if (line.Contains("jmp"))
                {
                    modifiedInput[i] = line.Replace("jmp", "nop");
                    Console.WriteLine("Modifying Line " + i + "(JMP -> NOP)");
                } 
                else if (line.Contains("nop")) 
                {
                    modifiedInput[i] = line.Replace("nop", "jmp");
                    Console.WriteLine("Modifying Line " + i + "(NOP -> JMP)");
                }
                else
                {
                    // No modifications, no need to test
                    continue;
                }

                bool stillLoop = Execute.TestForLoop(modifiedInput);

                if (stillLoop == false)
                {
                    Console.WriteLine("Modifying Line " + i + " prevented the loop");
                    break;
                }
            }


            Console.ReadLine();
        }
    }
}
