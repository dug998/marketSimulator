using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DealTable : Slot
{

    public Image fillImage;

    private int _limitNumberCard;
    public void Init(int limit)
    {
        _limitNumberCard = limit;
        UpdateProgress();

    }
    public void UpdateProgress()
    {
        var cardCount = CardLst.Count;
        fillImage.DOKill();
        float result = (float)cardCount / _limitNumberCard;
        fillImage.DOFillAmount(result, 0.1f);

        if (cardCount >= _limitNumberCard)
        {
            RemoveAllCard();
        }
    }
    public void RemoveAllCard()
    {
        var cardRemove = new List<Card>();
        for (int i = 0; i < _limitNumberCard; i++)
        {
            var last = CardLst.Last();
            cardRemove.Add(CardLst.Last());
            CardLst.Remove(last);
        }
        for (int i = cardRemove.Count - 1; i >= 0; i--)
        {
            Destroy(cardRemove[i].gameObject);
        }
        UpdateTopColor();
    }
}
