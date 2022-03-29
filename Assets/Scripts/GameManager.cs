using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerLoseLife()
    {
        Debug.Log("called");
        Invoke("RestartLevel", 1.3f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LerpPlatformColor(GameObject Platform)
    {
        var currentColor = Platform.GetComponent<MeshRenderer>().material.color;
        var DangerColor = Color.red;
        Platform.GetComponent<MeshRenderer>().material.color = Color.Lerp(currentColor, DangerColor, 0.01f);

        yield return new WaitForSeconds(3f);
        Platform.GetComponent<MeshRenderer>().material.color = DangerColor;
        if (Platform.GetComponent<MeshRenderer>().material.color == DangerColor)
        {
            FallPlatform(Platform);

        }
    }

    public void FallPlatform(GameObject Platform)
    {
        print("fall platform");
        if (Platform.GetComponent<Rigidbody>() != null)
            return;

        Platform.AddComponent<Rigidbody>();
        Platform.GetComponent<BoxCollider>().enabled = false;
    }
}
