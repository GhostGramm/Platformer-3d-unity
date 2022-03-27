using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveForce = 5f;
    private float jumpForce = 5f;
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
        if (Input.GetKeyDown("space"))
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0);

        if (Input.GetKeyDown("up"))
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, moveForce);

        if (Input.GetKeyDown("down"))
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -moveForce);

        if (Input.GetKeyDown("left"))
            GetComponent<Rigidbody>().velocity = new Vector3(-moveForce, 0, 0);

        if (Input.GetKeyDown("right"))
            GetComponent<Rigidbody>().velocity = new Vector3(moveForce, 0, 0);
    }
}
