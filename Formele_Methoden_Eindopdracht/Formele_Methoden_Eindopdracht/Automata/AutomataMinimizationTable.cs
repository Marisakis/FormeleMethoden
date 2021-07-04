using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    class AutomataMinimizationTable
    {
        private class SetupTableEntryItem
        {
            public readonly char Symbol;
            public List<string> States { get { return this.states; } }
            private List<string> states;

            public SetupTableEntryItem(char symbol)
            {
                this.Symbol = symbol;
                this.states = new List<string>();
            }

            public void AddState(string state)
            {
                if (!this.states.Contains(state))
                    this.states.Add(state);
            }
        }

        private class SetupTableEntry
        {
            public readonly string StateName;
            public readonly State.StateType StateType;
            public List<SetupTableEntryItem> Items { get { return this.items; } }
            private List<SetupTableEntryItem> items;

            public SetupTableEntry(string stateName, State.StateType stateType, List<char> symbols)
            {
                this.StateName = stateName;
                this.StateType = stateType;
                this.items = new List<SetupTableEntryItem>();
                for (int i = 0; i < symbols.Count; i++)
                    this.items.Add(new SetupTableEntryItem(symbols[i]));
            }

            public void AddItemState(char symbol, string state)
            {
                this.items.Where(m => m.Symbol == symbol).First().AddState(state);
            }
        }

        private class PartitionEntry
        { 
            public readonly string StateName;
            public List<Tuple<char, int>> partitionSegments;

            public PartitionEntry()
            {

            }
        }

        private class Partition
        {

        }

        private List<SetupTableEntry> setupTable;
        private List<Partition> partitions;

        public AutomataMinimizationTable(Automata automata)
        {
            this.setupTable = new List<SetupTableEntry>();
            this.partitions = new List<Partition>();

            automata.Validate();
            if(automata.IsDFA)
            {
                ConstructSetupTable(automata);
                ConstructFirstPartition(automata);
                ConstructPartitions(automata);
            }
        }

        private void ConstructSetupTable(Automata automata)
        {
            List<State> states = automata.GetStates();
            foreach (State state in states)
            {
                SetupTableEntry setupTableEntry = new SetupTableEntry(state.Name, state.stateType, automata.symbols);

                List<State.StateTransition> stateTransitions = state.GetStateTransitions();
                foreach (State.StateTransition stateTransition in stateTransitions)
                    setupTableEntry.AddItemState(stateTransition.Character, stateTransition.NextState);

                this.setupTable.Add(setupTableEntry);
            }
        }

        private void ConstructFirstPartition(Automata automata)
        {

        }

        private void ConstructPartitions(Automata automata)
        {

        }
    }
}
