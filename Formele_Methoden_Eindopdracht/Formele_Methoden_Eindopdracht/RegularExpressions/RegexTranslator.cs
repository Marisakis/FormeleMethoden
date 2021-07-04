using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    public class RegexTranslator
    {
        public static Automata TranslateRegex(string input)
        {
            var fromState = "S";
            var toState = "F";
            List<char> symbols = new List<char>();
            var transitions = BlackBox<string>.GenerateTransitions(fromState, input, toState, ref symbols);

            Automata automata = new Automata(symbols);
            automata.AddStartState(fromState);
            automata.AddEndState(toState);
            foreach (State.StateTransition t in transitions)
            {
                automata.AddIntermediateState(t.PreviousState);
                automata.AddIntermediateState(t.NextState);
            }

            foreach (State.StateTransition t in transitions)
            {
                if (t.IsEpsilon)
                    automata.AddTransition(t.PreviousState, t.NextState);
                else
                    automata.AddTransition(t.Character, t.PreviousState, t.NextState);
            }
            //automata.generateAlphabet() // TODO

            automata.Validate();
            return automata;
        }
    }
}
