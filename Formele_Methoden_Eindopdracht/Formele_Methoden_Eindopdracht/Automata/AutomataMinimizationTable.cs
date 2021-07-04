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
            public readonly State.StateType StateType;
            public List<PartitionEntryItem> Items { get { return this.items; } }
            private List<PartitionEntryItem> items;

            public PartitionEntry(string stateName, State.StateType stateType, List<char> symbols)
            {
                this.StateName = stateName;
                this.StateType = stateType;
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

            public void AddPartitionEntry(char symbol, State.StateType stateType, string state)
            {
                if (this.entries.Where(m => m.StateName == state).Count() == 0)
                    this.entries.Add(new PartitionEntry(state, stateType, this.symbols));

                this.entries.Where(m => m.StateName == state).First().AddStateItem(symbol, state);
            }

            public bool ContainsState(string state)
            {
                return (this.entries.Where(m => m.StateName == state).Count() > 0);
            }
        }

        private class Partition
        {
            public List<PartitionSegment> PrimarySegments { get { return this.primarySegments; } }
            public List<PartitionSegment> SecondarySegments { get { return this.secondarySegments; } }

            private List<PartitionSegment> primarySegments;
            private List<PartitionSegment> secondarySegments;

            private List<char> symbols;

            public Partition(List<char> symbols)
            {
                this.primarySegments = new List<PartitionSegment>();
                this.secondarySegments = new List<PartitionSegment>();

                this.symbols = symbols;
            }

            public void AddPrimarySegment(string segmentName)
            {
                if (this.primarySegments.Where(m => m.SegmentName == segmentName).Count() == 0)
                    this.primarySegments.Add(new PartitionSegment(segmentName, this.symbols));
            }

            public void AddSecondarySegment(string segmentName)
            {
                if (this.secondarySegments.Where(m => m.SegmentName == segmentName).Count() == 0)
                    this.secondarySegments.Add(new PartitionSegment(segmentName, this.symbols));
            }

            public void AddPrimaryEntry(string segmentName, char symbol, State.StateType stateType, string state)
            {
                AddPrimarySegment(segmentName);
                this.primarySegments.Where(m => m.SegmentName == segmentName).First().AddPartitionEntry(symbol, stateType, state);
            }

            public void AddSecondaryEntry(string segmentName, char symbol, State.StateType stateType, string state)
            {
                AddSecondarySegment(segmentName);
                this.secondarySegments.Where(m => m.SegmentName == segmentName).First().AddPartitionEntry(symbol, stateType, state);
            }

            public string GetSegmentNameContainingState(string state)
            {
                foreach(PartitionSegment primarySegment in this.primarySegments)
                {
                    if (primarySegment.ContainsState(state))
                        return primarySegment.SegmentName;
                }

                foreach (PartitionSegment secondarySegment in this.secondarySegments)
                {
                    if (secondarySegment.ContainsState(state))
                        return secondarySegment.SegmentName;
                }

                return "";
            }
        }

        #endregion

        private List<SetupTableEntry> setupTable;
        private List<Partition> partitions;

        private int partitionNumber = 1;

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
            List<SetupTableEntry> nonEndStateEntries = this.setupTable.Where(m => m.StateType != State.StateType.START_AND_END_STATE || m.StateType != State.StateType.END_STATE).ToList();
            List<SetupTableEntry> endStateEntries = this.setupTable.Where(m => m.StateType != State.StateType.START_AND_END_STATE || m.StateType != State.StateType.END_STATE).ToList();

            Partition partition = new Partition(automata.symbols);

            foreach (SetupTableEntry nonEndStateEntry in nonEndStateEntries)
                foreach(SetupTableEntryItem nonEndStateEntryItem in nonEndStateEntry.Items)
                    partition.AddPrimaryEntry("1", nonEndStateEntryItem.Symbol, nonEndStateEntry.StateType, nonEndStateEntry.StateName);

            foreach (SetupTableEntry endStateEntry in endStateEntries)
                foreach (SetupTableEntryItem endStateEntryItem in endStateEntry.Items)
                    partition.AddPrimaryEntry("2", endStateEntryItem.Symbol, endStateEntry.StateType, endStateEntry.StateName);

            this.partitionNumber = 3;
        }

        private void ConstructPartitions(Automata automata)
        {
            Tuple<Dictionary<string, List<string>>, Dictionary<string, List<string>>> partitionEvaluation = EvaluateLastPartition();


        }

        // Returns two dictionaries with keys indicating the segment name and a list of state names which are withing the segment
        private Tuple<Dictionary<string, List<string>>, Dictionary<string, List<string>>> EvaluateLastPartition()
        {
            Partition lastPartition = this.partitions[this.partitions.Count - 1];

            bool shouldPartition = false;
            Dictionary<string, List<string>> primaryDictionary = EvaluateSegments(lastPartition.PrimarySegments, ref lastPartition, ref shouldPartition);
            Dictionary<string, List<string>> secondaryDictionary = EvaluateSegments(lastPartition.SecondarySegments, ref lastPartition, ref shouldPartition);

            return new Tuple<Dictionary<string, List<string>>, Dictionary<string, List<string>>>(primaryDictionary, secondaryDictionary);
        }

        // Returns dictionary with keys indicating the segment name and a list of state names which are withing the segment
        private Dictionary<string, List<string>> EvaluateSegments(List<PartitionSegment> segments, ref Partition lastPartition, ref bool shouldPartition)
        {
            Dictionary<string, List<string>> segmentDictionary = new Dictionary<string, List<string>>();
            foreach (PartitionSegment segment in segments)
            {
                Dictionary<string, string> segmentDefinitions = new Dictionary<string, string>();
                foreach (PartitionEntry entry in segment.Entries)
                {
                    //if (segmentDefinitions.ContainsKey(segment.SegmentName))
                    //    segmentDefinitions.Add(segment.SegmentName, new List<string>());
                    //segmentDefinitions[segment.SegmentName].Add(entry.StateName);

                    //segmentDefinitions.Add(entry.StateName, segment.SegmentName);
                }

                shouldPartition = (segmentDefinitions.Count > 1);

                //List<string> segmentNames = segmentDefinitions.Keys.ToList();
                //foreach (string segmentName in segmentNames)
                //    segmentDictionary.Add(segmentName, segmentDefinitions[segmentName]);
            }

            return segmentDictionary;
        }
    }
}
