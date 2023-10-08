using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    private GameObject _previousScreen;
    private Dictionary<string, GameObject> _screens;

    void Start() {
        _previousScreen = null;
        _screens = new Dictionary<string, GameObject>();
        GameObject.FindGameObjectsWithTag("Screen").ToList().ForEach(screen => {
            _screens.Add(screen.name, screen);
            screen.SetActive(false);
        });
    }

    public void OnClickMap() {
        if (_previousScreen != null) {
            _previousScreen.SetActive(false);
        }
        _previousScreen = _screens["MapScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickUpgrades() {
        if (_previousScreen != null) {
            _previousScreen.SetActive(false);
        }
        _previousScreen = _screens["UpgradesScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickTasks() {
        if (_previousScreen != null) {
            _previousScreen.SetActive(false);
        }
        _previousScreen = _screens["TasksScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickInventory() {
        if (_previousScreen != null) {
            _previousScreen.SetActive(false);
        }
        _previousScreen = _screens["InventoryScreen"];
        _previousScreen.SetActive(true);
    }

}
