using System;
using System.Collections.Generic;
using System.Text;
using Automata;

namespace Regex
{
    class BlackBox<T> where T : IComparable<T>
    {

        //public static char[] operators = { 'ε', '*', '+', '(', ')', '.', '|' };
        //Order of operations: (), 


        /*private T fromState;
        private char symbol;
        private T toState;*/


        //public static int nextCharacter;
        public static int stateCounter;

        /*public BlackBox(T fromState, string input, T toState)
        {
            this.fromState = fromState;
            var regex = input;
            this.toState = toState;






        }*/

        public static T getNextState()
        {
            var nextState = "q" + stateCounter;
            stateCounter++;
            return (T)(object)nextState;
        }

        public static ISet<Transition<T>> GenerateTransitions(T fromState, string input, T toState)
        {
            //TODO: Assert input length > 0
            //TODO: assert total bracket numbers ( equals )
           stateCounter = 1;
           var transitions = new SortedSet<Transition<T>>();
           //var inputarray = input.ToCharArray();
           transitions.UnionWith(GenerateSubTransitions(fromState, input, toState));
           return transitions;
        }

        private static ISet<Transition<T>> GenerateSubTransitions(T fromState, string input, T toState)
        {
            var transitions = new SortedSet<Transition<T>>();
            //

            if (input.Length == 0) //safeguard
            {
                var transition =  new Transition<T>(fromState, toState); 
                transitions.Add(transition);
                System.Diagnostics.Debug.WriteLine("empty input found");
                return transitions;
            }
            else if (input.Length == 1) //end condition: string contains no more operators, only a character
            {
                
                var character = input[0];
                var transition = new Transition<T>(fromState, character, toState);
                transitions.Add(transition);
                return transitions;
            }
            else
            {

                //check for |
                foreach (char c in input)
                {
                    if (c == '|')
                    {
                        var orposition = input.IndexOf('|');
                        var frontstring = "";
                        var endstring = "";
                        if (orposition > 0)
                            frontstring = input.Substring(0, orposition);
                        if (orposition + 1 < input.Length)
                            endstring = input.Substring(orposition + 1, input.Length - 1 - orposition);
                        var state1 = getNextState();
                        var state2 = getNextState();
                        var state3 = getNextState();
                        var state4 = getNextState();

                        transitions.Add(new Transition<T>(fromState, state1));
                        transitions.Add(new Transition<T>(fromState, state2));
                        transitions.Add(new Transition<T>(state3, toState));
                        transitions.Add(new Transition<T>(state4, toState));
                        transitions.UnionWith(GenerateSubTransitions(state1, frontstring, state3));
                        transitions.UnionWith(GenerateSubTransitions(state2, endstring, state4));

                        return transitions;
                    }
                }


                //check for ()
                foreach (char c in input)
                {
                    if (c == '(')
                    {
                        var firstbracket = input.IndexOf('(');
                        var lastbracket = input.LastIndexOf(')');
                        //determine substrings
                        var frontstring = "";
                        var endstring = "";
                        if (firstbracket > 0)
                            frontstring = input.Substring(0, firstbracket);
                        var bracketstring = input.Substring(firstbracket + 1, lastbracket - firstbracket -1);
                        if (lastbracket + 1 < input.Length)
                            endstring = input.Substring(lastbracket +1, input.Length - 1 - lastbracket );
                        // assign transitions between substrings
                        var state1 = getNextState();
                        var state2 = getNextState();
                        transitions.UnionWith(GenerateSubTransitions(fromState, frontstring, state1));
                        transitions.UnionWith(GenerateSubTransitions(state1, bracketstring, state2));
                        transitions.UnionWith(GenerateSubTransitions(state2, endstring, toState));
                        return transitions;
                        break;
                    }
                }


                




                //check for rest of operators: ε, +, *, . and only characters
                var operation = input[1];
                var symbol = input[0];

                switch (operation)
                {
                    case 'ε': //handled the same as a character because the symbol for that is the same
                        goto default;
                        break;
                    case '+':
                        {
                            var front = getNextState();
                            var back = getNextState();
                            transitions.Add(new Transition<T>(fromState, front));
                            transitions.Add(new Transition<T>(back, toState));
                            transitions.Add(new Transition<T>(back, front));
                            transitions.Add(new Transition<T>(front, symbol, back));
                            transitions.UnionWith(GenerateSubTransitions(front, input.Substring(2), back));
                            break;
                        }
                    case '*':
                        {
                            var front = getNextState();
                            var back = getNextState();
                            transitions.Add(new Transition<T>(fromState, front));
                            transitions.Add(new Transition<T>(back, toState));
                            transitions.Add(new Transition<T>(back, front));
                            transitions.Add(new Transition<T>(front, symbol, back));
                            transitions.Add(new Transition<T>(fromState, toState));
                            transitions.UnionWith(GenerateSubTransitions(front, input.Substring(2), back));
                            break;
                        }
                    case '(':
                        //difficult
                       
                        break;
                    case ')':
                        //should not happen
                        break;
                    case '|':
                        
                        break;
                    case '.':
                        {
                            var nextState = getNextState();
                            transitions.Add(new Transition<T>(fromState, symbol, (T)(object)nextState));
                            transitions.UnionWith(GenerateSubTransitions((T)(object)nextState, input.Substring(2), toState));
                            break;
                        }
                    default: //next character not an operator
                        {
                            var nextState = getNextState();
                            transitions.Add(new Transition<T>(fromState, symbol, (T)(object)nextState));
                            transitions.UnionWith(GenerateSubTransitions((T)(object)nextState, input.Substring(1), toState));
                            break;
                        }
                }
                return transitions;
            }
        }

       
    }
}
