using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private GameManager gameManager;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 7.0f;
    public float lastHorizontalValue;
    public float lastVerticalValue;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SetRespawnPosition(transform.position);
    }

    private void Update() {
        this.gameObject.transform.eulerAngles = Vector3.zero;  // fix rotation
    }

    // player shouldn't move when time hit 0
    // trigger smoke covering player
    // followed by respawn
    // smoke opening again

    void FixedUpdate()
    {
        if (gameManager.timeOut)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), -9.81f, Input.GetAxis("Vertical"));

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            if (move.x != 0 || move.z != 0)
            {
                gameManager.UpdateTime(-Time.deltaTime);
            }
        }

        if (Input.GetAxis("Horizontal") == 0) {
            lastHorizontalValue = 0;
        }
        else if (Input.GetAxis("Horizontal") > 0) {
            lastHorizontalValue = 1;
        }
        else {
            lastHorizontalValue = -1;
        }

        if (Input.GetAxis("Vertical") == 0) {
            lastVerticalValue = 0;
        }
        else if (Input.GetAxis("Vertical") > 0) {
            lastVerticalValue = 1;
        }
        else {
            lastVerticalValue = -1;
        }
    }

    public void Respawn(Vector3 position)
    {
        controller.enabled = false;
        controller.transform.position = position;
        controller.enabled = true;
        lastHorizontalValue = 0;
        lastVerticalValue = 0;
    }
}
