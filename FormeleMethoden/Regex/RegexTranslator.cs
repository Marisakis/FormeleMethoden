using System;
using Automata;

namespace Regex
{

    public class RegexTranslator
    {
      
        public static Automata<String> translateRegex(String input)
        {
            var fromState = "S";
            var toState = "F";
            var blackbox = new BlackBox<String>(fromState, input, toState);
            var transitions = BlackBox2<String>.GenerateTransitions(fromState, input, toState);
            var automata = new Automata<String>();
            foreach(Transition<String> t in transitions)
            {
                automata.AddTransition(t);
            }
            automata.defineAsStartState(fromState);
            automata.defineAsFinalState(toState);
            //automata.generateAlphabet() // TODO
            return automata;
        }
    }
}
