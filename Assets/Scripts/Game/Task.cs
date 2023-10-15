using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task 
{

    private string _name;
    private string _description;

    public GameObject StickyNote {get; set;}
    public GameObject Region {get; set;}

    public Task(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public Task(string name, string description, GameObject stickyNote, GameObject region) : this(name, description)
    {
        StickyNote = stickyNote;
        Region = region;
        stickyNote.name = name;
        stickyNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        stickyNote.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
        StickyNote.GetComponent<Button>().onClick.AddListener(() => TaskManager.Instance.OnClick(this));
    }

}
