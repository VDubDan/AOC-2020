using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2020_Day2
{

    public class PasswordPolicy
    {

        public readonly char character;
        public readonly int min;
        public readonly int max;

        public PasswordPolicy(string rawPolicy)
        {
            string count = rawPolicy.Split(' ')[0];
            string letter = rawPolicy.Split(' ')[1];

            string min = count.Split('-')[0];
            string max = count.Split('-')[1];

            this.min = Convert.ToInt32(min);
            this.max = Convert.ToInt32(max);

            character = Char.Parse(letter);
        }

        public bool TestPasswordNew(string password)
        {
            char[] passwordChars = password.ToCharArray();

            char pos1 = passwordChars[min - 1];
            char pos2 = passwordChars[max - 1];

            bool pos1match = false;
            bool pos2match = false;

            if (pos1 == character) pos1match = true;
            if (pos2 == character) pos2match = true;

            if (pos1match && pos2match) return false;

            if (!pos1match && !pos2match) return false;

            if (pos1match && !pos2match)
            {
                return true;
            }

            if (!pos1match && pos2match)
            {
                return true;
            }



            return false;

        }


        public bool TestPasswordOld(string password)
        {
            int count = password.Split(character).Length - 1;

            if((count >= min) && (count <= max))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    public class Temp
    {

 
    }


    class Program
    {
        static void Main(string[] args)
        {

            string[] input = File.ReadAllLines("AOC-2020-Day2-Input.txt");

            int goodPasswords = 0;

            foreach(string line in input)
            {
                string policy = line.Split(':')[0];
                string password = line.Split(':')[1];


                PasswordPolicy passwordPolicy = new PasswordPolicy(policy);

                if (passwordPolicy.TestPasswordNew(password.Trim()))
                {
                    goodPasswords++;
                }
            }

            Console.WriteLine(goodPasswords);

            Console.ReadLine();



        }
    }
}
