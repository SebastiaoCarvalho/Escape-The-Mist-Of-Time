using Escape.Game.AI.EnemyStates;
using Escape.Game.AI.EnemyTransitions;
using UnityEngine;

namespace Escape.Game.AI {

    public class MrCapetaStateMachine : StateMachine {

        public MrCapetaStateMachine(GameObject character, GameObject anchorPoint, GameObject player, float wanderSpeed, float radius, float defendSpeed, float chaseDistance, float lostDistance) {
            Wander wander = new Wander(character, anchorPoint, wanderSpeed, radius);
            DefendRespawn defendRespawn = new DefendRespawn(character, player, anchorPoint, defendSpeed);
            Transition sawPlayer = new SawPlayer(character, player, chaseDistance) { TargetState = defendRespawn };
            Transition lostPlayer = new LostPlayer(character, player, lostDistance) { TargetState = wander };
            wander.Transitions.Add(sawPlayer);
            defendRespawn.Transitions.Add(lostPlayer);
            Build(new State[] { wander, defendRespawn }, wander);
        }
        
    }

}