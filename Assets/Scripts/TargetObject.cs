using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TargetObject : MonoBehaviour
{
    [SerializeField] private Outline outline;
    public RectTransform rectTransform {  get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (outline == null) { outline = gameObject.AddComponent<Outline>(); }
        outline.enabled = false;
    }

    public void EnableOutline(bool isEnable)
    {
        outline.enabled = isEnable;
    }
}
