using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Escape.Game.AI;

public class Enemy : MonoBehaviour
{

    private StateMachine _stateMachine;
    public GameObject AnchorPrefab;
    private GameObject _anchorPoint;
    // Start is called before the first frame update
    void Start()
    {
        _anchorPoint = Instantiate(AnchorPrefab, transform.position, Quaternion.identity);
        _stateMachine = new BasicEnemyStateMachine(gameObject, _anchorPoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        _stateMachine.ExecuteStateUpdate();
    }
}
