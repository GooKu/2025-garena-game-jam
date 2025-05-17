using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject optionGroup;
    [SerializeField] private string nextScene;

    private int score;

    void Start()
    {
        targetObject.gameObject.SetActive(false);
        optionGroup.gameObject.SetActive(false);
        foreach (var at in GameObject.FindObjectsByType<ActionToken>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            at.ActionEvent += actionHandle;
        }
    }

    private void actionHandle(ActionToken actionToken)
    {
        director.Play(actionToken.Result);
        score += actionToken.Score;
    }

    public void ShowOptions(int index)
    {
        targetObject.gameObject.SetActive(true);
        optionGroup.SetActive(true);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
