using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : IObserver
{

    private string _name;
    private string _description;
    public GameObject StickyNote {get; set;}
    public string Region {get; set;}
    public List<Task> _followUpTasks = new List<Task>();

    public Task(string name, string description, string region)
    {
        _name = name;
        _description = description;
        Region = region;
        _followUpTasks = new List<Task>();
    }

    public Task(string name, string description, string region, List<Task> followUpTasks) : this(name, description, region)
    {
        _followUpTasks = followUpTasks;
    }

    public Task(string name, string description, GameObject stickyNote, string region) : this(name, description, region)
    {
        StickyNote = stickyNote;
        Region = region;
        stickyNote.name = name;
        stickyNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        stickyNote.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
        StickyNote.GetComponent<Button>().onClick.AddListener(() => TaskManager.Instance.OnClick(this));
    }


    public void Update()
    {
        if (Region == null) return;
        if (Region.Equals("InProgressRegion")) {
            GameManager.Instance.CompleteTask(this);
        } 
        else {
            Debug.Log("Task " + _name + " is not in the In Progress region"); // FIXME : talk with team to decide what to do here
        }
    }


}
