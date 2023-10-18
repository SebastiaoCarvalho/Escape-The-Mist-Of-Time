using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemMenu;

public class InventoryManager : MonoBehaviour
{

    private List<ItemMenu> _items;
    public GameObject StickyNotePrefab;
    private GameObject _inventoryRegion;
    [SerializeField] private GameData gameData;
    public static InventoryManager Instance { get; set;}


    public void Start()
    {
        Instance = this;

        _inventoryRegion = GameObject.Find("InventoryRegion");

        GenerateItemMenu();

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

    private void GenerateItemMenu() {
        List<Item> items = gameData.Items;
        _items = new List<ItemMenu>();
        foreach(Item item in items) {
            ItemMenu itemMenu = _items.Find(menu => menu.Name == item.Name);
            if (itemMenu == null) {
                _items.Add(new ItemMenu(item.Name, 1, Instantiate(StickyNotePrefab), _inventoryRegion));
            } else {
                itemMenu.AddItem(item);
            }
        }
    }

}
