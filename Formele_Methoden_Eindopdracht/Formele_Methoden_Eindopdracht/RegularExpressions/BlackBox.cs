using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    //class BlackBox<T> where T : IComparable<T>
    //{

    //    public static int stateCounter;

    //    public static T getNextState()
    //    {
    //        var nextState = "q" + stateCounter;
    //        stateCounter++;
    //        return (T)(object)nextState;
    //    }

    //    public static ISet<State.StateTransition> GenerateTransitions(T fromState, string input, T toState, ref List<char> symbols)
    //    {
    //        //TODO: Assert input length > 0
    //        //TODO: assert total bracket numbers ( equals )
    //        stateCounter = 1;
    //        var transitions = new SortedSet<State.StateTransition>();
    //        transitions.UnionWith(GenerateSubTransitions(fromState, input, toState));
    //        return transitions;
    //    }

    //    private static ISet<State.StateTransition> GenerateSubTransitions(T fromState, string input, T toState)
    //    {
    //        var transitions = new SortedSet<State.StateTransition>();

    //        if (input.Length == 0) //safeguard
    //        {
    //            var transition = new State.StateTransition(fromState.ToString(), toState.ToString());
    //            transitions.Add(transition);
    //            System.Diagnostics.Debug.WriteLine("empty input found");
    //            return transitions;
    //        }
    //        else if (input.Length == 1) //end condition: string contains no more operators, only a character
    //        {

    //            var character = input[0];
    //            var transition = new State.StateTransition(character, fromState.ToString(), toState.ToString());
    //            transitions.Add(transition);
    //            return transitions;
    //        }
    //        else
    //        {

    //            //check for |
    //            foreach (char c in input)
    //            {
    //                if (c == '|')
    //                {
    //                    var orposition = input.IndexOf('|');
    //                    var frontstring = "";
    //                    var endstring = "";
    //                    if (orposition > 0)
    //                        frontstring = input.Substring(0, orposition);
    //                    if (orposition + 1 < input.Length)
    //                        endstring = input.Substring(orposition + 1, input.Length - 1 - orposition);
    //                    var state1 = getNextState();
    //                    var state2 = getNextState();
    //                    var state3 = getNextState();
    //                    var state4 = getNextState();

    //                    transitions.Add(new State.StateTransition(fromState.ToString(), state1.ToString()));
    //                    transitions.Add(new State.StateTransition(fromState.ToString(), state2.ToString()));
    //                    transitions.Add(new State.StateTransition(state3.ToString(), toState.ToString()));
    //                    transitions.Add(new State.StateTransition(state4.ToString(), toState.ToString()));
    //                    transitions.UnionWith(GenerateSubTransitions(state1, frontstring, state3));
    //                    transitions.UnionWith(GenerateSubTransitions(state2, endstring, state4));

    //                    return transitions;
    //                }
    //            }

    //            //check for ()
    //            foreach (char c in input)
    //            {
    //                if (c == '(')
    //                {
    //                    var firstbracket = input.IndexOf('(');
    //                    var lastbracket = input.LastIndexOf(')');
    //                    //determine substrings
    //                    var frontstring = "";
    //                    var endstring = "";
    //                    if (firstbracket > 0)
    //                        frontstring = input.Substring(0, firstbracket);
    //                    var bracketstring = input.Substring(firstbracket + 1, lastbracket - firstbracket - 1);
    //                    if (lastbracket + 1 < input.Length)
    //                        endstring = input.Substring(lastbracket + 1, input.Length - 1 - lastbracket);
    //                    // assign transitions between substrings
    //                    var state1 = getNextState();
    //                    var state2 = getNextState();
    //                    transitions.UnionWith(GenerateSubTransitions(fromState, frontstring, state1));
    //                    transitions.UnionWith(GenerateSubTransitions(state1, bracketstring, state2));
    //                    transitions.UnionWith(GenerateSubTransitions(state2, endstring, toState));
    //                    return transitions;
    //                    break;
    //                }
    //            }

    //            //check for rest of operators: ε, +, *, . and only characters
    //            var operation = input[1];
    //            var symbol = input[0];

    //            switch (operation)
    //            {
    //                case 'ε': //handled the same as a character because the symbol for that is the same
    //                    goto default;
    //                    break;
    //                case '+':
    //                    {
    //                        var front = getNextState();
    //                        var back = getNextState();
    //                        transitions.Add(new State.StateTransition(fromState.ToString(), front.ToString()));
    //                        transitions.Add(new State.StateTransition(back.ToString(), toState.ToString()));
    //                        transitions.Add(new State.StateTransition(back.ToString(), front.ToString()));
    //                        transitions.Add(new State.StateTransition(symbol, front.ToString(), back.ToString()));
    //                        transitions.UnionWith(GenerateSubTransitions(front, input.Substring(2), back));
    //                        break;
    //                    }
    //                case '*':
    //                    {
    //                        var front = getNextState();
    //                        var back = getNextState();
    //                        transitions.Add(new State.StateTransition(fromState.ToString(), front.ToString()));
    //                        transitions.Add(new State.StateTransition(back.ToString(), toState.ToString()));
    //                        transitions.Add(new State.StateTransition(back.ToString(), front.ToString()));
    //                        transitions.Add(new State.StateTransition(symbol, front.ToString(), back.ToString()));
    //                        transitions.Add(new State.StateTransition(fromState.ToString(), toState.ToString()));
    //                        transitions.UnionWith(GenerateSubTransitions(front, input.Substring(2), back));
    //                        break;
    //                    }
    //                    break;
    //                case '.':
    //                    {
    //                        var nextState = getNextState();
    //                        transitions.Add(new State.StateTransition(symbol, fromState.ToString(), nextState.ToString()));
    //                        transitions.UnionWith(GenerateSubTransitions((T)(object)nextState, input.Substring(2), toState));
    //                        break;
    //                    }
    //                default: //next character not an operator
    //                    {
    //                        var nextState = getNextState();
    //                        transitions.Add(new State.StateTransition(symbol, fromState.ToString(), nextState.ToString()));
    //                        transitions.UnionWith(GenerateSubTransitions((T)(object)nextState, input.Substring(1), toState));
    //                        break;
    //                    }
    //            }
    //            return transitions;
    //        }
    //    }


    //}

    class BlackBox<T> where T : IComparable<T>
    {
        public static int stateCounter;

        public static string GetNextState()
        {
            string nextState = "q" + stateCounter;
            stateCounter++;
            return nextState;
        }

        public static ISet<State.StateTransition> GenerateTransitions(string fromState, string input, string toState, ref List<char> symbols)
        {
            //TODO: Assert input length > 0
            //TODO: assert total bracket numbers ( equals )
            stateCounter = 1;
            var transitions = new SortedSet<State.StateTransition>();
            transitions.UnionWith(GenerateSubTransitions(fromState, input, toState));
            symbols.Distinct();
            return transitions;
        }

        private static ISet<State.StateTransition> GenerateSubTransitions(string fromState, string input, string toState)
        {
            var transitions = new SortedSet<State.StateTransition>();

            if (input.Length == 0) //safeguard
            {
                var transition = new State.StateTransition(fromState, toState);
                transitions.Add(transition);
                System.Diagnostics.Debug.WriteLine("empty input found");
                return transitions;
            }
            else if (input.Length == 1) //end condition: string contains no more operators, only a character
            {

                var character = input[0];
                var transition = new State.StateTransition(character, fromState, toState);
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
                        int orposition = input.IndexOf('|');
                        string frontstring = "";
                        string endstring = "";
                        if (orposition > 0)
                            frontstring = input.Substring(0, orposition);
                        if (orposition + 1 < input.Length)
                            endstring = input.Substring(orposition + 1, input.Length - 1 - orposition);
                        string state1 = GetNextState();
                        string state2 = GetNextState();
                        string state3 = GetNextState();
                        string state4 = GetNextState();

                        transitions.Add(new State.StateTransition(fromState, state1));
                        transitions.Add(new State.StateTransition(fromState, state2));
                        transitions.Add(new State.StateTransition(state3, toState));
                        transitions.Add(new State.StateTransition(state4, toState));
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
                        int firstbracket = input.IndexOf('(');
                        int lastbracket = input.LastIndexOf(')');
                        //determine substrings
                        string frontstring = "";
                        string endstring = "";
                        if (firstbracket > 0)
                            frontstring = input.Substring(0, firstbracket);
                        string bracketstring = input.Substring(firstbracket + 1, lastbracket - firstbracket - 1);
                        if (lastbracket + 1 < input.Length)
                            endstring = input.Substring(lastbracket + 1, input.Length - 1 - lastbracket);
                        // assign transitions between substrings
                        string state1 = GetNextState();
                        string state2 = GetNextState();
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
                            string front = GetNextState();
                            string back = GetNextState();
                            transitions.Add(new State.StateTransition(fromState, front));
                            transitions.Add(new State.StateTransition(back, toState));
                            transitions.Add(new State.StateTransition(back, front));
                            transitions.Add(new State.StateTransition(symbol, front, back));
                            transitions.UnionWith(GenerateSubTransitions(front, input.Substring(2), back));
                            break;
                        }
                    case '*':
                        {
                            string front = GetNextState();
                            string back = GetNextState();
                            transitions.Add(new State.StateTransition(fromState, front));
                            transitions.Add(new State.StateTransition(back, toState));
                            transitions.Add(new State.StateTransition(back, front));
                            transitions.Add(new State.StateTransition(symbol, front, back));
                            transitions.Add(new State.StateTransition(fromState, toState));
                            transitions.UnionWith(GenerateSubTransitions(front, input.Substring(2), back));
                            break;
                        }
                        break;
                    case '.':
                        {
                            string nextState = GetNextState();
                            transitions.Add(new State.StateTransition(symbol, fromState, nextState));
                            transitions.UnionWith(GenerateSubTransitions(nextState, input.Substring(2), toState));
                            break;
                        }
                    default: //next character not an operator
                        {
                            string nextState = GetNextState();
                            transitions.Add(new State.StateTransition(symbol, fromState, nextState));
                            transitions.UnionWith(GenerateSubTransitions(nextState, input.Substring(1), toState));
                            break;
                        }
                }
                return transitions;
            }
        }
    }
}
