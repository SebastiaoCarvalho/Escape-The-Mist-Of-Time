using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameData GameData;

    public void LoadPlanningMenu() {
        GameData.CleanData();
        GameData.DebugData();
        SceneManager.LoadSceneAsync("PlanningMenu");
    }

    public void QuitGame()
    {
        GameData.CleanData();
        Application.Quit();
    }
}
