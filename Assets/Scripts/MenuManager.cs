using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }


    public void OpenLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }


    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }


    public void GoBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }


    public void ExitGame()
    {
        Application.Quit();

        // Utile mentre sviluppi dentro Unity
        Debug.Log("Chiusura gioco");
    }

}