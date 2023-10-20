using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCheckpoint : MonoBehaviour, IInteractible
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
        //gameManager.switchRespawnPoint(interactor.transform.position);
        gameManager.SetRespawnPosition(interactor.transform.position);
        gameManager.SwitchCheckpoint();
        Debug.Log("Switch");
        return true;
    }

    public bool KeyPressed() {
        return Input.GetKeyDown(key);
    }
}
