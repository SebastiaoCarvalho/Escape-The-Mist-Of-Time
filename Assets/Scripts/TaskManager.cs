using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public List<Task> _toDoTasks;
    public GameObject _toDoRegion;
    public List<Task> _inProgressTasks;
    public GameObject _inProgressRegion;
    public List<Task> _completedTasks;
    public GameObject _completedRegion;
    public GameObject StickyNotePrefab;

    public static TaskManager Instance {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _toDoRegion = GameObject.Find("ToDoRegion");
        _inProgressRegion = GameObject.Find("InProgressRegion");
        _completedRegion = GameObject.Find("CompletedRegion");

        /*_toDoTasks = new List<Task>(){
            new Task("Task D", "survive you dumbass", Instantiate(StickyNotePrefab), _toDoRegion.name),
            new Task("Task C", "Just die please", Instantiate(StickyNotePrefab), _toDoRegion.name)
        };
        _inProgressTasks = new List<Task>() {
            new Task("Task B", "kill 'Mr Capeta'", Instantiate(StickyNotePrefab), _inProgressRegion.name)
        };
        _completedTasks = new List<Task>() {
            new Task("Task A", "simply exist", Instantiate(StickyNotePrefab), _completedRegion.name)
        };*/

        _toDoTasks = new List<Task>();
        _inProgressTasks = new List<Task>();
        _completedTasks = new List<Task>();

        ReloadTasks();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadTasks();
    }

    private void ReloadTasks() {
        ReloadTaskGroup(_toDoTasks, _toDoRegion);
        ReloadTaskGroup(_inProgressTasks, _inProgressRegion);
        ReloadTaskGroup(_completedTasks, _completedRegion);
    }

    private void ReloadTaskGroup(List<Task> tasks, GameObject region) {
        RectTransform rect = region.GetComponent<RectTransform>();
        RectTransform textRect = region.transform.GetChild(0).GetComponent<RectTransform>();
        Vector3 upBorder = new Vector3(0, rect.rect.height/2 - textRect.rect.height, 0);
        foreach(Task task in tasks) {
            task.StickyNote.transform.SetParent(region.transform, false);
        }
        float step = rect.rect.height / (tasks.Count + 1);
        for (int i = 0; i < tasks.Count; i++) {
            tasks[i].StickyNote.transform.localPosition = upBorder - (i + 1) * step * Vector3.up;
        }   
    }

    public void OnClick(Task task) {
        if (task.Region.Equals("InProgressRegion")) {
            _inProgressTasks.Remove(task);
            _toDoTasks.Add(task);
            task.Region = _toDoRegion.name;
        }
        else if (task.Region.Equals("ToDoRegion")) {
            _toDoTasks.Remove(task);
            _inProgressTasks.Add(task);
            task.Region = _inProgressRegion.name;
        }
    }

}
