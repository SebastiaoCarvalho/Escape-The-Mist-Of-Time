using UnityEngine;

namespace Escape.Game.AI.EnemyTransitions {
    public class SawPlayer : Transition {

        private GameManager _gameManager;
        private GameObject _character;
        private GameObject _player;
        private Player _playerScript;
        private float _sightDistance = 10f;

        public SawPlayer(GameObject character, GameObject player, float distance) {
            _character = character;
            _player = player;
            _playerScript = player.GetComponent<Player>();
            _sightDistance = distance;
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public override bool IsTriggered() {
            Vector2 characterPos = new Vector2(_character.transform.position.x, _character.transform.position.z);
            Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.z);
            return !_gameManager.timeOut && _playerScript.Moved && Vector3.Distance(characterPos, playerPos) < _sightDistance;
        }
    }
}