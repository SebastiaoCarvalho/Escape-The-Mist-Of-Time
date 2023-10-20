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
    private ButtonEnum _currentButtonKeyIterator = ButtonEnum.Unsingned;
    private ButtonEnum _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;

    private enum ButtonEnum
    {
        MapButton,
        UpgradesButton,
        TasksButton,
        InventoryButton,
        MainMenuButton,
        ExitButton,
        Unsingned
    }

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
        _currentButtonKeyIterator = ButtonEnum.MapButton;
        _previousScreen = _screens["MapScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickUpgrades()
    {
        if (_previousScreen != null)
        {
            _previousScreen.SetActive(false);
        }
        _currentButtonKeyIterator = ButtonEnum.UpgradesButton;
        _previousScreen = _screens["UpgradesScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickTasks()
    {
        if (_previousScreen != null)
        {
            _previousScreen.SetActive(false);
        }
        _currentButtonKeyIterator = ButtonEnum.TasksButton;
        _previousScreen = _screens["TasksScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickInventory()
    {
        if (_previousScreen != null)
        {
            _previousScreen.SetActive(false);
        }
        _currentButtonKeyIterator = ButtonEnum.InventoryButton;
        _previousScreen = _screens["InventoryScreen"];
        _previousScreen.SetActive(true);
    }

    public void OnClickMapIconButton()
    {
        //Get button that was pressed through EventSystem.current.currentSelectedGameObject
        GameObject button = EventSystem.current.currentSelectedGameObject;

        GameObject hierarchy = GameObject.Find("MapBackground");

        //Put button on top of hierarchy


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


            if (_currentButtonKeyIteratorTop == ButtonEnum.MainMenuButton)
            {
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
                _currentButtonKeyIterator = ButtonEnum.MapButton;
                _previousScreen = _screens["MapScreen"];
                _previousScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_buttons["MapButton"]);
            }
            else if (_currentButtonKeyIteratorTop == ButtonEnum.ExitButton)
            {
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
                _currentButtonKeyIterator = ButtonEnum.InventoryButton;
                _previousScreen = _screens["InventoryScreen"];
                _previousScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_buttons["InventoryButton"]);
            }
            //First when we enter the scene
            else if (_currentButtonKeyIteratorTop == ButtonEnum.Unsingned)
            {
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
                _currentButtonKeyIterator = ButtonEnum.MapButton;
                _previousScreen = _screens["MapScreen"];
                _previousScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_buttons["MapButton"]);
            }

            //Shouldn't arrive here, just a failsafe
            _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;


        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {

            if (_previousScreen != null && _currentButtonKeyIteratorTop == ButtonEnum.Unsingned)
            {
                _previousScreen.SetActive(false);
            }
            if (_currentButtonKeyIterator == ButtonEnum.MapButton || _currentButtonKeyIterator == ButtonEnum.UpgradesButton)
            {
                _currentButtonKeyIterator = ButtonEnum.Unsingned;
                _currentButtonKeyIteratorTop = ButtonEnum.MainMenuButton;
                EventSystem.current.SetSelectedGameObject(_buttons["MainMenuButton"]);
            }
            else if (_currentButtonKeyIterator == ButtonEnum.TasksButton || _currentButtonKeyIterator == ButtonEnum.InventoryButton)
            {
                _currentButtonKeyIterator = ButtonEnum.Unsingned;
                _currentButtonKeyIteratorTop = ButtonEnum.ExitButton;
                EventSystem.current.SetSelectedGameObject(_buttons["ExitButton"]);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            _currentButtonKeyIterator = ButtonEnum.Unsingned;

        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))  //This does not support gamepad input
        {
            if (_currentButtonKeyIterator == ButtonEnum.InventoryButton)
            {
                _currentButtonKeyIterator = ButtonEnum.MapButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIterator == ButtonEnum.MapButton)
            {
                _currentButtonKeyIterator = ButtonEnum.UpgradesButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIterator == ButtonEnum.UpgradesButton)
            {
                _currentButtonKeyIterator = ButtonEnum.TasksButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIterator == ButtonEnum.TasksButton)
            {
                _currentButtonKeyIterator = ButtonEnum.InventoryButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIteratorTop == ButtonEnum.MainMenuButton)
            {
                _currentButtonKeyIteratorTop = ButtonEnum.ExitButton;
                _currentButtonKeyIterator = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIteratorTop == ButtonEnum.ExitButton)
            {
                _currentButtonKeyIteratorTop = ButtonEnum.MainMenuButton;
                _currentButtonKeyIterator = ButtonEnum.Unsingned;
            }



            if (_previousScreen != null)
            {
                _previousScreen.SetActive(false);
            }


            switch (_currentButtonKeyIterator)
            {
                case ButtonEnum.MapButton:
                    _previousScreen = _screens["MapScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["MapButton"]);
                    break;
                case ButtonEnum.UpgradesButton:
                    _previousScreen = _screens["UpgradesScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["UpgradesButton"]);
                    break;
                case ButtonEnum.TasksButton:
                    _previousScreen = _screens["TasksScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["TasksButton"]);
                    break;
                case ButtonEnum.InventoryButton:
                    _previousScreen = _screens["InventoryScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["InventoryButton"]);
                    break;
            }

            switch (_currentButtonKeyIteratorTop)
            {
                case ButtonEnum.MainMenuButton:
                    EventSystem.current.SetSelectedGameObject(_buttons["MainMenuButton"]);
                    break;
                case ButtonEnum.ExitButton:
                    EventSystem.current.SetSelectedGameObject(_buttons["ExitButton"]);
                    break;
            }


        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))    //This does not support gamepad input
        {
            if (_currentButtonKeyIterator == ButtonEnum.InventoryButton)
            {
                _currentButtonKeyIterator = ButtonEnum.TasksButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIterator == ButtonEnum.MapButton)
            {
                _currentButtonKeyIterator = ButtonEnum.InventoryButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIterator == ButtonEnum.UpgradesButton)
            {
                _currentButtonKeyIterator = ButtonEnum.MapButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIterator == ButtonEnum.TasksButton)
            {
                _currentButtonKeyIterator = ButtonEnum.UpgradesButton;
                _currentButtonKeyIteratorTop = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIteratorTop == ButtonEnum.MainMenuButton)
            {
                _currentButtonKeyIteratorTop = ButtonEnum.ExitButton;
                _currentButtonKeyIterator = ButtonEnum.Unsingned;
            }
            else if (_currentButtonKeyIteratorTop == ButtonEnum.ExitButton)
            {
                _currentButtonKeyIteratorTop = ButtonEnum.MainMenuButton;
                _currentButtonKeyIterator = ButtonEnum.Unsingned;
            }

            if (_previousScreen != null)
            {
                _previousScreen.SetActive(false);
            }

            switch (_currentButtonKeyIterator)
            {
                case ButtonEnum.MapButton:
                    _previousScreen = _screens["MapScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["MapButton"]);
                    break;
                case ButtonEnum.UpgradesButton:
                    _previousScreen = _screens["UpgradesScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["UpgradesButton"]);
                    break;
                case ButtonEnum.TasksButton:
                    _previousScreen = _screens["TasksScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["TasksButton"]);
                    break;
                case ButtonEnum.InventoryButton:
                    _previousScreen = _screens["InventoryScreen"];
                    _previousScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_buttons["InventoryButton"]);
                    break;
            }

            switch (_currentButtonKeyIteratorTop)
            {
                case ButtonEnum.MainMenuButton:
                    EventSystem.current.SetSelectedGameObject(_buttons["MainMenuButton"]);
                    break;
                case ButtonEnum.ExitButton:
                    EventSystem.current.SetSelectedGameObject(_buttons["ExitButton"]);
                    break;
            }


        }
    }
}


