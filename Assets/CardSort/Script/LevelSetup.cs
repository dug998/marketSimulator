using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelSetup : MonoSingleton<LevelSetup>
{
    public float height;

    public Ease ease;
    public float totalDuration;

    public Transform HolderCard;

    public List<Card> cards;
    public List<ColorType> cardColors;
   
    public static Card GetCard(ColorType color)
    {
        return Instance.cards[(int)color % Instance.cards.Count];
    }

}
