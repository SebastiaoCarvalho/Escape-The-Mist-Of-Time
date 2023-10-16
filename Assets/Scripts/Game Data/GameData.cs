using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{

    public Player player;
    public List<Enemy> enemies;
    public List<ResourceBehaviour> resources;
    public List<Upgrade> upgrades;
    public List<Task> toDoTasks;
    public List<Task> inProgressTasks;
    public List<Task> completedTasks;
    public List<Item> items;

    public void OnEnable()
    {
        player = new Player();
        enemies = new List<Enemy>();
        resources = new List<ResourceBehaviour>();
        upgrades = new List<Upgrade>();
        toDoTasks = new List<Task>();
        inProgressTasks = new List<Task>();
        completedTasks = new List<Task>();
        items = new List<Item>();
    }

    public void CleanData()
    {
        player = new Player();
        enemies = new List<Enemy>();
        resources = new List<ResourceBehaviour>();
        upgrades = new List<Upgrade>();
        toDoTasks = new List<Task>();
        inProgressTasks = new List<Task>();
        completedTasks = new List<Task>();
        items = new List<Item>();
    }
}
