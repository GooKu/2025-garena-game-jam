using GModule;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip menuBGM;
    [SerializeField] private AudioClip gameBGM;
    [SerializeField] private AudioClip ending1BGM;
    [SerializeField] private AudioClip ending2BGM;

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
        MessageEventSystem.Register(EventKey.EntryMenu, entryMenuHandle);
        MessageEventSystem.Register(EventKey.StartOpeningNarrative, StartOpeningNarrativeHandle);
        MessageEventSystem.Register(EventKey.NewGame, newGameHandle);
        MessageEventSystem.Register(EventKey.UpdateScore, updateScoreHandle);
        MessageEventSystem.Register(EventKey.Ending, endingHandle);
    }

    private void StartOpeningNarrativeHandle(string arg1, object[] arg2)
    {
        playBGM(gameBGM);
    }

    private void entryMenuHandle(string arg1, object[] arg2)
    {
        playBGM(menuBGM);
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
        if (isEnding1)
        {
            playBGM(ending1BGM);
        }
        else
        {
            playBGM(ending2BGM);
        }
    }

    private void playBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
