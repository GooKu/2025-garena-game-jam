using GModule;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private int score;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        MessageEventSystem.Register(EventKey.NewGame, newGameHandle);
        MessageEventSystem.Register(EventKey.UpdateScore, updateScoreHandle);
    }

    private void newGameHandle(string arg1, object[] arg2)
    {
        score = 0;
        SceneManager.LoadScene("Stage01");
    }

    private void updateScoreHandle(string arg1, object[] arg2)
    {
        int deltaScore = (int)arg2[0];
        score += deltaScore;
    }
}
