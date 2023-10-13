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
            if ((_destination - _character.transform.position).magnitude < 1f) {
                Vector2 circle = UnityEngine.Random.insideUnitCircle * _radius;
                _destination = _anchorPoint.transform.position + new Vector3(circle.x, 0, circle.y);
                /* if (Physics.Raycast(_destination + new Vector3(0, 100, 0), Vector3.down, out RaycastHit hit, 200f, LayerMask.GetMask("Ground")))
                {
                    _destination = hit.point;
                } */
                
            }
            Vector3 movement = (_destination - _character.transform.position).normalized;
            movement.y = 0;
            _character.transform.position += 5 * Time.deltaTime * movement;
        }

    }

}
