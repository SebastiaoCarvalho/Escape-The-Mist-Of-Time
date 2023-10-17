using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenu
{

    private string name;
    private List<Item> _items = new List<Item>();
    public int Amount { get => _items.Count;}

    public GameObject StickyNote { get; set; }
    public GameObject Region { get; set; }

    public ItemMenu(string name)
    {
        this.name = name;
    }

    public ItemMenu(string name, int amount) : this(name)
    {
        GenerateItems(amount);
    }


    public ItemMenu(string name, GameObject stickyNote, GameObject region) : this(name)
    {
        StickyNote = stickyNote;
        Region = region;
        stickyNote.name = name;
        stickyNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        stickyNote.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Amount: " + Amount.ToString();
    }

    public ItemMenu(string name, int amount, GameObject stickyNote, GameObject region): this(name, stickyNote, region)
    {
        GenerateItems(amount);
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public void RefreshStickyNote() {
        StickyNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        StickyNote.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Amount: " + Amount.ToString();
    }

    private void GenerateItems(int amount) {
        for (int i = 0; i < amount; i++)
        {
            AddItem(new Item(name, "This is a " + name));
        }
    }
}

