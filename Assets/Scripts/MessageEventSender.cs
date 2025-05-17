using GModule;
using UnityEngine;

public class MessageEventSender : MonoBehaviour
{
    public void Send(string key)
    {
        MessageEventSystem.Notify(key);
    }
}
