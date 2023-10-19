using System;
using UnityEngine;
using Escape.Game;

namespace Escape.Game.AI.EnemyStates {

    public class Wander : State {

        private GameObject _character;
        private Enemy _enemyScript;
        private GameObject _anchorPoint;
        private float _radius = 15f;
        private float _speed = 0.5f;
        private Vector3 _destination;

        public Wander(GameObject gameObject, GameObject anchorPoint, float speed, float radius) {
            this._character = gameObject;
            this._enemyScript = _character.GetComponent<Enemy>();
            this._anchorPoint = anchorPoint;
            this._speed = speed;
            this._radius = radius;
            _destination = _anchorPoint.transform.position;
        }

        public override void Execute() {
            Vector2 destination = new Vector2(_destination.x, _destination.z);
            Vector2 character = new Vector2(_character.transform.position.x, _character.transform.position.z);
            if ((destination - character).magnitude < 1f) {
                Vector2 circle = UnityEngine.Random.insideUnitCircle * _radius;
                _destination = _anchorPoint.transform.position + new Vector3(circle.x, 0, circle.y);
            }
            else
            {
                Vector3 movement = _destination - _character.transform.position;
                movement.y = 0;
                movement.Normalize();
                _character.GetComponent<Rigidbody>().velocity = _speed * movement;
                _character.transform.rotation = Quaternion.LookRotation(movement);
                _character.transform.rotation *= Quaternion.Euler(0, _enemyScript.ExtraRotation, 0);
            }
        }

    }

}
