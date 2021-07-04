using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    public class State
    {
        public class StateTransition : IComparable<StateTransition>
        {
            public char Character;
            public string PreviousState;
            public string NextState;
            public bool IsEpsilon;

            public StateTransition(char character, string previousState, string nextState)
            {
                this.Character = character;
                this.PreviousState = previousState;
                this.NextState = nextState;
                this.IsEpsilon = false;
            }

            public StateTransition(string previousState, string nextState)
            {
                this.Character = 'ε';
                this.PreviousState = previousState;
                this.NextState = nextState;
                this.IsEpsilon = true;
            }

            public int CompareTo(StateTransition other)
            {
                if (((int)this.Character) < ((int)other.Character))
                    return -1;
                else if (((int)this.Character) > ((int)other.Character))
                    return 1;
                else
                    return 0;
            }
        }

        public enum StateType
        { 
            START_STATE,
            END_STATE,
            START_AND_END_STATE,
            INTERMEDIATE_STATE
        }

        public readonly string Name;
        public StateType stateType;
        private List<Transition> transitions;

        public bool IsFuik { get { return this.isFuik; } }
        private bool isFuik;

        public bool HasNDFATransitions { get { return this.hasNDFATransitions; } }
        private bool hasNDFATransitions;

        public State(string name, StateType stateType = StateType.INTERMEDIATE_STATE)
        {
            this.Name = name;
            this.stateType = stateType;
            this.transitions = new List<Transition>();

            this.isFuik = true;
            this.hasNDFATransitions = false;
        }

        public void AddTransition(char character, State nextState)
        {
            if (HasTransition(character))
                this.hasNDFATransitions = true;

            this.transitions.Add(new Transition(character, this, nextState));

            CheckFuik();
        }

        public void AddTransition(State nextState)
        {
            this.transitions.Add(new Transition(this, nextState));
            this.hasNDFATransitions = true;

            CheckFuik();
        }

        public void RemoveTransitions()
        {
            this.transitions.Clear();

            this.isFuik = true;
            this.hasNDFATransitions = false;
        }

        public void AddMissingSymbolTransitions(List<char> symbols, State nextState)
        {
            List<char> missingSymbols = symbols.Distinct().ToList();
            for (int i = 0; i < this.transitions.Count; i++)
                if (missingSymbols.Contains(this.transitions[i].Character))
                    missingSymbols.Remove(this.transitions[i].Character);

            for (int i = 0; i < missingSymbols.Count; i++)
                AddTransition(missingSymbols[i], nextState);
        }

        private void CheckFuik()
        {
            for (int i = 0; i < this.transitions.Count; i++)
            {
                if (this.transitions[i].NextState != this)
                {
                    this.isFuik = false;
                    return;
                }
            }

            this.isFuik = true;
        }

        public State EvaluateTransitions(char inputCharacter)
        {
            if(!this.hasNDFATransitions)
            {
                for (int i = 0; i < this.transitions.Count; i++)
                    if (this.transitions[i].EvaluateTransition(inputCharacter))
                        return this.transitions[i].NextState;
            }

            return this;
        }

        public bool HasTransition(char character)
        {
            for (int i = 0; i < this.transitions.Count; i++)
                if (this.transitions[i].Character == character)
                    return true;

            return false;
        }

        public bool HasSymbolTransitions(List<char> symbols)
        {
            List<char> encounteredSymbols = new List<char>();
            for (int i = 0; i < this.transitions.Count; i++)
            {
                if (!symbols.Contains(this.transitions[i].Character))
                    return false;
                else if (!encounteredSymbols.Contains(this.transitions[i].Character))
                    encounteredSymbols.Add(this.transitions[i].Character);
            }       

            return encounteredSymbols.Count == symbols.Count;
        }

        public List<StateTransition> GetStateTransitions()
        {
            List<StateTransition> symbolTransitions = new List<StateTransition>();
       
            for(int i = 0; i < this.transitions.Count; i++)
            {
                if(!this.transitions[i].IsEpsilon)
                    symbolTransitions.Add(new StateTransition(this.transitions[i].Character, this.transitions[i].PreviousState.Name, this.transitions[i].NextState.Name));
                else
                    symbolTransitions.Add(new StateTransition(this.transitions[i].PreviousState.Name, this.transitions[i].NextState.Name));
            }

            return symbolTransitions;
        }

        public StateTransition GetStateTransitionForSymbol(char symbol)
        {
            if (this.transitions.Where(m => m.Character == symbol).Count() > 0)
            {
                Transition transition = this.transitions.Where(m => m.Character == symbol).First();
                return new StateTransition(transition.Character, transition.PreviousState.Name, transition.NextState.Name);
            }

            return null;
        }

        public List<StateTransition> GetResolvedStateTransitions(List<char> symbols)
        {
            List<StateTransition> symbolTransitions = new List<StateTransition>();

            for (int i = 0; i < this.transitions.Count; i++)
            {
                foreach (char symbol in symbols)
                {
                    List<string> stateNames = new List<string>();
                    //EvaluateEpsilonClosure(symbol, !this.transitions[i].IsEpsilon, this.transitions[i].NextState, this.transitions[i].PreviousState.Name, ref stateNames);
                    EvaluateEpsilonClosure(symbol, this.transitions[i].PreviousState, ref stateNames, new List<string>());

                    List<string> closureStates = stateNames.Distinct().ToList();
                    closureStates.Sort();

                    //foreach (string stateName in closureStates)
                    //    symbolTransitions.Add(new StateTransition(this.transitions[i].Character, this.transitions[i].PreviousState.Name, stateName));

                    foreach (string stateName in closureStates)
                        symbolTransitions.Add(new StateTransition(symbol, this.transitions[i].PreviousState.Name, stateName));
                }
            }

            return symbolTransitions;
        }

        private void EvaluateEpsilonClosure(char symbol, State currentState, ref List<string> stateNames, List<string> epsilonStates, string startState = "", bool symbolEncountered = false)
        {
            if (startState == "")
                startState = currentState.Name;
            else if (currentState.Name == startState)
                return;

            for (int i = 0; i < currentState.transitions.Count; i++)
            {
                if (currentState.transitions[i].Character == symbol)
                {
                    if (symbolEncountered)
                        continue;
                    else
                    {
                        symbolEncountered = true;
                        stateNames.Add(currentState.transitions[i].NextState.Name);

                        if(epsilonStates.Count > 0)
                        {
                            foreach (string epsilonStateName in epsilonStates)
                                stateNames.Add(epsilonStateName);
                        }

                        currentState.EvaluateEpsilonClosure(symbol, currentState.transitions[i].NextState, ref stateNames, epsilonStates, startState, symbolEncountered);
                    }
                }
                else if (currentState.transitions[i].IsEpsilon)
                {
                    if (symbolEncountered)
                    {
                        stateNames.Add(currentState.transitions[i].NextState.Name);
                        currentState.EvaluateEpsilonClosure(symbol, currentState.transitions[i].NextState, ref stateNames, epsilonStates, startState, symbolEncountered);
                    }
                    else
                    {
                        epsilonStates.Add(currentState.transitions[i].NextState.Name);
                        currentState.EvaluateEpsilonClosure(symbol, currentState.transitions[i].NextState, ref stateNames, epsilonStates, startState, symbolEncountered);
                    }
                }
                epsilonStates.Clear();
            }
        }

        //private void EvaluateEpsilonClosure(char symbol, bool symbolEncountered, State currentState, string startState, ref List<string> stateNames)
        //{
        //    if (symbolEncountered)
        //        stateNames.Add(currentState.Name);

        //    if (currentState.Name == startState)
        //        return;

        //    for (int i = 0; i < currentState.transitions.Count; i++)
        //    {
        //        if ((currentState.transitions[i].Character == symbol) && symbolEncountered)
        //            continue;
        //        else if((currentState.transitions[i].Character == symbol) || currentState.transitions[i].IsEpsilon)
        //        {
        //            if (currentState.transitions[i].Character == symbol)
        //                symbolEncountered = true;
        //            currentState.EvaluateEpsilonClosure(symbol, symbolEncountered, currentState.transitions[i].NextState, startState, ref stateNames);
        //        }
        //    }
        //}

        public override string ToString()
        {
            string type = "INTERMEDIATE";
            if (this.stateType == StateType.START_STATE)
                type = "START";
            else if (this.stateType == StateType.END_STATE)
                type = "END";
            else if (this.stateType == StateType.START_AND_END_STATE)
                type = "START & END";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("State: (" + this.Name + ") type: (" + type + ")");
            stringBuilder.AppendLine("\tTransitions:");
            for(int i = 0; i < this.transitions.Count; i++)
                stringBuilder.AppendLine("\t(" + this.transitions[i].Character + ") -> state: (" + this.transitions[i].NextState.Name + ")");

            return stringBuilder.ToString();
        }
    }
}
