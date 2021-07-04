using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    class AutomataMinimizationTable
    {

        #region SETUP_TABLE

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

        #endregion

        #region PARTITIONS_TABLE

        private class PartitionEntryItem
        {
            public readonly char Symbol;
            public List<string> States { get { return this.states; } }
            private List<string> states;

            public PartitionEntryItem(char symbol)
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

        private class PartitionEntry
        { 
            public readonly string StateName;
            public List<PartitionEntryItem> Items { get { return this.items; } }
            private List<PartitionEntryItem> items;

            public PartitionEntry(string stateName, List<char> symbols)
            {
                this.StateName = stateName;
                this.items = new List<PartitionEntryItem>();
                for (int i = 0; i < symbols.Count; i++)
                    this.items.Add(new PartitionEntryItem(symbols[i]));
            }

            public void AddStateItem(char symbol, string state)
            {
                if (!this.items.Where(m => m.Symbol == symbol).First().States.Contains(state))
                    this.items.Where(m => m.Symbol == symbol).First().AddState(state);
            }
        }

        private class PartitionSegment
        {
            public readonly string SegmentName;
            public List<PartitionEntry> Entries { get { return this.entries; } }
            private List<PartitionEntry> entries;

            private List<char> symbols;

            public PartitionSegment(string segmentName, List<char> symbols)
            {
                this.SegmentName = segmentName;
                this.entries = new List<PartitionEntry>();

                this.symbols = symbols;
            }

            public void AddPartitionEntry(char symbol, string state)
            {
                if (this.entries.Where(m => m.StateName == state).Count() == 0)
                    this.entries.Add(new PartitionEntry(state, this.symbols));

                this.entries.Where(m => m.StateName == state).First().AddStateItem(symbol, state);
            }
        }

        private class Partition
        {
            public List<PartitionSegment> primarySegments;
            public List<PartitionSegment> secondarySegments;

            public Partition()
            {
                this.primarySegments = new List<PartitionSegment>();
                this.secondarySegments = new List<PartitionSegment>();
            }
        }

        #endregion

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
