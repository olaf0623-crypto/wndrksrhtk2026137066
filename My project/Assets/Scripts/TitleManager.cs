using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPanel;

    public void GamesStart()
    {
        SceneManager.LoadScene("PlayScene_Door1");
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
}