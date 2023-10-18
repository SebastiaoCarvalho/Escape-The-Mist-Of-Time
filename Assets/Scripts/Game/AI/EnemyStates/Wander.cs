using System;
using UnityEngine;

namespace Escape.Game.AI.Enemy.States {

    public class Wander : State {

        private GameObject _character;
        private GameObject _anchorPoint;
        private float _radius = 20f;
        private Vector3 _destination;

        public Wander(GameObject gameObject, GameObject anchorPoint) {
            this._character = gameObject;
            this._anchorPoint = anchorPoint;
            _destination = _anchorPoint.transform.position;
        }

        public override void Execute() {
            Vector2 destination = new Vector2(_destination.x, _destination.z);
            Vector2 character = new Vector2(_character.transform.position.x, _character.transform.position.z);
            if ((destination - character).magnitude < 1f) {
                Vector2 circle = UnityEngine.Random.insideUnitCircle * _radius;
                _destination = _anchorPoint.transform.position + new Vector3(circle.x, 0, circle.y);
                /* if (Physics.Raycast(_destination + new Vector3(0, 100, 0), Vector3.down, out RaycastHit hit, 200f, LayerMask.GetMask("Ground")))
                {
                    _destination = hit.point;
                } */
                
            }
            Vector3 movement = _destination - _character.transform.position;
            movement.y = 0;
            movement.Normalize();
            _character.transform.position += 5 * Time.deltaTime * movement;
            _character.transform.rotation = Quaternion.LookRotation(movement);
            _character.transform.rotation *= Quaternion.Euler(0, 90, 0);
        }

    }

}
