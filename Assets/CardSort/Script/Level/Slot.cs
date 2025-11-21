using System.Collections;
using System.Collections.Generic;
using Bus;
using DG.Tweening;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public List<Card> CardLst;
    public ColorType _topCardColor;
    public SlotStatus currentStatu;
    public bool IsDealTable;
    public BoxCollider _collider;

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
        UpdateCollider();
    }
    public void RemoveCard(Card card)
    {
        CardLst.Remove(card);
        UpdateTopColor();
        UpdateCollider();
    }
    public void UpdateTopColor()
    {
        _topCardColor = CardLst.Count == 0 ? ColorType.Empty : CardLst[^1]._color;
    }
    public void UpdateCollider()
    {
        _collider.size = new Vector3(_collider.size.x, CardLst.Count * Config.cardPositionOffsetY, _collider.size.z);
        _collider.center = new Vector3(_collider.center.x, _collider.size.y / 2, _collider.center.z);

    }

    public void OnCompletAddCardToDealButton()
    {
        int diff = CardLst.Count - 20;
        if (diff <= 0) return;
        _collider.enabled = false;

        for (int i = 0; i < diff; i++)
        {
            GameObject t = CardLst[i].gameObject;
            t.SetActive(false);
        }
        CardLst.RemoveRange(0, diff);
        float targetY = 0;
        Sequence sequence = DOTween.Sequence();
        foreach (var t in CardLst)
        {
            sequence.Join(t.transform.DOMoveY(targetY, 0.1f));
            targetY += Config.cardPositionOffsetY;
        }
        sequence.OnComplete(() =>
        {
            _collider.enabled = true;
        });


    }
}
