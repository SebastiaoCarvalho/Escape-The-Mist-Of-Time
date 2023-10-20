using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{

    private bool _isClean = true;
    public bool IsClean { get => _isClean; }
    private PlayerData _player = new PlayerData();
    public PlayerData Player { get => _player; set { _player = value; _isClean = false; } }
    private List<EnemyData> _enemies = new List<EnemyData>();
    public List<EnemyData> Enemies { get => _enemies; set { _enemies = value; _isClean = false; } }
    private List<Vector3> _resources = new List<Vector3>(); // We use Vector3 since we only have one resource for our prototype
    public List<Vector3> Resources { get => _resources; set { _resources = value; _isClean = false; } }
    private List<Upgrade> _upgrades = new List<Upgrade>();
    public List<Upgrade> Upgrades { get => _upgrades; set { _upgrades = value; _isClean = false; } }
    private List<Task> _toDoTasks = new List<Task>();
    public List<Task> ToDoTasks { get => _toDoTasks; set { _toDoTasks = value; _isClean = false; } }
    private List<Task> _inProgressTasks = new List<Task>();
    public List<Task> InProgressTasks { get => _inProgressTasks; set { _inProgressTasks = value; _isClean = false; } }
    private List<Task> _completedTasks = new List<Task>();
    public List<Task> CompletedTasks { get => _completedTasks; set { _completedTasks = value; _isClean = false; } }
    private List<Item> _items = new List<Item>();
    public List<Item> Items { get => _items; set { _items = value; _isClean = false; } }
    private List<Vector3> _respawnPoints = new List<Vector3>();
    public List<Vector3> RespawnPoints { get => _respawnPoints; set { _respawnPoints = value; _isClean = false; } }

    public void CleanData()
    {
        _player = new PlayerData();
        _enemies = new List<EnemyData>();
        _resources = new List<Vector3>();
        _upgrades = new List<Upgrade>();
        _toDoTasks = new List<Task>();
        _inProgressTasks = new List<Task>();
        _completedTasks = new List<Task>();
        _items = new List<Item>();
        _respawnPoints = new List<Vector3>();
        _isClean = true;
    }

    public void DebugData() {
        Debug.Log("Is clean: " + _isClean);
        Debug.Log("Player: " + _player);
        Debug.Log("Enemies: " + _enemies.Count);
        Debug.Log("Resources: " + _resources.Count);
        Debug.Log("Upgrades: " + _upgrades.Count);
        Debug.Log("ToDoTasks: " + _toDoTasks.Count);
        Debug.Log("InProgressTasks: " + _inProgressTasks.Count);
        Debug.Log("CompletedTasks: " + _completedTasks.Count);
        Debug.Log("Items: " + _items.Count);
        Debug.Log("RespawnPoints: " + _respawnPoints.Count);
    }

}
