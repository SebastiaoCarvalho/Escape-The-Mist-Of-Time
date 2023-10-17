using Escape.Game.AI.Enemy.States;
using Escape.Game.AI.Enemy.Transitions;
using UnityEngine;

namespace Escape.Game.AI {

    public class BasicEnemyStateMachine : StateMachine {

        public BasicEnemyStateMachine(GameObject character, GameObject anchorPoint, GameObject player) {
            Wander wander = new Wander(character, anchorPoint);
            PursuePlayer pursuePlayer = new PursuePlayer(character, player);
            Transition sawPlayer = new SawPlayer(character, player) { TargetState = pursuePlayer };
            Transition lostPlayer = new LostPlayer(character, player) { TargetState = wander };
            wander.Transitions.Add(sawPlayer);
            pursuePlayer.Transitions.Add(lostPlayer);
            Build(new State[] { wander, pursuePlayer }, wander);
        }
        
    }

}