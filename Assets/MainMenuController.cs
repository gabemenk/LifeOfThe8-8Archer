using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HighScores()
    {
        SceneManager.LoadScene("High Scores");
    }
    
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void setName(string str)
    {
        MainManager.Instance.playerName = str;
    }

}
