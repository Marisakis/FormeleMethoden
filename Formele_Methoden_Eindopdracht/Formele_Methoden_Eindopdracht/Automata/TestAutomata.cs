using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    class TestAutomata
    {
        #region REGEX_AUTOMATA

        public static Automata RegexAAA()
        {
            Automata automata = RegexTranslator.TranslateRegex("aaa");
            automata.name = "aaa";
            return automata;
        }

        public static Automata RegexAAB()
        {
            Automata automata = RegexTranslator.TranslateRegex("aab");
            automata.name = "aab";
            return automata;
        }

        public static Automata RegexABC()
        {
            Automata automata = RegexTranslator.TranslateRegex("abc");
            automata.name = "abc";
            return automata;
        }

        public static Automata RegexAorB()
        {
            Automata automata = RegexTranslator.TranslateRegex("a|b");
            automata.name = "a|b";
            return automata;
        }

        public static Automata RegexAloop()
        {
            Automata automata = RegexTranslator.TranslateRegex("a*");
            automata.name = "a*";
            return automata;
        }

        #endregion

        #region DFA_AUTOMATA

        public static Automata AOneOrMoreBOnOrMore_DFA()
        {
            Automata automata = new Automata(new List<char>() { 'a', 'b' });

            automata.AddStartState("1");
            automata.AddIntermediateState("2");
            automata.AddEndState("3");

            automata.AddTransition('a', "1", "2");
            automata.AddTransition('b', "1", "1");
            automata.AddTransition('b', "2", "3");
            automata.AddTransition('a', "2", "2");
            automata.AddTransition('a', "3", "3");
            automata.AddTransition('b', "3", "3");

            automata.Validate();
            return automata;
        }

        public static Automata StartsWith_ab_Endswith_ba_DFA()
        {
            Automata automata = new Automata(new List<char>() { 'a', 'b' });

            automata.AddStartState("1");
            automata.AddIntermediateState("2");
            automata.AddIntermediateState("3");
            automata.AddEndState("4");
            automata.AddIntermediateState("Fuik");

            automata.AddTransition('a', "1", "2");
            automata.AddTransition('b', "1", "Fuik");

            automata.AddTransition('a', "2", "Fuik");
            automata.AddTransition('b', "2", "3");

            automata.AddTransition('a', "3", "4");
            automata.AddTransition('b', "3", "3");

            automata.AddTransition('a', "4", "2");
            automata.AddTransition('b', "4", "3");

            automata.AddTransition('a', "Fuik", "Fuik");
            automata.AddTransition('b', "Fuik", "Fuik");

            automata.Validate();
            return automata;
        }

        #endregion

        #region NDFA_AUTOMATA

        public static Automata ab_no_epsilon_NDFA()
        {
            Automata automata = new Automata(new List<char>() { 'a', 'b' });

            automata.AddStartState("0");
            automata.AddIntermediateState("1");
            automata.AddEndState("2");

            automata.AddTransition('a', "0", "1");
            automata.AddTransition('b', "1", "2");

            automata.Validate();
            return automata;
        }

        public static Automata ba_bba_no_epsilon_NDFA()
        {
            Automata automata = new Automata(new List<char>() { 'a', 'b' });

            automata.AddStartState("0");
            automata.AddIntermediateState("1");
            automata.AddIntermediateState("3");
            automata.AddEndState("2");
            automata.AddEndState("4");

            automata.AddTransition('b', "0", "1");
            automata.AddTransition('a', "1", "2");
            automata.AddTransition('b', "1", "3");
            automata.AddTransition('a', "3", "4");

            automata.Validate();
            return automata;
        }

        public static Automata ab_aab_aaa_with_epsilon_NDFA()
        {
            Automata automata = new Automata(new List<char>() { 'a', 'b' });

            automata.AddStartState("0");
            automata.AddIntermediateState("1");
            automata.AddIntermediateState("2");
            automata.AddIntermediateState("4");
            automata.AddIntermediateState("5");
            automata.AddIntermediateState("7");
            automata.AddIntermediateState("9");
            automata.AddEndState("3");
            automata.AddEndState("6");
            automata.AddEndState("8");

            automata.AddTransition('a', "0", "1");
            automata.AddTransition("1", "2");
            automata.AddTransition('b', "2", "3");

            automata.AddTransition("1", "4");
            automata.AddTransition('a', "4", "5");
            automata.AddTransition('b', "5", "6");

            automata.AddTransition('a', "5", "7");
            automata.AddTransition("7", "8");

            automata.Validate();
            return automata;
        }

        public static Automata b_bba_with_epsilon_NDFA()
        {
            Automata automata = new Automata(new List<char>() { 'a', 'b' });

            automata.AddStartState("0");
            automata.AddIntermediateState("1");
            automata.AddIntermediateState("3");
            automata.AddEndState("2");
            automata.AddEndState("4");

            automata.AddTransition('b', "0", "1");
            automata.AddTransition("1", "2");
            automata.AddTransition('b', "1", "3");
            automata.AddTransition('a', "3", "4");

            automata.Validate();
            return automata;
        }

        #endregion
    }
}
