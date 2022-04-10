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
    private Animator animator;

    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 720f;
    private float horizontalInput;
    private float verticalInput;
    void Start()
    {
        animator = GetComponent<Animator>();
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


        //playerRb.velocity = new Vector3(moveForce * horizontalInput, playerRb.velocity.y, moveForce * verticalInput);
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        print(movementDirection);
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

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

        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.RestartLevel();
        }

    }

    void Death(bool condition)
    {
        //GetComponent<MeshRenderer>().enabled = !condition;
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = !condition;
        GetComponent<Rigidbody>().isKinematic = condition;
    }
}
