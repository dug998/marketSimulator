using System.Collections;
using System.Collections.Generic;
using Bus;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public List<Card> CardLst;
    public ColorType _topCardColor;
    public SlotStatus currentStatu;
    public bool IsDealTable;
    public void Init()
    {

    }
    private void OnMouseDown()
    {
        HandleTap();
    }
    protected virtual void HandleTap()
    {
        if (currentStatu != SlotStatus.Active) return;
        EventGame.Game.OnTapSlot.Invoke(this);
    }

    public ColorType GetTopColor()
    {
        return _topCardColor;
    }
    public void AddCard(Card card)
    {
        CardLst.Add(card);
        UpdateTopColor();
    }
    public void RemoveCard(Card card)
    {
        CardLst.Remove(card);
        UpdateTopColor();
    }
    public void UpdateTopColor()
    {
        _topCardColor = CardLst.Count == 0 ? ColorType.Empty : CardLst[^1]._color;
    }
}
