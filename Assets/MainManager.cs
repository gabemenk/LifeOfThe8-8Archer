using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    [SerializeField] public static MainManager Instance;
    [SerializeField] public float timeRemaining = 300;
    [SerializeField] public int reindeerHits = 0;
    [SerializeField] public string playerName = "unnamed";
    [SerializeField] public int score = 0;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public string GetName()
    {
        return playerName;
    }
    public int GetScore()
    {
        return score;   
    }
}
