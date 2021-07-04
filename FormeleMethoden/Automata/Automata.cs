using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Automata
{
    public class Automata<T> where T: IComparable<T>
    {
        private SortedSet<Transition<T>> transitions;

        private SortedSet<T> states;
        private SortedSet<T> startStates;
        private SortedSet<T> finalStates;
        private SortedSet<char> symbols;

        public Automata(): this(new SortedSet<char>())
        { }

        public Automata(char[] s) : this(new SortedSet<char>(s))
        { }

        public IEnumerable<Transition<T>> getTransitions()
        {
            return transitions;
        }

        public SortedSet<T> getStartStates()
        {
            return this.startStates;
        }

        public Automata(SortedSet<char> symbols)
        {
            transitions = new SortedSet<Transition<T>>();
            states = new SortedSet<T>();
            startStates = new SortedSet<T>();
            finalStates = new SortedSet<T>();
            this.setAlphabet(symbols);
        }

        public SortedSet<T> getFinalStates()
        {
            return this.finalStates;
        }

        public void setAlphabet(char[] s)
        {
            this.setAlphabet(new SortedSet<char>(s));
        }

        public void setAlphabet(SortedSet<char> symbols)
        {
            this.symbols = symbols;
        }

        public SortedSet<char> getAlphabet()
        {
            return symbols;
        }

        public void AddTransition(Transition<T> t)
        {
            transitions.Add(t);
            states.Add(t.getFromState());
            states.Add(t.getToState());
        }

        public void defineAsStartState(T t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            startStates.Add(t);
        }

        public void defineAsFinalState(T t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            finalStates.Add(t);
        }

        public void printTransitions()
        {

            foreach (Transition<T> t in transitions)
            {
                System.Diagnostics.Debug.WriteLine(t.ToString());
            }
        }

        /*public Boolean isDFA()
        {
            Boolean isDFA = true;

            foreach (T state in states)
            {
                foreach (char symbol in symbols)
                {
                    isDFA = isDFA && getToStates(from, symbol).size() == 1; //Todo
                }
            }

            return isDFA;
        }*/

        
    }
}
