using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("UI/Custom Button")]
public class CustomButton : Button
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData); // giữ behavior gốc của Button (onClick)
        PlayClickSound();
    }

    private void PlayClickSound()
    {

    }
}
