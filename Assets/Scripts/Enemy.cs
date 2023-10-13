using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Escape.Game.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{

    private StateMachine _stateMachine;
    public GameObject AnchorPrefab;
    private GameObject _anchorPoint;
    // Start is called before the first frame update
    void Start()
    {
        _anchorPoint = FindNearestAnchor();
        _stateMachine = new BasicEnemyStateMachine(gameObject, _anchorPoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        _stateMachine.ExecuteStateUpdate();
    }

    private GameObject FindNearestAnchor() {
        float min = GameObject.FindGameObjectsWithTag("Anchor").ToList().Min(x => (x.transform.position - transform.position).magnitude);
        return GameObject.FindGameObjectsWithTag("Anchor").ToList().Find(x => (x.transform.position - transform.position).magnitude == min);
    }
}
