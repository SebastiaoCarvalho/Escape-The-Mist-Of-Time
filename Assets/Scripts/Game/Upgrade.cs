using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade {
    
    private Upgrade _parent;
    public Upgrade Parent { get { return _parent; } }
    private string _name;
    private string _description;
    private int _cost;
    public int Cost { get { return _cost; } }

    private bool _isPurchased = false;
    public bool IsPurchased { get { return _isPurchased; } }
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
        Object.GetComponent<Button>().onClick.AddListener(() => UpgradeManager.Instance.OnClick(this));
    }

    public Upgrade(string name, string description, int cost, Upgrade parent) : this(name, description, cost)
    {
        _parent = parent;
        _parent.AddChild(this);
    }

    public Upgrade(string name, string description, int cost, Upgrade parent, GameObject gameObject) : this(name, description, cost, parent)
    {
        
    }

    public void InitializePrefab(GameObject gameObject) {
        Object = gameObject;
        Object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _name;
        Object.GetComponent<Button>().onClick.AddListener(() => UpgradeManager.Instance.OnClick(this));
        if (_isPurchased) {
            Object.GetComponent<Image>().color = Color.green;
            Object.GetComponent<Button>().interactable = false;
        }
        else if (! UpgradeManager.Instance.IsBuyable(this)) {
            Object.GetComponent<Image>().color = Color.gray;
            Object.GetComponent<Button>().interactable = false;
        }
    }

    public void AddChild(Upgrade child)
    {
        _children.Add(child);
    }

    public Upgrade GetChild(int index)
    {
        return _children[index];
    }

    public void Purchase()
    {
        _isPurchased = true;
    }
}