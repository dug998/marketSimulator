using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [Header(" ---  Info Level --- ")]
    public int timePlayLevel = 60;
    public int ScoreCardLimit = 10;

    [Header(" ---  Setting --- "), Space(20)]
    public float height = 1.5f;
    public Ease ease = Ease.Linear;
    public float DurationMoveCard = 0.5f;

    public Transform HolderCard;

    public ParticleSystem splashPrefab;

    public List<Card> cards;
    public List<ColorType> cardColors;
    public List<Color> colors;



    public Card GetCard(ColorType color)
    {
        return cards[(int)color % cards.Count];
    }
    public Color GetColorByIdColor(int id)
    {
        return colors[id % colors.Count];
    }

}
