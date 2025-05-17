using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GModule;
using UnityEngine.Timeline;
using System.Collections;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TimelineAsset startTimeline;
    [SerializeField] private PlayableDirector transitionInDirector;
    [SerializeField] private TimelineAsset transitionIn;
    [SerializeField] private TimelineAsset transitionOut;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject optionGroup;
    [SerializeField] private CountDownUI countDownUI;
    [SerializeField] private float countDownTime = 10;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private string nextScene;

    private float time;

    IEnumerator Start()
    {
        targetObject.gameObject.SetActive(false);
        optionGroup.gameObject.SetActive(false);
        foreach (var at in GameObject.FindObjectsByType<ActionToken>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            at.ActionEvent += actionHandle;
        }
        countDownUI.Show(1);
        transitionInDirector.Play(transitionOut);
        yield return new WaitForSeconds(1);
        director.Play(startTimeline);
    }
    //trigger by signal
    public void StartGame()
    {
        targetObject.gameObject.SetActive(true);
        optionGroup.SetActive(true);
        StartCoroutine(countDown());
    }

    private IEnumerator countDown()
    {
        time = countDownTime;
        do
        {
            yield return null;
            time -= Time.deltaTime;
            countDownUI.Show(time/countDownTime);
        } while (time > 0);
        gameOverUI.SetActive(true);
    }
    //setup at button
    public void BeckToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void actionHandle(ActionToken actionToken)
    {
        StopAllCoroutines();
        director.Play(actionToken.Result);
        MessageEventSystem.Notify(EventKey.UpdateScore, actionToken.Score);
    }

    public void NextScene()
    {
        StartCoroutine(_nextScene());
    }
    private IEnumerator _nextScene()
    {
        transitionInDirector.Play(transitionIn);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
    }

    public void ToEnding()
    {
        StopAllCoroutines();
        StartCoroutine(_toEnding());
    }

    private IEnumerator _toEnding()
    {
        transitionInDirector.Play(transitionIn);
        yield return new WaitForSeconds(1);
        transitionInDirector.Play(transitionOut);
        MessageEventSystem.Notify(EventKey.Ending);
    }

}
