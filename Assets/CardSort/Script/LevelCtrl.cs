using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LevelCtrl : MonoSingleton<LevelCtrl>
{
    public static Slot FromSlot
    {
        get
        {
            return Instance.fromSlot;
        }
        set { Instance.fromSlot = value; }
    }
    public static Slot ToSlot
    {
        get
        {
            return Instance.toSlot;
        }
        set { Instance.toSlot = value; }
    }

    public Slot fromSlot;
    public Slot toSlot;

    public DealTable _dealTable;
    private void OnEnable()
    {
        EventGame.Game.OnTapSlot += OnTapSlot;
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        SlotCtrl.Instance.Init();
        _dealTable.Init(5);
    }
    private void OnTapSlot(Slot slot)
    {

        if (FromSlot == slot) return;
        if (FromSlot == null)
        {
            if (slot.IsDealTable) return;
            FromSlot = slot;
            ToSlot = null;
            return;
        }
        else if (FromSlot != slot)
        {
            ToSlot = slot;
        }
        if (FromSlot._topCardColor != ToSlot._topCardColor && ToSlot._topCardColor != ColorType.Empty)
        {
            FromSlot = null;
            ToSlot = null;
            return;
        }


        List<Card> FormCardLst = new List<Card>(FromSlot.CardLst);
        Stack<Card> selectCard = new Stack<Card>();

        foreach (Card card in FormCardLst)
        {
            if (card._color == FromSlot._topCardColor)
            {
                selectCard.Push(card);
            }

        }
        float delay = 0;
        int cardCount = selectCard.Count;
        var _offset = ToSlot.CardLst.Count == 0 ? 0 : ToSlot.CardLst.Last().transform.position.y + Config.cardPositionOffsetY;

        for (int i = 0; i < cardCount; i++)
        {
            Card last = selectCard.Pop();
            FromSlot.RemoveCard(last);
            last.PlayAnimation(ToSlot, _offset, delay).OnComplete(
                () =>
                {
                    Debug.Log(ToSlot.name);
                    if (ToSlot.IsDealTable && ToSlot is DealTable table)
                    {
                        table.UpdateProgress();
                    }

                });
            ToSlot.AddCard(last);
            delay += Config.DelayBetweenCards; //0.075f
            _offset += Config.cardPositionOffsetY;
        }
        FromSlot = null;
       // ToSlot = null;

    }
}
