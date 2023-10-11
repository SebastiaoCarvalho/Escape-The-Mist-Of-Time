using Escape.Game.AI.Enemy.States;
using UnityEngine;

namespace Escape.Game.AI {

    public class BasicEnemyStateMachine : StateMachine {

        public BasicEnemyStateMachine(GameObject character, GameObject anchorPoint) {
            Wander wander = new Wander(character, anchorPoint);
            Build(new State[] { wander }, wander);
        }
        
    }

}