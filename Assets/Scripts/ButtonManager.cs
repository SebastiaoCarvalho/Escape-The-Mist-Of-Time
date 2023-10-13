using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    private GameObject _previousScreen;
    private Dictionary<string, GameObject> _screens;
    private Dictionary<string, GameObject> _buttons;
    private int _currentButtonKeyIterator;
    private bool _inIteration = false;

    void Start()
    {
        _previousScreen = null;
        _screens = new Dictionary<string, GameObject>();
        _buttons = new Dictionary<string, GameObject>();
        GameObject.FindGameObjectsWithTag("Screen").ToList().ForEach(screen =>
        {
            _screens.Add(screen.name, screen);
            screen.SetActive(false);
        });
        GameObject.FindGameObjectsWithTag("MenuButton").ToList().ForEach(button =>
        {
            _buttons.Add(button.name, button);
        });

    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2); // FIXME : change this in the future
    }

    public void OnClickMap()
    {
        if (_previousScreen != null)
        {
            _previousScreen.SetActive(false);
        }
        _inIteration = true;
        _currentButtonKeyIterator = 0;
        _previousScreen = _screens["MapScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickUpgrades()
    {
        if (_previousScreen != null)
        {
            _previousScreen.SetActive(false);
        }
        _inIteration = true;
        _currentButtonKeyIterator = 1;
        _previousScreen = _screens["UpgradesScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickTasks()
    {
        if (_previousScreen != null)
        {
            _previousScreen.SetActive(false);
        }
        _inIteration = true;
        _currentButtonKeyIterator = 2;
        _previousScreen = _screens["TasksScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickInventory()
    {
        if (_previousScreen != null)
        {
            _previousScreen.SetActive(false);
        }
        _inIteration = true;
        _currentButtonKeyIterator = 3;
        _previousScreen = _screens["InventoryScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayGame();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _inIteration = true;
            _currentButtonKeyIterator = 0;
            if (_previousScreen != null)
            {
                _previousScreen.SetActive(false);
            }
            _previousScreen = _screens["MapScreen"];
            _previousScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_buttons["MapButton"]);
            //_buttons["MapButton"].Select();

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _inIteration = false;
            if (_previousScreen != null)
            {
                _previousScreen.SetActive(false);
            }
            if (_currentButtonKeyIterator == 0)
            {
                EventSystem.current.SetSelectedGameObject(_buttons["MainMenuButton"]);
            }
            else if (_currentButtonKeyIterator == 3)
            {
                EventSystem.current.SetSelectedGameObject(_buttons["ExitButton"]);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

        }

        //Not necessary and work a little bit weird

        /*else if (!_inIteration && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
        {

            EventSystem.current.SetSelectedGameObject(_buttons["ExitButton"]);
        }
        else if (!_inIteration && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {
            EventSystem.current.SetSelectedGameObject(_buttons["MainMenuButton"]);
        }*/
        else if (_inIteration && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))  //This does not support gamepad input
        {
            if (_currentButtonKeyIterator == 3)
            {
                _currentButtonKeyIterator = 0;
            }
            else
            {
                _currentButtonKeyIterator++;
            }

            if (_previousScreen != null)
            {
                _previousScreen.SetActive(false);
            }

            switch (_currentButtonKeyIterator % 4)
            {
                case 0:
                    _previousScreen = _screens["MapScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["MapButton"]);
                    break;
                case 1:
                    _previousScreen = _screens["UpgradesScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["UpgradesButton"]);
                    break;
                case 2:
                    _previousScreen = _screens["TasksScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["TasksButton"]);
                    break;
                case 3:
                    _previousScreen = _screens["InventoryScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["InventoryButton"]);
                    break;
            }

            _previousScreen.SetActive(true);
        }
        else if (_inIteration && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))    //This does not support gamepad input
        {
            if (_currentButtonKeyIterator == 0)
            {
                _currentButtonKeyIterator = 3;
            }
            else
            {
                _currentButtonKeyIterator--;
            }

            if (_previousScreen != null)
            {
                _previousScreen.SetActive(false);
            }

            switch (_currentButtonKeyIterator % 4)
            {
                case 0:
                    _previousScreen = _screens["MapScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["MapButton"]);
                    break;
                case 1:
                    _previousScreen = _screens["UpgradesScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["UpgradesButton"]);
                    break;
                case 2:
                    _previousScreen = _screens["TasksScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["TasksButton"]);
                    break;
                case 3:
                    _previousScreen = _screens["InventoryScreen"];
                    EventSystem.current.SetSelectedGameObject(_buttons["InventoryButton"]);
                    break;
            }

            _previousScreen.SetActive(true);
        }
    }
}


