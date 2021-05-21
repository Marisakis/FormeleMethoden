using System;
using System.Collections.Generic;
using System.Text;

namespace Automata
{
    public class TestAutomata
    {

        static public Automata<String> getExampleSlide8Lesson2()
        {
            char[] alphabet = { 'a', 'b' };
            Automata<String> m = new Automata<String>(alphabet);

            m.AddTransition(new Transition<String>("q0", 'a', "q1"));
            m.AddTransition(new Transition<String>("q0", 'b', "q4"));

            m.AddTransition(new Transition<String>("q1", 'a', "q4"));
            m.AddTransition(new Transition<String>("q1", 'b', "q2"));

            m.AddTransition(new Transition<String>("q2", 'a', "q3"));
            m.AddTransition(new Transition<String>("q2", 'b', "q4"));

            m.AddTransition(new Transition<String>("q3", 'a', "q1"));
            m.AddTransition(new Transition<String>("q3", 'b', "q2"));

            // the error state, loops for a and b:
            m.AddTransition(new Transition<String>("q4", 'a')); 
            m.AddTransition(new Transition<String>("q4", 'b'));

            // only on start state in a dfa:
            m.defineAsStartState("q0");

            // two final states:
            m.defineAsFinalState("q2");
            m.defineAsFinalState("q3");

            return m;
        }


        static public Automata<String> getExampleSlide14Lesson2()
        {
            char[] alphabet = { 'a', 'b' };
            Automata<String> m = new Automata<String>(alphabet);

            m.AddTransition(new Transition<String>("A", 'a', "C"));
            m.AddTransition(new Transition<String>("A", 'b', "B"));
            m.AddTransition(new Transition<String>("A", 'b', "C"));

            m.AddTransition(new Transition<String>("B", 'b', "C"));
            m.AddTransition(new Transition<String>("B", "C"));

            m.AddTransition(new Transition<String>("C", 'a', "D"));
            m.AddTransition(new Transition<String>("C", 'a', "E"));
            m.AddTransition(new Transition<String>("C", 'b', "D"));

            m.AddTransition(new Transition<String>("D", 'a', "B"));
            m.AddTransition(new Transition<String>("D", 'a', "C"));

            m.AddTransition(new Transition<String>("E", 'a'));
            m.AddTransition(new Transition<String>("E", "D"));

            // only on start state in a dfa:
            m.defineAsStartState("A");

            // two final states:
            m.defineAsFinalState("C");
            m.defineAsFinalState("E");

            return m;
        }

    }
}
