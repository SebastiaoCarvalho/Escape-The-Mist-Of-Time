using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Escape.Game.AI;
using System.Linq;

public struct EnemyData {
    public GameObject prefab;
    public Vector3 position;
    public float hp;
    public bool givesUpgrade;
    public List<IObserver> observers;
}

public class Enemy : MonoBehaviour, IObserved
{
    private StateMachine _stateMachine;
    public float Hp { get { return _hp; } set { _hp = value; } }
    public float Damage { get { return _damage; } }
    public bool GivesUpgrade { get { return _givesUpgrade; } set { _givesUpgrade = value; } }
    public float ExtraRotation { get { return _extraRotation; } }
    public GameObject AnchorPrefab;
    private GameObject _anchorPoint;
    private List<IObserver> _observers;

    [SerializeField] private float _hp = 10;
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _chaseSpeed = 0.5f;
    [SerializeField] private float _radius = 15.0f;
    [SerializeField] private float _extraRotation = 0.0f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _mrCapeta = false;
    [SerializeField] private float _chaseDistance;
    [SerializeField] private float _lostDistance;
    [SerializeField] private bool _givesUpgrade = false;

    // Start is called before the first frame update

    private void Awake() {
        _observers = new List<IObserver>();
    }

    void Start()
    {
        _anchorPoint = FindNearestAnchor();
        if (_mrCapeta) {
            _stateMachine = new MrCapetaStateMachine(gameObject, _anchorPoint, GameObject.Find("Player"), _speed, _radius, _chaseSpeed, _chaseDistance, _lostDistance);
        }
        else {
            _stateMachine = new BasicEnemyStateMachine(gameObject, _anchorPoint, GameObject.Find("Player"), _speed, _radius, _chaseSpeed, _chaseDistance, _lostDistance);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
        Debug.Log("HP: " + _hp + " Damage: " + damage);
        _hp -= damage;
        if (_hp <= 0) {
            NotifyObservers();
            int i = GameManager.Instance.Enemies.IndexOf(this);
            GameManager.Instance.Enemies[i] = null;
            if (_givesUpgrade) {
                GameObject.Find("Player").GetComponent<Player>().AddSkillPoint();
            }
            Destroy(gameObject);
        }
        else {
            if (_audioSource != null) {
                _audioSource.Play();
            }
        }
    }

}
