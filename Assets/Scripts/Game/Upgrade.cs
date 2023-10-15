using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade {
    
    private Upgrade _parent;
    private string _name;
    private string _description;
    private int _cost;
    private List<Upgrade> _children = new List<Upgrade>();
    public int ChildCount { get { return _children.Count; } }
    public GameObject Object { get; set; }

    public Upgrade(string name, string description, int cost)
    {
        _name = name;
        _description = description;
        _cost = cost;
    }

    public Upgrade(string name, string description, int cost, GameObject gameObject) : this(name, description, cost)
    {
        Object = gameObject;
        Object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        Debug.Log(Object);
    }

    public Upgrade(string name, string description, int cost, Upgrade parent) : this(name, description, cost)
    {
        _parent = parent;
        _parent.AddChild(this);
    }

    public Upgrade(string name, string description, int cost, Upgrade parent, GameObject gameObject) : this(name, description, cost, parent)
    {
        Object = gameObject;
        Object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
    }

    public void AddChild(Upgrade child)
    {
        _children.Add(child);
    }

    public Upgrade GetChild(int index)
    {
        return _children[index];
    }
}