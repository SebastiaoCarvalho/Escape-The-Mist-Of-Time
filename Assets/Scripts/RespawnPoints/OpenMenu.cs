using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenMenu : MonoBehaviour, IInteractible
{
    private GameManager gameManager;
    [SerializeField] private string prompt;
    [SerializeField] private string key;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public bool Interact(Interactor interactor) {
        // switch scene
        Debug.Log("Open");
        gameManager.OpenMenu();
        return true;
    }

    public bool KeyPressed() {
        return Input.GetKeyDown(key);
    }
}
