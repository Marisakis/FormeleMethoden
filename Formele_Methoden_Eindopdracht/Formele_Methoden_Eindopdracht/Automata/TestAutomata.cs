using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    class TestAutomata
    {
        public static Automata EvenNumberOfCharacters(char character, List<char> symbols)
        {
            Automata automata = new Automata(symbols);

            automata.AddStartAndEndState("1");
            automata.AddIntermediateState("2");

            automata.AddTransition(character, "1", "2");
            automata.AddTransition(character, "2", "1");

            automata.AddMissingSymbolTransitions("1", "1");
            automata.AddMissingSymbolTransitions("2", "2");

            return automata;
        }

        public static Automata UnevenNumberOfCharacters(char character, List<char> symbols)
        {
            Automata automata = new Automata(symbols);

            automata.AddStartState("1");
            automata.AddEndState("2");

            automata.AddTransition(character, "1", "2");
            automata.AddTransition(character, "2", "1");

            automata.AddMissingSymbolTransitions("1", "1");
            automata.AddMissingSymbolTransitions("2", "2");

            return automata;
        }
    }
}
