using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class CountDownUI : MonoBehaviour
{
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void Show(float rate)
    {
        img.enabled = true;
        img.fillAmount = rate;
    }
}
