using GModule;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    void Start()
    {
        MessageEventSystem.Notify(EventKey.EntryMenu);
    }

    public void StartOpeningNarrative()
    {
        MessageEventSystem.Notify(EventKey.StartOpeningNarrative);
    }
}
