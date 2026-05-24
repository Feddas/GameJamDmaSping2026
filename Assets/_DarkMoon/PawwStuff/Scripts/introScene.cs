using UnityEngine;
using UnityEngine.InputSystem; // 1. Make sure to add this namespace
using UnityEngine.SceneManagement;

public class introScene : MonoBehaviour
{
    void Update()
    {
        // 2. Check if the keyboard exists and if any key was pressed this frame
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            // Your existing logic to load the next scene or skip
            SceneManager.LoadScene("MainGameScene");
        }
    }
}
