using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupGame : MonoSingleton<UIPopupGame>
{
    public GameObject ObjCrosshair => Details.ObjCrosshair;

    public UIDetails Details => details;

    [SerializeField] private UIDetails details;
}
