using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameData GameData;

    //chance to mapManager
    [SerializeField] private GameObject MapScreen;
    [SerializeField] private UpgradeManager UpgradeManager;
    [SerializeField] private TaskManager TaskManager;
    //chance to inventoryManager
    [SerializeField] private GameObject InventoryScreen;



    // Start is called before the first frame update
    void Start()
    {
        //In a "real game" we would want GameData to be persistent, but for testing, we need to reset it
        GameData.CleanData();

        GameData.toDoTasks = new List<Task>(){
            new Task("Task D", "survive you dumbass", Instantiate(TaskManager.StickyNotePrefab), TaskManager._toDoRegion.name),
            new Task("Task C", "Just die please", Instantiate(TaskManager.StickyNotePrefab), TaskManager._toDoRegion.name)
        };
        GameData.inProgressTasks = new List<Task>() {
            new Task("Task B", "kill 'Mr Capeta'", Instantiate(TaskManager.StickyNotePrefab), TaskManager._inProgressRegion.name)
        };
        GameData.completedTasks = new List<Task>() {
            new Task("Task A", "simply exist", Instantiate(TaskManager.StickyNotePrefab), TaskManager._completedRegion.name)
        };

        UpgradeManager._head = new Upgrade("Speed boost", "Boost your speed", 0, Instantiate(UpgradeManager.UpgradePrefab));
        GameData.upgrades = new List<Upgrade>()
        {
            UpgradeManager._head,
            new Upgrade("Mind kill", "Boost your psychic powers", 1, UpgradeManager._head, Instantiate(UpgradeManager.UpgradePrefab)),
            new Upgrade("Flying", "Spread your wings", 1, UpgradeManager._head, Instantiate(UpgradeManager.UpgradePrefab))
        };


    }

    // Update is called once per frame
    void Update()
    {
        //Do I need this here?
        UpdateTask();
        UpdateUpgrades();
    }

    void UpdateTask()
    {
        TaskManager._toDoTasks = GameData.toDoTasks;
        TaskManager._inProgressTasks = GameData.inProgressTasks;
        TaskManager._completedTasks = GameData.completedTasks;
    }

    void UpdateUpgrades()
    {

    }
}
