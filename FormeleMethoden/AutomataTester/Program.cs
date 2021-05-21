using System;
using Automata;

namespace AutomataTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var automata1 = Automata.TestAutomata.getExampleSlide8Lesson2();
            var automata2 = Automata.TestAutomata.getExampleSlide14Lesson2();

            automata1.printTransitions();
            automata2.printTransitions();

        }
    }
}
