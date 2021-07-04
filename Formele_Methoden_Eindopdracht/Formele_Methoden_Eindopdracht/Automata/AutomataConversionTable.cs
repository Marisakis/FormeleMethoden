using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    class AutomataConversionTable
    {
        private class SymbolStateTransition
        {
            public readonly char symbol;
            public List<string> States { get { return this.states; } }
            private List<string> states;

            public SymbolStateTransition(char symbol)
            {
                this.symbol = symbol;
                this.states = new List<string>();
            }

            public void AddState(string state)
            {
                if (!this.states.Contains(state))
                    this.states.Add(state);
            }

            public string GetNameFromStates()
            {
                return GetCombinedStatesName(this.states);
            }

            public static string GetCombinedStatesName(List<string> states)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < states.Count; i++)
                {
                    stringBuilder.Append(states[i]);
                    if (i < (states.Count - 1))
                        stringBuilder.Append(",");
                }
                return stringBuilder.ToString();
            }
        }

        private class TableStateEntry
        {
            public readonly string stateName;
            public readonly State.StateType stateType;
            private List<SymbolStateTransition> symbolStateTransitions;

            public TableStateEntry(string stateName, State.StateType stateType, List<char> symbols)
            {
                this.stateName = stateName;
                this.stateType = stateType;
                this.symbolStateTransitions = new List<SymbolStateTransition>();
                for (int i = 0; i < symbols.Count; i++)
                    this.symbolStateTransitions.Add(new SymbolStateTransition(symbols[i]));
            }

            public void AddStateTransition(char symbol, string state)
            {
                if(!this.symbolStateTransitions.Where(m => m.symbol == symbol).First().States.Contains(state))
                    this.symbolStateTransitions.Where(m => m.symbol == symbol).First().AddState(state);
            }

            public List<string> GetTransitionStateNames()
            {
                List<string> transitionStateNames = new List<string>();
                for (int i = 0; i < this.symbolStateTransitions.Count; i++)
                    transitionStateNames.Add(this.symbolStateTransitions[i].GetNameFromStates());
                return transitionStateNames;
            }

            public List<string> GetTransitionStatesBySymbol(char symbol)
            {
                return this.symbolStateTransitions.Where(m => m.symbol == symbol).Select(m => m.States).First();
            }

            public List<string> GetTransitionStatesBySymbolWithEmpty(char symbol)
            {
                if (this.symbolStateTransitions.Where(m => m.symbol == symbol).Select(m => m.States).First().Count() == 0)
                    return new List<string>() { "{}" };
                else
                    return this.symbolStateTransitions.Where(m => m.symbol == symbol).Select(m => m.States).First();
            }
        }

        // Help table
        private List<TableStateEntry> helpTableStates;

        // Final table
        private List<TableStateEntry> finalTableStates;
        private int proccesIndex = 0;

        Automata referencedAutomata;

        public AutomataConversionTable(Automata automata)
        {
            this.helpTableStates = new List<TableStateEntry>();
            this.finalTableStates = new List<TableStateEntry>();

            this.referencedAutomata = automata;

            ConstructHelpTable(automata);
            ConstructFinalTable(automata);
        }

        private void ConstructHelpTable(Automata automata)
        {
            List<State> states = automata.GetStates();
            foreach(State state in states)
            {
                TableStateEntry tableStateEntry = new TableStateEntry(state.Name, state.stateType, automata.symbols);

                List<State.StateTransition> stateTransitions = state.GetResolvedStateTransitions(automata.symbols);
                foreach(State.StateTransition stateTransition in stateTransitions)
                    tableStateEntry.AddStateTransition(stateTransition.Character, stateTransition.NextState);

                this.helpTableStates.Add(tableStateEntry);
            }
        }

        private void ConstructFinalTable(Automata automata)
        {
            List<string> helpTableEndStates = this.helpTableStates.Where(m => m.stateType == State.StateType.END_STATE).Select(m => m.stateName).ToList();

            TableStateEntry helpStartStateEntry = this.helpTableStates.Where(m => (m.stateType == State.StateType.START_STATE) || (m.stateType == State.StateType.START_AND_END_STATE)).First();
            TableStateEntry startStateEntry = new TableStateEntry(helpStartStateEntry.stateName, helpStartStateEntry.stateType, automata.symbols);
            CombineStates(this.helpTableStates.Where(m => (m.stateType == State.StateType.START_STATE) || (m.stateType == State.StateType.START_AND_END_STATE)).ToList(), automata.symbols, startStateEntry);

            this.finalTableStates.Add(startStateEntry);
            List<string> startStateNames = startStateEntry.GetTransitionStateNames();
            AddNewStates(startStateNames, helpTableEndStates, automata.symbols);

            TableStateEntry currentStateEntry = null;
            this.proccesIndex++;

            while (this.proccesIndex < this.finalTableStates.Count)
            {
                currentStateEntry = this.finalTableStates[this.proccesIndex];

                CombineStates(this.helpTableStates.Where(m => currentStateEntry.stateName.Contains(m.stateName)).ToList(), automata.symbols, currentStateEntry);
                List<string> stateNames = currentStateEntry.GetTransitionStateNames();
                AddNewStates(stateNames, helpTableEndStates, automata.symbols);

                this.proccesIndex++;
            }
        }

        private void CombineStates(List<TableStateEntry> states, List<char> symbols, TableStateEntry stateEntry)
        {
            State.StateType stateType = State.StateType.INTERMEDIATE_STATE;
            if (states.Where(m => m.stateType == State.StateType.START_STATE).Count() > 0)
                stateType = State.StateType.START_STATE;
            else if (states.Where(m => m.stateType == State.StateType.END_STATE).Count() > 0)
                stateType = State.StateType.END_STATE;
            else if (states.Where(m => m.stateType == State.StateType.START_AND_END_STATE).Count() > 0)
                stateType = State.StateType.START_AND_END_STATE;

            foreach (char symbol in symbols)
            {
                List<string> combinedStates = new List<string>();
                foreach (TableStateEntry tableStateEntry in states)
                {
                    List<string> transitionStates = tableStateEntry.GetTransitionStatesBySymbol(symbol);
                    for (int i = 0; i < transitionStates.Count; i++)
                        stateEntry.AddStateTransition(symbol, transitionStates[i]);
                }
            }
        }

        private void AddNewStates(List<string> states, List<string> endStates, List<char> symbols)
        {
            foreach(string state in states)
            {
                string stateName = state;
                if (String.IsNullOrEmpty(stateName))
                    stateName = "{}";

                if (!(this.finalTableStates.Where(m => m.stateName == stateName).Count() > 0))
                {
                    bool isEndState = false;
                    foreach (string endState in endStates)
                    {
                        if (stateName.Contains(endState))
                        {
                            isEndState = true;
                            break;
                        }
                    }

                    this.finalTableStates.Add(new TableStateEntry(stateName, (isEndState) ? State.StateType.END_STATE : State.StateType.INTERMEDIATE_STATE, symbols));
                }
            }
        }

        public List<Tuple<string, State.StateType>> GetFinalStates()
        {
            List<Tuple<string, State.StateType>> states = new List<Tuple<string, State.StateType>>();

            foreach(TableStateEntry finalTableStateEntry in this.finalTableStates)
                states.Add(new Tuple<string, State.StateType>(finalTableStateEntry.stateName, finalTableStateEntry.stateType));

            return states;
        }

        public List<State.StateTransition> GetFinalStateTransitions(string stateName)
        {
            List<State.StateTransition> stateTransitions = new List<State.StateTransition>();

            TableStateEntry tableStateEntry = this.finalTableStates.Where(m => m.stateName == stateName).First();
            foreach(char symbol in this.referencedAutomata.symbols)
            {
                List<string> transitions = tableStateEntry.GetTransitionStatesBySymbolWithEmpty(symbol);
                string finalStateName = SymbolStateTransition.GetCombinedStatesName(transitions);
                stateTransitions.Add(new State.StateTransition(symbol, stateName, finalStateName));
            }

            return stateTransitions;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Help table:");
            foreach (char symbol in this.referencedAutomata.symbols)
                stringBuilder.Append("\t\t\t\t" + symbol);
            stringBuilder.Append("\n");
            foreach (TableStateEntry helpTableStateEntry in this.helpTableStates)
            {
                stringBuilder.Append(helpTableStateEntry.stateName);
                List<string> stateTransitions = helpTableStateEntry.GetTransitionStateNames();
                foreach (string stateTransition in stateTransitions)
                    stringBuilder.Append("\t\t\t\t" + ((String.IsNullOrEmpty(stateTransition)) ? "{}" : stateTransition));
                stringBuilder.Append("\n");
            }

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("Final table:");
            foreach (char symbol in this.referencedAutomata.symbols)
                stringBuilder.Append("\t\t\t\t" + symbol);
            stringBuilder.Append("\n");
            foreach (TableStateEntry finalTableStateEntry in this.finalTableStates)
            {
                stringBuilder.Append(finalTableStateEntry.stateName);
                List<string> stateTransitions = finalTableStateEntry.GetTransitionStateNames();
                foreach (string stateTransition in stateTransitions)
                    stringBuilder.Append("\t\t\t\t" + ((String.IsNullOrEmpty(stateTransition)) ? "{}" : stateTransition));
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}
