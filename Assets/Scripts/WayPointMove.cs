using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    public Transform[] Locations;
    public float Speed = 1f;

    private int CurrentLocation = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveGameObject();
    }

    void MoveGameObject()
    {
        if (Vector3.Distance(transform.position, Locations[CurrentLocation].position) < 0.1f)
            CurrentLocation++;

        if (CurrentLocation >= Locations.Length)
            CurrentLocation = 0;

        transform.position = Vector3.MoveTowards(transform.position, Locations[CurrentLocation].position, Speed * Time.deltaTime);

    }
}
