using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed.");
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SettingsButton()
    {

        SceneManager.LoadScene("SettingsMenu");
    }
    public void BackButoon()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
