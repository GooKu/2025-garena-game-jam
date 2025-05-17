using GModule;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    [SerializeField] private GameObject ending1;
    [SerializeField] private GameObject ending2;

    void Start()
    {
        MessageEventSystem.Register(EventKey.ShowEnding, showEndingHandle);
    }

    private void showEndingHandle(string arg1, object[] arg2)
    {
        bool isEnding1 = (bool)arg2[0];
        if (isEnding1)
        {
            ending1.SetActive(true);
        }
        else
        {
            ending2.SetActive(true);
        }
    }
}
