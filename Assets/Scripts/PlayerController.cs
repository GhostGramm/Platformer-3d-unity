using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerRb;
    public Transform FootLocation;
    public LayerMask GroundLayer;

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

        if (Input.GetButtonDown("Jump") && IsGrounded())
            Jump();

    }

    void Jump()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(FootLocation.position, 0.1f, GroundLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player Killed");
            Death(true);
            GameManager.instance.OnPlayerLoseLife();
        }

        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Jump();
            Destroy(collision.transform.parent.parent.gameObject);
        }
    }

    void Death(bool condition)
    {
        GetComponent<MeshRenderer>().enabled = !condition;
        GetComponent<Rigidbody>().isKinematic = condition;
    }
}
