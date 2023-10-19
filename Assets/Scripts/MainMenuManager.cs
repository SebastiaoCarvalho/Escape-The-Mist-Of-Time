using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; set; }
    private Dictionary<string, GameObject> _buttons;
    private ButtonEnum _currentButton = ButtonEnum.Unsigned;
    [SerializeField] private GameData GameData;


    private enum ButtonEnum
    {
        Start,
        Quit,
        Unsigned
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _buttons = new Dictionary<string, GameObject>();

        GameObject.FindGameObjectsWithTag("MainMenuButton").ToList().ForEach(button =>
        {
            _buttons.Add(button.name, button);
        });
    }

    public void LoadPlanningMenu()
    {
        GameData.CleanData();
        SceneManager.LoadSceneAsync("PlanningMenu");
    }

    public void QuitGame()
    {
        GameData.CleanData();
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if(_currentButton == ButtonEnum.Unsigned)
            {
                _currentButton = ButtonEnum.Start;
                EventSystem.current.SetSelectedGameObject(_buttons["Play"]);
            }
            else if(_currentButton == ButtonEnum.Start)
            {
                _currentButton = ButtonEnum.Quit;
                EventSystem.current.SetSelectedGameObject(_buttons["Quit"]);
            }
            else if(_currentButton == ButtonEnum.Quit)
            {
                _currentButton = ButtonEnum.Start;
                EventSystem.current.SetSelectedGameObject(_buttons["Play"]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (_currentButton == ButtonEnum.Quit)
            {
                _currentButton = ButtonEnum.Start;
                EventSystem.current.SetSelectedGameObject(_buttons["Play"]);
            }
            else if(_currentButton == ButtonEnum.Start)
            {
                _currentButton = ButtonEnum.Quit;
                EventSystem.current.SetSelectedGameObject(_buttons["Quit"]);
            }

        }
    }
}
