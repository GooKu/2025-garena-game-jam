using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    private int score;

    void Start()
    {
        foreach(var at in GameObject.FindObjectsByType<ActionToken>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            at.ActionEvent += actionHandle;
        }
    }

    private void actionHandle(ActionToken actionToken)
    {
        director.Play(actionToken.Result);
        score += actionToken.Score;
    }
}
