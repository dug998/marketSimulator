using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bus;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class DealButton : MonoBehaviour
{
    public int numberSpawn;
    public Transform spawnPoint;

    private bool IsHandleTap;

    public void OnMouseDown()
    {
        HandleTap();
    }

    public void HandleTap()
    {
        if (IsHandleTap) return;
        StartCoroutine(IHandleTap());
    }
    private IEnumerator IHandleTap()
    {
        IsHandleTap = true;

        EventGame.Game.OnTapDealButton.Invoke();

        var slots = LevelManager.Current._slotCtrl.SlotActions();

        transform.DOScaleY(0.5f, 0.2f).OnComplete(() => transform.DOScaleY(1.25f, 0.2f));

        for (int i = 0; i < slots.Count; i++)
        {
            yield return new WaitForSeconds(Config.delayBetweenSlots);
            SendCardsToSlot(slots[i]);
        }
        yield return new WaitForSeconds(LevelManager.LevelSetup.DurationMoveCard);
        IsHandleTap = false;
    }
    private void SendCardsToSlot(Slot targetSlot)
    {
        float offset = targetSlot.CardLst.Count == 0 ? 0 : targetSlot.CardLst.Last().transform.position.y + Config.cardPositionOffsetY;

        ColorType targetColor = targetSlot.GetTopColor();
        List<ColorType> colourOptions = new List<ColorType>(LevelManager.LevelSetup.cardColors);
        colourOptions.Remove(targetColor);
        int indexColor = Random.Range(0, colourOptions.Count);

        float timeDelay = 0;
        for (int i = 0; i < numberSpawn; i++)
        {
            var obj = Instantiate(LevelManager.LevelSetup.GetCard(colourOptions[indexColor]), LevelManager.LevelSetup.HolderCard);
            obj.transform.position = spawnPoint.position;
            int index = i;
            obj.PlayAnimationJump(targetSlot, offset, timeDelay).OnComplete(() =>
            {
                if (index == numberSpawn - 1)
                {
                    targetSlot.OnCompletAddCardToDealButton();
                }
            });
            timeDelay += Config.DelayBetweenCards;
            offset += Config.cardPositionOffsetY;
            targetSlot.AddCard(obj);
        }

    }
}
