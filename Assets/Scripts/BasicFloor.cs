using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFloor : MonoBehaviour
{
    [Header("Floor Settings")]
    public FloorType floorType = FloorType.NormalFloor;
    public bool isDestroying;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroying)
            StartCoroutine(GameManager.instance.LerpPlatformColor(gameObject));
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (floorType)
        {
            case FloorType.NormalFloor:
                if (collision.gameObject.CompareTag("Player"))
                    isDestroying = true;
                break;
            case FloorType.MovingFloor:
                if (collision.gameObject.name == "Player")
                    collision.gameObject.transform.SetParent(transform);
                break;
            default:
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        switch (floorType)
        {
            case FloorType.NormalFloor:
                break;
            case FloorType.MovingFloor:
                if (collision.gameObject.name == "Player")
                    collision.gameObject.transform.SetParent(null);
                break;
            default:
                break;
        }
    }
}
