using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{

    private bool _isClean = true;
    public bool IsClean { get => _isClean; }
    private Player _player = null;
    public Player Player { get => _player; set { _player = value; _isClean = false; } }
    private List<Enemy> _enemies = new List<Enemy>();
    public List<Enemy> Enemies { get => _enemies; set { _enemies = value; _isClean = false; } }
    private List<ResourceBehaviour> _resources = new List<ResourceBehaviour>();
    public List<ResourceBehaviour> Resources { get => _resources; set { _resources = value; _isClean = false; } }
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

    public void CleanData()
    {
        _player = null;
        _enemies = new List<Enemy>();
        _resources = new List<ResourceBehaviour>();
        _upgrades = new List<Upgrade>();
        _toDoTasks = new List<Task>();
        _inProgressTasks = new List<Task>();
        _completedTasks = new List<Task>();
        _items = new List<Item>();
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
    }

}
