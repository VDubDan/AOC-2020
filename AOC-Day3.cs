using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2020_Day3
{
    class Program
    {

        public class ForestLine
        {
            public readonly string line;

            public ForestLine(string line)
            {
                this.line = line;
            }

            public bool isTree(int xPos)
            {
                char[] lineArray = line.ToCharArray();

                int arrayPos = xPos;

                if (arrayPos >= lineArray.Length)
                {

                    double result = (double) arrayPos / lineArray.Length;

                    lineArray = string.Concat(Enumerable.Repeat(line, (int) Math.Ceiling(result+10))).ToCharArray();


                }

                if (lineArray[arrayPos] == '#')
                {
                    return true;
                }

                return false;
            }


        }


        static void Main(string[] args)
        {



            int right = 1;
            int down = 2;

            string[] input = File.ReadAllLines("AOC-2020-Day3-Input.txt");

            List<ForestLine> lineList = new List<ForestLine>();

            foreach (string line in input)
            {
                ForestLine forestLine = new ForestLine(line);
                lineList.Add(forestLine);
            }

            int currentYPos = 0;
            int currentXPos = 0;

            int numTrees = 0;

            while (currentYPos < lineList.Count)
            {

                if (lineList[currentYPos].isTree(currentXPos))
                {
                    numTrees++;
                }


                currentXPos += right;
                currentYPos += down;
            }

            Console.WriteLine(numTrees);
            Console.ReadLine();
        }
    }
}
