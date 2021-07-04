using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    static class AutomataBuilder
    {
        public static Automata StartsWithDFA(string text, List<char> symbols)
        {
            Automata automata = new Automata(symbols);

            if (text.Length > 1)
            {
                automata.AddStartState("0");

                for (int i = 1; i <= text.Length; i++)
                {
                    if((i == text.Length))
                        automata.AddEndState(i.ToString());
                    else
                        automata.AddIntermediateState(i.ToString());
                    automata.AddTransition(text[i - 1], (i - 1).ToString(), i.ToString());
                }

                automata.AddIntermediateState("Fuik");
                automata.GetState("Fuik").AddMissingSymbolTransitions(symbols, automata.GetState("Fuik"));

                for (int i = 0; i < text.Length; i++)
                    automata.GetState(i.ToString()).AddMissingSymbolTransitions(symbols, automata.GetState("Fuik"));

                automata.GetState(text.Length.ToString()).AddMissingSymbolTransitions(symbols, automata.GetState(text.Length.ToString()));
            }

            automata.Validate();
            return automata;
        }

        public static Automata EndsWithDFA(string text, List<char> symbols)
        {
            Automata automata = new Automata(symbols);

            if (text.Length > 1)
            {
                automata.AddStartState("0");

                for (int i = 1; i <= text.Length; i++)
                {
                    if ((i == text.Length))
                        automata.AddEndState(i.ToString());
                    else
                        automata.AddIntermediateState(i.ToString());
                    automata.AddTransition(text[i - 1], (i - 1).ToString(), i.ToString());
                }

                string processedText = "";
                for (int i = 0; i <= text.Length; i++)
                {
                    foreach (char symbol in symbols)
                    {
                        if ((i == text.Length) || (symbol != text[i]))
                        {
                            int returnIndex = GetIndexEqualsExtens(text, processedText + symbol);
                            automata.AddTransition(symbol, i.ToString(), returnIndex.ToString());
                        }
                    }

                    if(i < text.Length)
                        processedText += text[i];
                }
            }

            automata.Validate();
            return automata;
        }

        public static Automata ContainsDFA(string text, List<char> symbols)
        {
            Automata automata = new Automata(symbols);

            if (text.Length > 1)
            {
                automata.AddStartState("0");

                for (int i = 1; i <= text.Length; i++)
                {
                    if ((i == text.Length))
                        automata.AddEndState(i.ToString());
                    else
                        automata.AddIntermediateState(i.ToString());
                    automata.AddTransition(text[i - 1], (i - 1).ToString(), i.ToString());
                }

                string processedText = "";
                for (int i = 0; i < text.Length; i++)
                {
                    foreach (char symbol in symbols)
                    {
                        if (symbol != text[i])
                        {
                            int returnIndex = GetIndexEqualsExtens(text, processedText + symbol);
                            automata.AddTransition(symbol, i.ToString(), returnIndex.ToString());
                        }
                    }

                    processedText += text[i];
                }

                automata.AddMissingSymbolTransitions(text.Length.ToString(), text.Length.ToString());
            }

            automata.Validate();
            return automata;
        }

        public static Automata EvenNumberOfCharacters(char character, List<char> symbols)
        {
            Automata automata = new Automata(symbols);

            automata.AddStartAndEndState("1");
            automata.AddIntermediateState("2");

            automata.AddTransition(character, "1", "2");
            automata.AddTransition(character, "2", "1");

            automata.AddMissingSymbolTransitions("1", "1");
            automata.AddMissingSymbolTransitions("2", "2");

            automata.Validate();
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

            automata.Validate();
            return automata;
        }

        public static int GetIndexEqualsExtens(string text, string processedText)
        {
            int length = Math.Min(text.Length, processedText.Length);

            for (int i = length; i >= 0; i--)
            {
                string textSubString = text.Substring(0, i);
                string processedTextSubString = processedText.Substring(processedText.Length - i, i);
                if (textSubString == processedTextSubString)
                    return i;
            }

            return 0;
        }
    }
}
