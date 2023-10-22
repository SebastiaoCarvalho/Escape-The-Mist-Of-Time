using System;
using UnityEngine;

namespace Escape.Game.AI.EnemyStates {

    public class DefendRespawn : State {

        private GameObject _character;
        private Enemy _enemyScript;
        private GameObject _player;
        private GameObject _anchorPoint;

        private float _speed;
        private Vector3 _destination;


        public DefendRespawn(GameObject character, GameObject player, GameObject anchorPoint, float speed) {
            _character = character;
            _enemyScript = _character.GetComponent<Enemy>();
            _player = player;
            _speed = speed;
            _anchorPoint = anchorPoint;
            _destination = _anchorPoint.transform.position;
        }

        public override void Execute() {
            Vector3 playerTragectory = (_player.transform.position - _destination);
            playerTragectory.y = 0;
            Vector3 playerDirection = playerTragectory.normalized;
            float playerDistance = playerTragectory.magnitude;

            Vector3 defencePosition = _destination + playerDirection * playerDistance * 0.6f;

            Vector3 movement = defencePosition - _character.transform.position;
            movement.y = 0;
            movement.Normalize();
            _character.GetComponent<Rigidbody>().velocity = _speed * movement;
            _character.transform.rotation = Quaternion.LookRotation(playerTragectory);
            _character.transform.rotation *= Quaternion.Euler(0, _enemyScript.ExtraRotation, 0);
        }

    }

}