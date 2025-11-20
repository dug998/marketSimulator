using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bus;
using Sirenix.OdinInspector;
using UnityEngine;

public class DealButton : MonoBehaviour
{
    public int numberSpawn;
    public Transform spawnPoint;
    public int indexSlot;
    [Button]
    public void HandleTap()
    {
        SendCardsToSlot(SlotCtrl.Instance.slots[indexSlot]);
    }
    IEnumerator ISendCard(Slot slot)
    {
        yield return null;
        SendCardsToSlot(slot);
    }
    private void SendCardsToSlot(Slot targetSlot)
    {
        float offset = targetSlot.CardLst.Count == 0 ? 0 : targetSlot.CardLst.Last().transform.position.y + Config.cardPositionOffsetY;

        ColorType targetColor = targetSlot.GetTopColor();
        List<ColorType> colourOptions = new List<ColorType>(LevelSetup.Instance.cardColors);
        colourOptions.Remove(targetColor);
        int index = Random.Range(0, colourOptions.Count);

        float timeDelay = 0;
        for (int i = 0; i < numberSpawn; i++)
        {
            var obj = Instantiate(LevelSetup.GetCard(colourOptions[index]), LevelSetup.Instance.HolderCard);
            obj.transform.position = spawnPoint.position;

            obj.PlayAnimation(targetSlot, offset, timeDelay);
            timeDelay += Config.DelayBetweenCards;
            offset += Config.cardPositionOffsetY;
            targetSlot.AddCard(obj);
        }
        Debug.Log("aaa");
    }
}
