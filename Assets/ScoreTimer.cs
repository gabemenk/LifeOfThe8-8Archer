using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreTimer : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] Text timeText;
    [SerializeField] Text sceneText;
    [SerializeField] int level;
    [SerializeField] float timeRemaining;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] int reindeerHits;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = MainManager.Instance.timeRemaining;
        reindeerHits = MainManager.Instance.reindeerHits;
        timerIsRunning = true;
        level = SceneManager.GetActiveScene().buildIndex;
        DisplayScene();
        DisplayTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        DisplayScene();
        DisplayTime();
    }

    private void DisplayTime()
    {
        int time = Mathf.FloorToInt(timeRemaining);
        timeText.text = ("Time Remaining: " + time);
    }

    private void DisplayScene()
    {
        sceneText.text = ("Level: " + level);
    }

    public void ReindeerHit()
    {
        reindeerHits++;
    }

    public void NextLevel()
    {
        MainManager.Instance.timeRemaining = timeRemaining;
        MainManager.Instance.reindeerHits = reindeerHits;
        SceneManager.LoadScene(level+1);
    }
}
