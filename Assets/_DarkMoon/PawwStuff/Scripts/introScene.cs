using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introScene : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Object initialized. Starting automatic 5-second countdown...");
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        for (int i = 5; i > 0; i--)
        {
            Debug.Log($"Changing scene in {i} seconds...");
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Timer finished. Loading next scene now.");
        SceneManager.LoadScene("MainGameScene");
    }
}