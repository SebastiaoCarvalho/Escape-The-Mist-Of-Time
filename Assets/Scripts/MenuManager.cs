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
        //In a "real game" we would want GameData to be persistent, but for testing, we need to reset it
        GameData.CleanData();
        
        GameData.toDoTasks = new List<Task>(){
            new Task("Task D", "survive you dumbass", Instantiate(TaskManager.Instance.StickyNotePrefab), TaskManager.Instance._toDoRegion.name),
            new Task("Task C", "Just die please", Instantiate(TaskManager.Instance.StickyNotePrefab), TaskManager.Instance._toDoRegion.name)
        };
        GameData.inProgressTasks = new List<Task>() {
            new Task("Task B", "kill 'Mr Capeta'", Instantiate(TaskManager.Instance.StickyNotePrefab), TaskManager.Instance._inProgressRegion.name)
        };
        GameData.completedTasks = new List<Task>() {
            new Task("Task A", "simply exist", Instantiate(TaskManager.Instance.StickyNotePrefab), TaskManager.Instance._completedRegion.name)
        };

        UpgradeManager.Instance._head = new Upgrade("Speed boost", "Boost your speed", 0, Instantiate(UpgradeManager.Instance.UpgradePrefab));
        GameData.upgrades = new List<Upgrade>()
    {
            UpgradeManager.Instance._head,
            new Upgrade("Mind kill", "Boost your psychic powers", 1, UpgradeManager.Instance._head, Instantiate(UpgradeManager.Instance.UpgradePrefab)),
            new Upgrade("Flying", "Spread your wings", 1, UpgradeManager.Instance._head, Instantiate(UpgradeManager.Instance.UpgradePrefab))
        };

        
    }

    
}
