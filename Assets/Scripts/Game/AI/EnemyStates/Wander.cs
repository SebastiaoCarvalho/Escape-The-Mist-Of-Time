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
            if (_destination == _character.transform.position) {
                _destination = _anchorPoint.transform.position + UnityEngine.Random.insideUnitSphere * _radius;
                _destination.y = _anchorPoint.transform.position.y;
            }
            _character.transform.position = Vector3.MoveTowards(_character.transform.position, _destination, 5f * Time.deltaTime);
        }

    }

}