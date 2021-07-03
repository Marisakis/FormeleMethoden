using System;
using System.Collections.Generic;

namespace Automata
{

    public class Transition<T> : IComparable<Transition<T>> where T : IComparable<T>
    {
        public const char EPSILON = 'ε'; 


        private T fromState;
        private char symbol;
        private T toState;

        //TODO: implement overloads
        // this constructor can be used to define loops:
        public Transition(T fromOrTo, char s) : this(fromOrTo, s, fromOrTo) //Loop symbol transition
        { }
        public Transition(T from, T to) : this(from, EPSILON, to)//Epsilon transitions
        { }


        public Transition(T from, char s, T to) //Symbol transitions
        {
            this.fromState = from;
            this.symbol = s;
            this.toState = to;
        }


        // overriding equals
        public Boolean equals(Object other)
        {
            if (other == null)
            {
                return false;
            }
            else if (other.GetType().Equals(typeof(Transition<T>)))
            {
                return this.fromState.Equals(((Transition<T>)other).fromState) && this.toState.Equals(((Transition<T>)other).toState) && this.symbol == (((Transition<T>)other).symbol);
            }
            else return false;
        }

        //@SuppressWarnings("unchecked")
        public int CompareTo(Transition<T> t)
        {
            int fromCmp = fromState.CompareTo(t.fromState);
            int symbolCmp = symbol.CompareTo(t.symbol);
            int toCmp = toState.CompareTo(t.toState);

            return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
        }

        public T getFromState()
        {
            return fromState;
        }

        public T getToState()
        {
            return toState;
        }

        public char getSymbol()
        {
            return symbol;
        }

        public String ToString()
        {
            return "(" + this.getFromState() + ", " + this.getSymbol() + ")" + "-->" + this.getToState();
        }

    }
}

