using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    public class Automata
    {
        public readonly List<char> symbols;
        private Dictionary<string, State> states;
        private State currentState;

        private bool isValid;
        private bool isDFA;
        public string name;

        public bool IsDFA { get { return this.isDFA; } }

        public Automata(List<char> symbols)
        {
            this.symbols = symbols;
            this.states = new Dictionary<string, State>();

            this.isValid = false;
            this.isDFA = false;
        }

        private void AddState(string name, State.StateType stateType = State.StateType.INTERMEDIATE_STATE)
        {
            if(!this.states.ContainsKey(name))
                this.states.Add(name, new State(name, stateType));
        }

        public void AddStartState(string name)
        {
            if (!this.states.ContainsKey(name))
                this.states.Add(name, new State(name, State.StateType.START_STATE));
        }

        public void AddEndState(string name)
        {
            if (!this.states.ContainsKey(name))
                this.states.Add(name, new State(name, State.StateType.END_STATE));
        }

        public void AddStartAndEndState(string name)
        {
            if (!this.states.ContainsKey(name))
                this.states.Add(name, new State(name, State.StateType.START_AND_END_STATE));
        }

        public void AddIntermediateState(string name)
        {
            if (!this.states.ContainsKey(name))
                this.states.Add(name, new State(name, State.StateType.INTERMEDIATE_STATE));
        }

        public State GetState(string name)
        {
            try { return this.states[name]; }
            catch(KeyNotFoundException exeption) { return null; }
        }

        public bool AddTransition(char character, string stateAName, string stateBName)
        {
            if(this.states.ContainsKey(stateAName) && this.states.ContainsKey(stateBName))
            {
                this.states[stateAName].AddTransition(character, this.states[stateBName]);
                this.isValid = false;
                this.isDFA = false;
                return true;
            }

            return false;
        }

        public bool AddTransition(string stateAName, string stateBName)
        {
            if (this.states.ContainsKey(stateAName) && this.states.ContainsKey(stateBName))
            {
                this.states[stateAName].AddTransition(this.states[stateBName]);
                this.isValid = false;
                this.isDFA = false;
                return true;
            }

            return false;
        }

        public void AddMissingSymbolTransitions(string fromState, string toState)
        {
            State stateA = GetState(fromState);
            State stateB = GetState(toState);
            if (stateA != null && stateB != null)
                stateA.AddMissingSymbolTransitions(this.symbols, stateB);
        }

        public bool Evaluate(string input)
        {
            if(CheckInputWithSymbols(input))
            {
                Validate();

                if (this.isValid && this.isDFA)
                {
                    currentState = states.Values.ToArray<State>().Where(m => (m.stateType == State.StateType.START_STATE) || (m.stateType == State.StateType.START_AND_END_STATE)).FirstOrDefault();

                    for (int i = 0; i < input.Length; i++)
                    {
                        currentState = currentState.EvaluateTransitions(input[i]);
                        if (currentState.IsFuik)
                            break;
                    }

                    return ((currentState.stateType == State.StateType.END_STATE) || (currentState.stateType == State.StateType.START_AND_END_STATE));
                }
                else if(!this.isValid)
                    Console.WriteLine("Invalid automata!");
                else if(!this.isDFA)
                    Console.WriteLine("Automata cannot be processed, it's not a DFA!\nConvert NDFA to DFA to process!");
            }
            else
                Console.WriteLine("Invalid symbols in input string!");

            return false;
        }

        public List<State> GetStates()
        {
            return this.states.Values.ToList();
        }

        #region VALIDATION

        public void Validate()
        {
            if(!this.isValid)
            {
                if (this.states.Count < 2)
                {
                    Console.WriteLine("Not enough states in automata!");
                    this.isValid = false;
                    return;
                }

                List<State> stateList = this.states.Values.ToList<State>();
                int startStateCount = stateList.Where(m => (m.stateType == State.StateType.START_STATE) || (m.stateType == State.StateType.START_AND_END_STATE)).Count();
                int endStateCount = stateList.Where(m => (m.stateType == State.StateType.END_STATE) || (m.stateType == State.StateType.START_AND_END_STATE)).Count();
                if(endStateCount == 0 || (startStateCount == 0 || startStateCount > 1))
                {
                    if(endStateCount == 0)
                        Console.WriteLine("No endstate found!");
                    if(startStateCount == 0)
                        Console.WriteLine("No startstate found!");
                    //else if (startStateCount > 1)
                    //    Console.WriteLine("To many start states!");
                    this.isValid = false;
                    return;
                }    

                for (int i = 0; i < stateList.Count; i++)
                {
                    if (!stateList[i].HasSymbolTransitions(this.symbols))
                    {
                        this.isValid = false;
                        return;
                    }
                }

                this.isValid = true;

                CheckDFA();
            }
        }

        private void CheckDFA()
        {
            List<State> stateList = this.states.Values.ToList<State>();
            int startStatesCount = 0;
            for (int i = 0; i < stateList.Count; i++)
            {
                if (stateList[i].stateType == State.StateType.START_STATE)
                {
                    startStatesCount++;
                    if (startStatesCount > 1)
                    {
                        this.isDFA = false;
                        return;
                    }
                }

                if (stateList[i].HasNDFATransitions)
                {
                    this.isDFA = false;
                    return;
                }
            }

            this.isDFA = true;
        }

        private bool CheckInputWithSymbols(string input)
        {
            for (int i = 0; i < input.Length; i++)
                if (!this.symbols.Contains(input[i]))
                    return false;

            return true;
        }

        #endregion

        #region MINIMIZATION

        public Automata Minimize()
        {
            Validate();
            if (this.isValid && this.isDFA)
            {
                AutomataMinimizationTable minimizationTable = new AutomataMinimizationTable(this);
            }
            else if(this.isValid)
                Console.WriteLine("Unable to minimize automata, automata is not valid!");
            else if(!this.isDFA)
                Console.WriteLine("Unable to minimize automata, automata is not a DFA!");

            return null;
        }

        public Automata Minimize2()
        {
            Validate();
            if (this.isValid && this.isDFA)
            {
                return Reverse().ConvertToDFA().Reverse().ConvertToDFA();
            }
            else if (this.isValid)
                Console.WriteLine("Unable to minimize automata, automata is not valid!");
            else if (!this.isDFA)
                Console.WriteLine("Unable to minimize automata, automata is not a DFA!");

            return null;
        }

        public Automata Minimized()
        {
            Automata clonedAutomata = Clone();
            clonedAutomata.Minimize();
            return clonedAutomata;
        }

        public Automata Minimized2()
        {
            Automata clonedAutomata = Clone();
            clonedAutomata.Minimize2();
            return clonedAutomata;
        }

        #endregion

        #region REVERSING

        public Automata Reverse()
        {
            List<State.StateTransition> stateTransitions = new List<State.StateTransition>();
            List<string> stateNames = this.states.Keys.ToList();
            for (int i = 0; i < stateNames.Count; i++)
            {
                stateTransitions.AddRange(this.states[stateNames[i]].GetStateTransitions());
                this.states[stateNames[i]].RemoveTransitions();

                State.StateType stateType = this.states[stateNames[i]].stateType;
                if (stateType == State.StateType.START_STATE)
                    this.states[stateNames[i]].stateType = State.StateType.END_STATE;
                else if (stateType == State.StateType.END_STATE)
                    this.states[stateNames[i]].stateType = State.StateType.START_STATE;
            }

            for (int i = 0; i < stateNames.Count; i++)
            {
                for (int j = 0; j < stateTransitions.Count; j++)
                {
                    if(stateTransitions[j].NextState == stateNames[i])
                        this.states[stateNames[i]].AddTransition(stateTransitions[j].Character, this.states[stateTransitions[j].PreviousState]);
                }
            }

            this.Validate();
            return this;
        }

        public Automata Reversed()
        {
            Automata automata = this.Clone();
            automata.Reverse();

            return automata;
        }

        #endregion

        #region CONVERSION_TO_DFA

        public Automata ConvertToDFA()
        {
            Validate();

            if (this.isValid)
            {
                if (this.isDFA)
                    return this;
            }

            AutomataConversionTable conversionTable = new AutomataConversionTable(this);

            //Console.WriteLine(conversionTable.ToString());

            this.states.Clear();
            this.isValid = false;
            this.isDFA = false;

            List<Tuple<string, State.StateType>> finalStates = conversionTable.GetFinalStates();
            for (int i = 0; i < finalStates.Count; i++)
                AddState(finalStates[i].Item1, finalStates[i].Item2);

            for (int i = 0; i < finalStates.Count; i++)
            {
                List<State.StateTransition> stateTransitions = conversionTable.GetFinalStateTransitions(finalStates[i].Item1);

                for (int j = 0; j < stateTransitions.Count; j++)
                    AddTransition(stateTransitions[j].Character, stateTransitions[j].PreviousState, stateTransitions[j].NextState);
            }

            return this;
        }

        public Automata ConvertedToDFA()
        {
            Automata clonedAutomata = Clone();
            clonedAutomata.ConvertToDFA();
            return clonedAutomata;
        }

        #endregion

        #region UTILITY

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<State> stateList = this.states.Values.ToList<State>();
            for (int i = 0; i < stateList.Count; i++)
                stringBuilder.Append(stateList[i].ToString());

            return stringBuilder.ToString();
        }

        public Automata Clone()
        {
            Automata automata = new Automata(this.symbols);

            List<string> stateNames = this.states.Keys.ToList();
            for (int i = 0; i < stateNames.Count; i++)
                automata.AddState(stateNames[i], this.states[stateNames[i]].stateType);

            for (int i = 0; i < stateNames.Count; i++)
            {
                List<State.StateTransition> stateTransitions = this.states[stateNames[i]].GetStateTransitions();
                for (int j = 0; j < stateTransitions.Count; j++)
                {
                    if(stateTransitions[j].IsEpsilon)
                        automata.AddTransition(stateNames[i], stateTransitions[j].NextState);
                    else
                        automata.AddTransition(stateTransitions[j].Character, stateNames[i], stateTransitions[j].NextState);
                }
            }

            return automata;
        }

        #endregion

        #region OPERATIONS

        public static Automata And(Automata automataA, Automata automataB)
        {
            automataA.Validate();
            automataB.Validate();

            if (automataA.isValid && automataB.isValid)
            {
                if (automataA.isDFA && automataB.isDFA)
                {
                    if (!CheckSymbolsEquavalance(automataA, automataB))
                        return null;
                    Automata automata = new Automata(automataA.symbols);

                    List<Tuple<string, string>> newStates = new List<Tuple<string, string>>();

                    List<State> statesA = automataA.states.Values.ToList();
                    List<State> statesB = automataB.states.Values.ToList();
                    for (int a = 0; a < statesA.Count; a++)
                    {
                        for (int b = 0; b < statesB.Count; b++)
                        {
                            newStates.Add(new Tuple<string, string>(statesA[a].Name, statesB[b].Name));
                            automata.AddIntermediateState(statesA[a].Name + "," + statesB[b].Name);
                        }
                    }

                    foreach(Tuple<string, string> newState in newStates)
                    {
                        foreach(char symbol in automataA.symbols)
                        {
                            State.StateTransition stateTransitionA = automataA.states[newState.Item1].GetStateTransitionForSymbol(symbol);
                            State.StateTransition stateTransitionB = automataB.states[newState.Item2].GetStateTransitionForSymbol(symbol);
                            automata.AddTransition(symbol, stateTransitionA.PreviousState + "," + stateTransitionB.PreviousState, stateTransitionA.NextState + "," + stateTransitionB.NextState);
                        }

                        State.StateType stateType = State.StateType.INTERMEDIATE_STATE;
                        State.StateType stateTypeA = automataA.GetState(newState.Item1).stateType;
                        State.StateType stateTypeB = automataB.GetState(newState.Item2).stateType;
                        if (stateTypeA == State.StateType.START_STATE && stateTypeB == State.StateType.START_STATE)
                            stateType = State.StateType.START_STATE;
                        else if (stateTypeA == State.StateType.END_STATE && stateTypeB == State.StateType.END_STATE)
                            stateType = State.StateType.END_STATE;
                        else if (stateTypeA == State.StateType.START_AND_END_STATE || stateTypeB == State.StateType.START_AND_END_STATE)
                            stateType = State.StateType.START_AND_END_STATE;

                        automata.GetState(newState.Item1 + "," + newState.Item2).stateType = stateType;
                    }

                    //return automata.ConvertToDFA();
                    return automata;
                }
            }
            return null;
        }

        public static Automata Or(Automata automataA, Automata automataB)
        {
            automataA.Validate();
            automataB.Validate();

            if (automataA.isValid && automataB.isValid)
            {
                if (automataA.isDFA && automataB.isDFA)
                {
                    if (!CheckSymbolsEquavalance(automataA, automataB))
                        return null;
                    Automata automata = new Automata(automataA.symbols);

                    List<Tuple<string, string>> newStates = new List<Tuple<string, string>>();

                    List<State> statesA = automataA.states.Values.ToList();
                    List<State> statesB = automataB.states.Values.ToList();
                    for (int a = 0; a < statesA.Count; a++)
                    {
                        for (int b = 0; b < statesB.Count; b++)
                        {
                            newStates.Add(new Tuple<string, string>(statesA[a].Name, statesB[b].Name));
                            automata.AddIntermediateState(statesA[a].Name + "," + statesB[b].Name);
                        }
                    }

                    foreach (Tuple<string, string> newState in newStates)
                    {
                        foreach (char symbol in automataA.symbols)
                        {
                            State.StateTransition stateTransitionA = automataA.states[newState.Item1].GetStateTransitionForSymbol(symbol);
                            State.StateTransition stateTransitionB = automataB.states[newState.Item2].GetStateTransitionForSymbol(symbol);
                            automata.AddTransition(symbol, stateTransitionA.PreviousState + "," + stateTransitionB.PreviousState, stateTransitionA.NextState + "," + stateTransitionB.NextState);
                        }

                        State.StateType stateType = State.StateType.INTERMEDIATE_STATE;
                        State.StateType stateTypeA = automataA.GetState(newState.Item1).stateType;
                        State.StateType stateTypeB = automataB.GetState(newState.Item2).stateType;
                        if (stateTypeA == State.StateType.START_STATE && stateTypeB == State.StateType.START_STATE)
                            stateType = State.StateType.START_STATE;
                        else if (stateTypeA == State.StateType.END_STATE || stateTypeB == State.StateType.END_STATE)
                            stateType = State.StateType.END_STATE;
                        else if (stateTypeA == State.StateType.START_AND_END_STATE || stateTypeB == State.StateType.START_AND_END_STATE)
                            stateType = State.StateType.START_AND_END_STATE;

                        automata.GetState(newState.Item1 + "," + newState.Item2).stateType = stateType;
                    }

                    //return automata.ConvertToDFA();
                    return automata;
                }
            }
            return null;
        }

        public static Automata Not(Automata automata)
        {
            Automata notAutomata = automata.Clone();

            List<string> stateNames = notAutomata.states.Keys.ToList();
            for (int i = 0; i < stateNames.Count; i++)
            {
                State.StateType stateType = automata.states[stateNames[i]].stateType;
                if (stateType == State.StateType.INTERMEDIATE_STATE)
                    notAutomata.states[stateNames[i]].stateType = State.StateType.END_STATE;
                else if (stateType == State.StateType.END_STATE)
                    notAutomata.states[stateNames[i]].stateType = State.StateType.INTERMEDIATE_STATE;
                else if (stateType == State.StateType.START_STATE)
                    notAutomata.states[stateNames[i]].stateType = State.StateType.START_AND_END_STATE;
                else if (stateType == State.StateType.START_AND_END_STATE)
                    notAutomata.states[stateNames[i]].stateType = State.StateType.START_STATE;
            }

            return notAutomata.ConvertToDFA();
        }

        private static bool CheckSymbolsEquavalance(Automata automataA, Automata automataB)
        {
            if (automataA.symbols.Count != automataB.symbols.Count)
                return false;

            foreach (char symbol in automataA.symbols)
                if (!automataB.symbols.Contains(symbol))
                    return false;

            return true;
        }

        public SortedSet<string> GenerateWordsInLanguage(int maxLength, int maxCycles)
        {
            int cycles = 0;
            var list = new SortedSet<string>();
            foreach(State s in states.Values)
            {
                if(s.stateType == State.StateType.START_AND_END_STATE || s.stateType == State.StateType.START_STATE)
                {
                    foreach(Transition t in s.GetTransitions())
                    {
                        var newWord = "";
                        if (!t.IsEpsilon)
                            newWord = newWord + t.Character.ToString();
                        SubGeneratewordsInLanguage(t.NextState, newWord ,maxLength, cycles, maxCycles, ref list);
                    }

                }
            }
            list.Remove("");
            return list;
        }

        private void SubGeneratewordsInLanguage(State s, string word, int maxLength, int cycles, int maxCycles, ref SortedSet<string> list)
        {
            cycles++;
            if (cycles < maxCycles)
            {
                if (word.Length <= maxLength)
                {
                    if (s.stateType == State.StateType.END_STATE || s.stateType == State.StateType.START_AND_END_STATE)
                    {
                        list.Add(word);
                    }
                    foreach (Transition t in s.GetTransitions())
                    {
                        var newWord = word;
                        if (!t.IsEpsilon)
                            newWord = newWord + t.Character.ToString();


                        SubGeneratewordsInLanguage(t.NextState, newWord, maxLength, cycles, maxCycles, ref list);
                    }

                }
            }

        }

        /*public SortedSet<string> GenerateWordsNotInLanguage(int maxLength, int maxCycles)
        {
            this.ConvertToDFA();
                int cycles = 0;
                var list = new SortedSet<string>();
                foreach (State s in states.Values)
                {
                    if (s.stateType == State.StateType.START_AND_END_STATE || s.stateType == State.StateType.START_STATE)
                    {
                        foreach (Transition t in s.GetTransitions())
                        {
                            var newWord = "";
                            if (!t.IsEpsilon)
                                newWord = newWord + t.Character.ToString();
                            SubGeneratewordsNotInLanguage(t.NextState, newWord, maxLength, cycles, maxCycles, ref list);
                        }

                    }
                }
                list.Remove("");
                return list;
            
        }

        private void SubGeneratewordsNotInLanguage(State s, string word, int maxLength, int cycles, int maxCycles, ref SortedSet<string> list)
        {
            cycles++;
            if (cycles < maxCycles)
            {
                if (word.Length <= maxLength)
                {
                    if (s.stateType != State.StateType.END_STATE && s.stateType != State.StateType.START_AND_END_STATE)
                    {
                        list.Add(word);
                    }
                    foreach (Transition t in s.GetTransitions())
                    {
                        var newWord = word;
                        if (!t.IsEpsilon)
                            newWord = newWord + t.Character.ToString();


                        SubGeneratewordsInLanguage(t.NextState, newWord, maxLength, cycles, maxCycles, ref list);
                    }

                }
            }

        }
        
        #endregion
    }*/
}
