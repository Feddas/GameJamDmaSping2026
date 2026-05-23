using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    public void GoToIntroScene()
    {
        // Replace "YourSceneName" with the actual name of your scene
        SceneManager.LoadScene("IntroScene");
    }
}
