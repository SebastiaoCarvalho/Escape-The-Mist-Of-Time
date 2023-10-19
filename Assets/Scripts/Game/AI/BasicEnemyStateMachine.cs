using Escape.Game.AI.EnemyStates;
using Escape.Game.AI.EnemyTransitions;
using UnityEngine;

namespace Escape.Game.AI {

    public class BasicEnemyStateMachine : StateMachine {

        public BasicEnemyStateMachine(GameObject character, GameObject anchorPoint, GameObject player, float wanderSpeed, float radius, float chaseSpeed) {
            Wander wander = new Wander(character, anchorPoint, wanderSpeed, radius);
            PursuePlayer pursuePlayer = new PursuePlayer(character, player, chaseSpeed);
            Transition sawPlayer = new SawPlayer(character, player) { TargetState = pursuePlayer };
            Transition lostPlayer = new LostPlayer(character, player) { TargetState = wander };
            wander.Transitions.Add(sawPlayer);
            pursuePlayer.Transitions.Add(lostPlayer);
            Build(new State[] { wander, pursuePlayer }, wander);
        }
        
    }

}