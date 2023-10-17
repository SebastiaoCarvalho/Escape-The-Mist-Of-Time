using UnityEngine;

namespace Escape.Game.AI.Enemy.Transitions {
    public class LostPlayer : Transition {

        private GameObject _character;
        private GameObject _player;
        private float _sightDistance = 15f;

        public LostPlayer(GameObject character, GameObject player) {
            _character = character;
            _player = player;
        }

        public override bool IsTriggered() {
            return Vector3.Distance(_character.transform.position, _player.transform.position) > _sightDistance;
        }
    }
}