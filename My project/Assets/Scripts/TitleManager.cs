using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPanel;

    public void GamesStart()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GameTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }

    public void ButtonLog()
    {
        Debug.Log("BUTTON CLICKED!");
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}