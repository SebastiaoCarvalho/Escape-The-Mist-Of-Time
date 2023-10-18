using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Escape.Game.AI;
using System.Linq;

public struct EnemyData {
    public GameObject prefab;
    public Vector3 position;
    public float hp;
}

public class Enemy : MonoBehaviour, IObserved
{

    private StateMachine _stateMachine;
    private float _hp = 10;
    public float Hp { get { return _hp; } set { _hp = value; } }
    public GameObject AnchorPrefab;
    private GameObject _anchorPoint;
    private List<IObserver> _observers;
    // Start is called before the first frame update

    private void Awake() {
        _observers = new List<IObserver>();
    }

    void Start()
    {
        _anchorPoint = FindNearestAnchor();
        _stateMachine = new BasicEnemyStateMachine(gameObject, _anchorPoint, GameObject.Find("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        _stateMachine.ExecuteStateUpdate();
    }

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach(IObserver observer in _observers) {
            observer.Update();
        }
    }

    private GameObject FindNearestAnchor() {
        float min = GameObject.FindGameObjectsWithTag("Anchor").ToList().Min(x => (x.transform.position - transform.position).magnitude);
        return GameObject.FindGameObjectsWithTag("Anchor").ToList().Find(x => (x.transform.position - transform.position).magnitude == min);
    }

    public void TakeDamage(float damage) {
        _hp -= damage;
        if (_hp <= 0) {
            NotifyObservers();
            int i = GameManager.Instance.Enemies.IndexOf(this);
            GameManager.Instance.Enemies[i] = null;
            Destroy(gameObject);
        }
    }

}
