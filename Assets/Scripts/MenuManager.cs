using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameData GameData;

    [SerializeField] private GameObject MapScreen;
    [SerializeField] private GameObject UpgradeScreen;
    [SerializeField] private TaskManager TaskManager;
    [SerializeField] private GameObject InventoryScreen;
   
    

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTask();
    }

    void UpdateTask()
    {
        TaskManager._toDoTasks = GameData.toDoTasks;
        TaskManager._inProgressTasks = GameData.inProgressTasks;
        TaskManager._completedTasks = GameData.completedTasks;
       
    }
}
