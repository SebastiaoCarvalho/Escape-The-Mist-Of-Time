using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameData GameData;

    void Awake()
    {
        GameData.DebugData();
        //In a "real game" we would want GameData to be persistent, but for testing, we need to reset it
        if (GameData.IsClean)
        {
            InitializeGameData();
        }
    }

    private void InitializeGameData() {
        GameData.ToDoTasks = new List<Task>(){
            new Task("Task D", "survive you dumbass", "ToDoRegion"),
            new Task("Task C", "Just die please",  "ToDoRegion")
        };
        GameData.InProgressTasks = new List<Task>() {
            new Task("Task B", "kill 'Mr Capeta'", "InProgressRegion")
        };
        GameData.CompletedTasks = new List<Task>() {
            new Task("Task A", "simply exist", "CompletedRegion")
        };

        var head = new Upgrade("Speed boost", "Boost your speed", 0, Instantiate(UpgradeManager.Instance.UpgradePrefab));
        GameData.Upgrades = new List<Upgrade>()
        {
            head,
            new Upgrade("Mind kill", "Boost your psychic powers", 1, head, Instantiate(UpgradeManager.Instance.UpgradePrefab)),
            new Upgrade("Flying", "Spread your wings", 1, head, Instantiate(UpgradeManager.Instance.UpgradePrefab))
        };

        GameData.Items= new List<Item>()
        {
            new Item("Stone", "This is a stone"),
            new Item("Stone", "This is a stone"),
            new Item("Stone", "This is a stone"),
        };
    }

    public void Clean() {
        GameData.CleanData();
    }

}
