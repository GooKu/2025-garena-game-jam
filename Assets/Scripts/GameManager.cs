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
        MessageEventSystem.Register(EventKey.Ending, endingHandle);
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

    private void endingHandle(string arg1, object[] arg2)
    {
        bool isEnding1 = score > 0;
        MessageEventSystem.Notify(EventKey.ShowEnding, isEnding1);
    }
}
