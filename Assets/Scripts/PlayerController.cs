using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerRb;

    [Header("Player Movement")]
    public float moveForce = 5f;
    public float jumpForce = 5f;
    private float horizontalInput;
    private float verticalInput;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        playerRb.velocity = new Vector3(moveForce * horizontalInput, playerRb.velocity.y, moveForce * verticalInput);

        if (Input.GetButtonDown("Jump"))
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
    }
}
