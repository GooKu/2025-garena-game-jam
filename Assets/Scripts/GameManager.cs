using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private List<GameObject> optionGroup;

    private int score;

    void Start()
    {
        targetObject.gameObject.SetActive(false);
        for (int i = 0; i < optionGroup.Count; i++)
        {
            optionGroup[i].SetActive(false);
        }
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
        for (int i = 0; i < optionGroup.Count; i++)
        {
            optionGroup[i].SetActive(i == index);
        }
    }
}
