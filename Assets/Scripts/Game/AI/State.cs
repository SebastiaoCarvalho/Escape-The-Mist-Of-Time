using System.Collections.Generic;

namespace Escape.Game.AI {

    public class State {

        private List<Transition> _transitions = new List<Transition>();
        public List<Transition> Transitions { get { return _transitions; } }

        public virtual void Enter() {}
        public virtual void Execute() {}
        public virtual void Exit() {}
    }

}