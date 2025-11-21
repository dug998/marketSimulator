using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class LevelCtrl : MonoBehaviour
{




    public Slot FromSlot;
    public Slot ToSlot;

    public DealTable _dealTable;
    public SlotCtrl _slotCtrl;
    public LevelSetup _levelSetup;

    private Stack<Card> _selectCard = new Stack<Card>();

    public int CurrentScore;
    private void OnEnable()
    {
        EventGame.Game.OnTapSlot += OnTapSlot;
        EventGame.Game.OnTapDealButton += OnTapDealButton;
    }
    public void Init()
    {
        CurrentScore = 0;

        FromSlot = null;
        ToSlot = null;
        _selectCard.Clear();

        _slotCtrl.Init();
        _dealTable.Init(5);
    }
    private void OnTapSlot(Slot slot)
    {

        if (FromSlot == slot) return;

        if (FromSlot == null)
        {
            
            FromSlot = slot;
            _selectCard.Clear();
            var FormCardLst = FromSlot.CardLst;
            for (int i = FormCardLst.Count - 1; i >= 0; i--)
            {
                if (FormCardLst[i]._color != FromSlot._topCardColor)
                {
                    break;
                }
                _selectCard.Push(FormCardLst[i]);
            }
            SetHighlighted(true, 0.1f);

            ToSlot = null;
            return;
        }
        else if (FromSlot != slot)
        {
            if (slot is DealTable dealtable && dealtable.IsRemovingCard)
            {
                return;
            }
            ToSlot = slot;
        }
        if (FromSlot._topCardColor != ToSlot._topCardColor && ToSlot._topCardColor != ColorType.Empty)
        {
            SetHighlighted(false, 0.1f);
            FromSlot = null;
            ToSlot = null;
            return;
        }
        var cardFlys = new Stack<Card>(_selectCard);

        float delay = 0;
        int cardCount = cardFlys.Count;
        var _offset = ToSlot.CardLst.Count == 0 ? 0 : ToSlot.CardLst.Last().transform.position.y + Config.cardPositionOffsetY;

        for (int i = 0; i < cardCount; i++)
        {
            Card last = cardFlys.Pop();
            FromSlot.RemoveCard(last);
            int index = i;
            ToSlot.AddCard(last);

            last.PlayAnimationJump(ToSlot, _offset, delay).OnComplete(
               () =>
               {
                   if (ToSlot.IsDealTable && ToSlot is DealTable table)
                   {
                       if (index == cardCount - 1)
                       {
                           Debug.Log(" card last" + ToSlot._topCardColor.ToString());
                       }
                       table.UpdateProgress(index == cardCount - 1);

                   }

               });
            delay += Config.DelayBetweenCards; //0.075f
            _offset += Config.cardPositionOffsetY;


        }

        if (FromSlot.IsDealTable && FromSlot is DealTable table)
        {
            table.UpdateProgress();
        }
        FromSlot = null;
        // ToSlot = null;

    }
    public void SetHighlighted(bool on, float animDuration = 0f)
    {
        if (_selectCard.Count <= 0) return;
        foreach (var card in _selectCard)
        {
            if (card == null) continue;
            card.SetHighlighted(on, animDuration);
        }
    }
    public void OnTapDealButton()
    {
        FromSlot = null;
        ToSlot = null;
        SetHighlighted(false);
    }
    public void UpdateScore(int score)
    {
        CurrentScore += score;
        EventGame.Game.OnUpdateScoreLevel.Invoke();
        if (CurrentScore >= LevelManager.LevelSetup.ScoreCardLimit)
        {
            GameManager.Instance.WinGame();
        }
    }
}
