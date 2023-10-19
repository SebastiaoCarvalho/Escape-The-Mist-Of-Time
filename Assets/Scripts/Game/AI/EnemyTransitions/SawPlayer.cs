using UnityEngine;

namespace Escape.Game.AI.EnemyTransitions {
    public class SawPlayer : Transition {

        private GameObject _character;
        private GameObject _player;
        private float _sightDistance = 10f;

        public SawPlayer(GameObject character, GameObject player) {
            _character = character;
            _player = player;
        }

        public override bool IsTriggered() {
            Vector2 characterPos = new Vector2(_character.transform.position.x, _character.transform.position.z);
            Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.z);
            return Vector3.Distance(characterPos, playerPos) < _sightDistance;
        }
    }
}