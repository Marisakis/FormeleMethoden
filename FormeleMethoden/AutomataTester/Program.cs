using System;
using Automata;
using Regex;

namespace AutomataTester
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello World!");
            var automata1 = Automata.TestAutomata.getExampleSlide8Lesson2();
            var automata2 = Automata.TestAutomata.getExampleSlide14Lesson2();

            automata1.printTransitions();
            automata2.printTransitions();*/

            var automata = RegexTranslator.translateRegex("(b)|a");
            automata.printTransitions();

        }
    }
}
