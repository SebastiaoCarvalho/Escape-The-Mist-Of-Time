using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 7.0f;
    public float lastHorizontalValue;
    public float lastVerticalValue;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
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
}
