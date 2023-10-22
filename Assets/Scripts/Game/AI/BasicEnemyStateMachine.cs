using Escape.Game.AI.EnemyStates;
using Escape.Game.AI.EnemyTransitions;
using UnityEngine;

namespace Escape.Game.AI {

    public class BasicEnemyStateMachine : StateMachine {

        public BasicEnemyStateMachine(GameObject character, GameObject anchorPoint, GameObject player, float wanderSpeed, float radius, float chaseSpeed, float chaseDistance, float lostDistance) {
            Wander wander = new Wander(character, anchorPoint, wanderSpeed, radius);
            PursuePlayer pursuePlayer = new PursuePlayer(character, player, chaseSpeed);
            Transition sawPlayer = new SawPlayer(character, player, chaseDistance) { TargetState = pursuePlayer };
            Transition lostPlayer = new LostPlayer(character, player, lostDistance) { TargetState = wander };
            wander.Transitions.Add(sawPlayer);
            pursuePlayer.Transitions.Add(lostPlayer);
            Build(new State[] { wander, pursuePlayer }, wander);
        }
        
    }

}