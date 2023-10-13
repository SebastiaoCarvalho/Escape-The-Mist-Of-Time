using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 1.5f;
    [SerializeField] private LayerMask interactibleMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private GameManager gameManager;
    private IInteractible[] interactibles;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }



    // Update is called once per frame
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, (int)interactibleMask);

        if (numFound > 0)
        {
            interactibles = colliders[0].GetComponents<IInteractible>();

            if (colliders[0].gameObject.tag == "Respawn Point")
            {
                gameManager.safePlace = true;
            }

            foreach (var interactible in interactibles)
            {
                if (interactible != null)
                {
                    if (interactionPromptUI.CanDisplay()) 
                    {
                        interactionPromptUI.SetUp(interactible.InteractionPrompt);
                    }
                    
                    if (interactible.KeyPressed())
                    {
                        interactible.Interact(this);
                    }
                }
            }
        }
        else
        {
            if (interactibles != null)
            {
                interactibles = null;
            }
            if (interactionPromptUI.isDisplayed)
            {
                interactionPromptUI.Close();
            }
            if (gameManager.safePlace)
            {
                gameManager.safePlace = false;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
