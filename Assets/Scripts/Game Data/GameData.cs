using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{

    public Player player = new Player();
    public List<Enemy> enemies = new List<Enemy>();
    public List<ResourceBehaviour> resources = new List<ResourceBehaviour>();
    public List<Upgrade> upgrades = new List<Upgrade>();
    public List<Task> toDoTasks = new List<Task>();
    public List<Task> inProgressTasks = new List<Task>();
    public List<Task> completedTasks = new List<Task>();
    public List<Item> items = new List<Item>();

   

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
