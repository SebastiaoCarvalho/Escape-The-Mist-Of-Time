using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject
{

    public Player player;
    public List<Enemy> enemies;
    public List<ResourceBehaviour> resources;
    public List<Upgrade> upgrades;
    public List<Task> tasks;
    public List<Item> items;

    public void OnEnable()
    {
        player = new Player();
        enemies = new List<Enemy>();
        resources = new List<ResourceBehaviour>();
        upgrades = new List<Upgrade>();
        tasks = new List<Task>();
        items = new List<Item>();
    }
}
