using UnityEngine;

namespace Escape.Game.AI.EnemyTransitions {
    public class LostPlayer : Transition {

        private GameManager _gameManager;
        private GameObject _character;
        private GameObject _player;
        private float _sightDistance = 15f;

        public LostPlayer(GameObject character, GameObject player) {
            _character = character;
            _player = player;
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public override bool IsTriggered() {
            Vector2 characterPos = new Vector2(_character.transform.position.x, _character.transform.position.z);
            Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.z);
            return _gameManager.timeOut || Vector3.Distance(characterPos, playerPos) > _sightDistance;
        }
    }
}