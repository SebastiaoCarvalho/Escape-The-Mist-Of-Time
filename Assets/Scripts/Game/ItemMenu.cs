using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenu
{

    private string name;
    public int amount;

    public GameObject StickyNote { get; set; }
    public GameObject Region { get; set; }

    public ItemMenu(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    }


    public ItemMenu(string name, int amount, GameObject stickyNote, GameObject region)
    {
        StickyNote = stickyNote;
        Region = region;
        stickyNote.name = name;
        stickyNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        stickyNote.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Amount: " + amount.ToString();
        //StickyNote.GetComponent<Button>().onClick.AddListener(() => TaskManager.Instance.OnClick(this));

    }
}

