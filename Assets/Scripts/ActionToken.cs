using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Timeline;
using System;

[RequireComponent(typeof(RectTransform))]
public class ActionToken : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<ActionToken> ActionEvent;

    [SerializeField] private Canvas canvas;
    [SerializeField] private TimelineAsset result;
    public TimelineAsset Result => result;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 orgPos;
    private TargetObject targetObject;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        orgPos = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //TODO
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if(targetObject != null)
        {
            targetObject.EnableOutline(false);
            targetObject = null;
        }
        foreach (var to in GameObject.FindObjectsByType<TargetObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
        {
            if (IsOverlapping(to.rectTransform))
            {
                targetObject = to;
                targetObject.EnableOutline(true);
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(targetObject == null) 
        {
            rectTransform.anchoredPosition = orgPos;
            return;
        }
        targetObject.EnableOutline(false);
        ActionEvent?.Invoke(this);
        Destroy(gameObject);
    }

    private bool IsOverlapping(RectTransform targetRect)
    {
        Rect myWorldRect = GetWorldRect(rectTransform);
        Rect targetWorldRect = GetWorldRect(targetRect);
        return myWorldRect.Overlaps(targetWorldRect);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector2 size = corners[2] - corners[0];
        return new Rect(corners[0], size);
    }
}
