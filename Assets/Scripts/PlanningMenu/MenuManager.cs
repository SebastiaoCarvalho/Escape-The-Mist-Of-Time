using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameData GameData;

    // Start is called before the first frame update
    void Start()
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
            new Task("Task D", "survive you dumbass", TaskManager.Instance._toDoRegion.name),
            new Task("Task C", "Just die please", TaskManager.Instance._toDoRegion.name)
        };
        GameData.InProgressTasks = new List<Task>() {
            new Task("Task B", "kill 'Mr Capeta'", TaskManager.Instance._inProgressRegion.name)
        };
        GameData.CompletedTasks = new List<Task>() {
            new Task("Task A", "simply exist", TaskManager.Instance._completedRegion.name)
        };

        UpgradeManager.Instance._head = new Upgrade("Speed boost", "Boost your speed", 0, Instantiate(UpgradeManager.Instance.UpgradePrefab));
        GameData.Upgrades = new List<Upgrade>()
        {
            UpgradeManager.Instance._head,
            new Upgrade("Mind kill", "Boost your psychic powers", 1, UpgradeManager.Instance._head, Instantiate(UpgradeManager.Instance.UpgradePrefab)),
            new Upgrade("Flying", "Spread your wings", 1, UpgradeManager.Instance._head, Instantiate(UpgradeManager.Instance.UpgradePrefab))
        };
    }

    public void Clean() {
        GameData.CleanData();
    }

}
