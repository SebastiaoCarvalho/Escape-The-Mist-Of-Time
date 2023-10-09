using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    private List<Task> _toDoTasks;
    private GameObject _toDoRegion;
    private List<Task> _inProgressTasks;
    private GameObject _inProgressRegion;
    private List<Task> _completedTasks;
    private GameObject _completedRegion;
    public GameObject StickyNotePrefab;

    // Start is called before the first frame update
    void Start()
    {
        _toDoRegion = GameObject.Find("ToDoRegion");
        _inProgressRegion = GameObject.Find("InProgressRegion");
        _completedRegion = GameObject.Find("CompletedRegion");

        _toDoTasks = new List<Task>(){
            new Task("Task D", "survive you dumbass", Instantiate(StickyNotePrefab))
        };
        _inProgressTasks = new List<Task>() {
            new Task("Task B", "kill 'Mr Capeta'", Instantiate(StickyNotePrefab))
        };
        _completedTasks = new List<Task>() {
            new Task("Task A", "simply exist", Instantiate(StickyNotePrefab))
        };
        ReloadTasks();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadTasks();
    }

    private void ReloadTasks() {
        foreach(Task task in _toDoTasks) {
            task.StickyNote.transform.SetParent(_toDoRegion.transform, false);
        }
        foreach(Task task in _inProgressTasks) {
            task.StickyNote.transform.SetParent(_inProgressRegion.transform, false);
        }
        foreach(Task task in _completedTasks) {
            task.StickyNote.transform.SetParent(_completedRegion.transform, false);
        }
    }
}
