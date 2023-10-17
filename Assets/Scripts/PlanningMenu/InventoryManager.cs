using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemMenu;

public class InventoryManager : MonoBehaviour
{

    private List<ItemMenu> _items;
    public GameObject StickyNotePrefab;
    private GameObject _inventoryRegion;

    public static InventoryManager Instance { get; set;}


    public void Start()
    {
        Instance = this;

        _inventoryRegion = GameObject.Find("InventoryRegion");

        _items = new List<ItemMenu>()
        {
            new ItemMenu("Stone", 3, Instantiate(StickyNotePrefab), _inventoryRegion)
        };

        ReloadItems();
    }

    public void Update()
    {
        ReloadItems();
    }


    private void ReloadItems()
    {
        foreach(ItemMenu item in _items)
        {
            item.StickyNote.transform.SetParent(_inventoryRegion.transform, false);
            item.RefreshStickyNote();
        }
    }



}
