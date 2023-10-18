using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public GameObject _toDoRegion;
    public GameObject _inProgressRegion;
    public GameObject _completedRegion;

    public GameObject StickyNotePrefab;

    [SerializeField] private GameData GameData;

    public static TaskManager Instance {get; set;}

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _toDoRegion = GameObject.Find("ToDoRegion");
        _inProgressRegion = GameObject.Find("InProgressRegion");
        _completedRegion = GameObject.Find("CompletedRegion");
        InitializeTasks();
        ReloadTasks();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadTasks();
    }

    public void InitializeTasks() {
        Debug.Log(GameData.ToDoTasks.Count);
        GameData.ToDoTasks.ForEach(task => task.InitializePrefab(Instantiate(StickyNotePrefab)));
        GameData.InProgressTasks.ForEach(task => task.InitializePrefab(Instantiate(StickyNotePrefab)));
        GameData.CompletedTasks.ForEach(task => task.InitializePrefab(Instantiate(StickyNotePrefab)));
    }

    private void ReloadTasks() {
        ReloadTaskGroup(GameData.ToDoTasks, _toDoRegion);
        ReloadTaskGroup(GameData.InProgressTasks, _inProgressRegion);
        ReloadTaskGroup(GameData.CompletedTasks, _completedRegion);
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
            GameData.InProgressTasks.Remove(task);
            GameData.ToDoTasks.Add(task);
            task.Region = _toDoRegion.name;
        }
        else if (task.Region.Equals("ToDoRegion")) {
            GameData.ToDoTasks.Remove(task);
            GameData.InProgressTasks.Add(task);
            task.Region = _inProgressRegion.name;
        }
    }

}
