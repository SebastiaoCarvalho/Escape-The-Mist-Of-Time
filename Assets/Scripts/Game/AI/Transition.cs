namespace Escape.Game.AI {

    public abstract class Transition {

        public State TargetState { get; set; }

        public abstract bool IsTriggered();

    }

}