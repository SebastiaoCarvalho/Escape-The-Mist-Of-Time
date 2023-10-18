using System;
using UnityEngine;

namespace Escape.Game.AI.Enemy.States {

    public class PursuePlayer : State {

        private GameObject _character;
        private GameObject _player;

        private float _maxPrediction = 1f;


        public PursuePlayer(GameObject character, GameObject player) {
            _character = character;
            _player = player;
        }

        public override void Execute() {
            Vector3 direction = (_player.transform.position - _character.transform.position).normalized;
            direction.y = 0;
            float distance = direction.magnitude; 
            float speed = _character.GetComponent<Rigidbody>().velocity.magnitude;
            float minSpeedPrediction = distance / _maxPrediction;
            float t;
            if (speed < minSpeedPrediction)
                t = _maxPrediction;
            else
                t = distance/speed;
            Vector3 playerVelocity = _player.GetComponent<CharacterController>().velocity;
            playerVelocity.y = 0;
            Vector3 predictedPosition = _player.transform.position + t * playerVelocity;
            Vector3 movement = (predictedPosition - _character.transform.position).normalized;
            _character.transform.position += 5 * Time.deltaTime * movement;
            _character.transform.rotation = Quaternion.LookRotation(movement);
            _character.transform.rotation *= Quaternion.Euler(0, 90, 0);
        }

    }

}