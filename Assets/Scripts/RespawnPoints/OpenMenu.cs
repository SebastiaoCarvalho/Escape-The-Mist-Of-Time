using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour, IInteractible
{
    private GameManager gameManager;
    [SerializeField] private string prompt;
    [SerializeField] private string key;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor) {
        // switch scene
        Debug.Log("Open");
        SceneManager.LoadSceneAsync(1);
        return true;
    }

    public bool KeyPressed() {
        return Input.GetKeyDown(key);
    }
}
