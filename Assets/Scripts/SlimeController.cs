using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("References")]
    public CharacterController characterController;
    private Animator animator;

    [Header("Movement")]
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpSpeed;

    private float ySpeed;
    private float originalStepOffset;
    public bool isJumping;
    public bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        originalStepOffset = characterController.stepOffset;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * movementSpeed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            print("Character is grounded");
            isGrounded = true;
            animator.SetBool("IsGrounded", true);

            animator.SetBool("IsJumping", false);
            isJumping = false;

            animator.SetBool("IsFalling", false);
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                print("Jump button pressed");
                animator.SetBool("IsJumping", true);
                isJumping = true;
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsFalling", true);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }

            characterController.stepOffset = 0;
        }


        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        transform.Translate(movementDirection * magnitude * Time.deltaTime, Space.World);
        characterController.Move(velocity * Time.deltaTime);

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

    }
}
