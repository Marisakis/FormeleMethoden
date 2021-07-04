using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formele_Methoden_Eindopdracht
{
    public class Transition
    {
        public readonly char Character;
        public readonly bool IsEpsilon;

        public readonly State PreviousState;
        public readonly State NextState;

        public Transition(char character, State previousState, State nextState)
        {
            this.Character = character;
            this.IsEpsilon = false;
            this.PreviousState = previousState;
            this.NextState = nextState;
        }

        public Transition(State previousState, State nextState)
        {
            this.Character = 'ε';
            this.IsEpsilon = true;
            this.PreviousState = previousState;
            this.NextState = nextState;
        }

        public bool EvaluateTransition(char inputCharacter)
        {
            return (this.Character == inputCharacter);
        }
    }
}
