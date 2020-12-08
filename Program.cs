using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AOC_Day7
{

    class Colour
    {
        public readonly string _colourName;

        public Colour(string colourName)
        {

            this._colourName = colourName;

        }

        public override string ToString()
        {
            return _colourName;
        }
    }

    class Bag
    {
        public readonly Colour colour;

        List<Bag> containsBags = new List<Bag>();

        public int IndirectlyContains()
        {
            if (containsBags.Count < 1) return 0;

            int total = containsBags.Count;

            foreach (Bag bag in containsBags)
            {
                total = total + bag.IndirectlyContains();

            }

            return total;
        }


        public bool CanIndirectlyContain(string _colour)
        {

            if (CanContain(_colour)) return true;

            foreach (Bag bag in containsBags)
            {
                if (bag.CanIndirectlyContain(_colour)) return true;
            }

            return false;
        }

        private bool CanContain(string _colour)
        {
            foreach (Bag bag in containsBags)
            {
                if (bag.colour.ToString() == _colour) return true;
            }

            return false;
        }

        public Bag(Colour _colour)
        {
            colour = _colour;
        }

        public void AddBag(Bag bag)
        {
            containsBags.Add(bag);

        }

        public override string ToString()
        {
            return colour.ToString();
        }

    }

    class Rule
    {
        public readonly Bag subject;

        public readonly int quantity;
        public readonly Bag contains;

        public Rule(Bag _subject, Bag _contains, int _quantity)
        {
            subject = _subject;
            quantity = _quantity;
            contains = _contains;
        }

    }

    static class Decode
    {
        public static List<Colour> EnumerateColours(string[] input)
        {

            List<Colour> colourList = new List<Colour>();

            foreach (string line in input)
            {
                colourList.Add(new Colour(GetSubjectColourFromLine(line)));
            }

            return colourList;

        }

        public static void EnumerateContainingBags(Dictionary<string, Bag> bagDictionary, string[] input)
        {
            List<Rule> allRules = new List<Rule>();
 
            foreach (string line in input)
            {
                var lineRules = CreateRulesFromLine(bagDictionary, line);
                allRules.AddRange(lineRules);
            }


            foreach (Rule rule in allRules)
            {

             //   if(rule.quantity > 0) rule.subject.AddBag(rule.contains);

                
                for (int i = 0; i < rule.quantity; i++)
                {
                    rule.subject.AddBag(rule.contains);
                }


            }


        }



        public static List<Rule> CreateRulesFromLine(Dictionary<string, Bag> bagDictionary, string line)
        {
            List<Rule> ruleList = new List<Rule>();

            string subjectColour = GetSubjectColourFromLine(line);
            Bag subjectBag = bagDictionary[subjectColour];


            string[] containingBags = GetContainingBagsFromLine(line);

            // No contained bags
            if (containingBags == null) return ruleList;

            foreach (string strContainingBag in containingBags)
            {

                var result = ReadRule(strContainingBag);

                Bag containedBag = bagDictionary[result.Item2];


                Rule rule = new Rule(subjectBag,containedBag, result.Item1);

                ruleList.Add(rule);

            }

            return ruleList;
        }

        
        public static Tuple<int, string> ReadRule(string rule)
        {
            string regex = "(?<qty>[0-9]) (?<colour>.*) (?<bags>bag)";
            RegexOptions options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            var match = Regex.Match(rule, regex, options);

            int quantity = Convert.ToInt32(match.Groups["qty"].Value);

            if (quantity < 1) throw new Exception();

            string colour = match.Groups["colour"].Value;

            if(string.IsNullOrWhiteSpace(colour)) throw new Exception();


            return new Tuple<int, string>(quantity,colour);
        }

        public static string[] GetContainingBagsFromLine(string line)
        {

            line = line.TrimEnd('.');

            string containDefinition = line.Split(new string[] { " contain " }, StringSplitOptions.None)[1];

            string[] containingBags = containDefinition.Split(',');

            if (containingBags[0] == "no other bags") return null;

            return containingBags;
        }

        public static string GetSubjectColourFromLine(string line)
        {
            string bagDefinition = line.Split(new string[] { " contain " }, StringSplitOptions.None)[0];

            if (string.IsNullOrWhiteSpace(bagDefinition)) throw new Exception();

            string subjectColour = bagDefinition.Split(new string[] { " bags" }, StringSplitOptions.None)[0];

            if (string.IsNullOrWhiteSpace(subjectColour)) throw new Exception();

            return subjectColour;

        }
    }



    class Program
    {
        static void Main(string[] args)
        {

            string needle = "shiny gold";

            Dictionary<string, Bag> bagDictionary = new Dictionary<string, Bag>();

            List<Bag> bagList = new List<Bag>();

            var input = File.ReadAllLines("aoc-day7-input.txt");


            var colours = Decode.EnumerateColours(input);


            // Define all our bags

            foreach (Colour colour in colours)
            {
                Bag bag = new Bag(colour);

                bagDictionary.Add(bag.colour._colourName, bag);

            }

            Console.WriteLine(bagDictionary.Count.ToString() + " bags generated");

            Decode.EnumerateContainingBags(bagDictionary, input);

            int ruleMatch = 0;

            

            foreach (KeyValuePair<string, Bag> bag in bagDictionary)

            {
                Console.WriteLine("Checking " + bag.Value.colour.ToString());


                bool canContainNeedle = bag.Value.CanIndirectlyContain(needle);
                if (canContainNeedle) ruleMatch++;
            }



           int numBags = bagDictionary["shiny gold"].IndirectlyContains();

           Console.WriteLine(numBags);

        }



    }
}
