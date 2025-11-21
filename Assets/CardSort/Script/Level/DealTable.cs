using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DealTable : Slot
{
    public bool IsRemovingCard;

    public Image fillImage;

    private int _limitNumberCard;
    public void Init(int limit)
    {
        CardLst.Clear();
        _limitNumberCard = limit;
        UpdateProgress();

    }
    public void UpdateProgress(bool isCardLast = false)
    {
        var cardCount = CardLst.Count;
        fillImage.DOKill();
        fillImage.DOFillAmount((float)cardCount / _limitNumberCard, 0.1f);
        fillImage.color = LevelManager.LevelSetup.GetColorByIdColor((int)_topCardColor);

        
        if (cardCount >= _limitNumberCard && isCardLast)
        {
            RemoveAllCard();
        }
       
    }
    public void RemoveAllCard()
    {
        StartCoroutine(IRemoveAllCard());
    }
    private IEnumerator IRemoveAllCard()
    {
        IsRemovingCard = true;
        var cardRemove = new List<Card>(CardLst);
        CardLst.Clear();
        var wait = new WaitForSeconds(0.1f);
        var color = LevelManager.LevelSetup.GetColorByIdColor((int)_topCardColor);
        for (int i = cardRemove.Count - 1; i >= 0; i--)
        {
           
            var vfx = Instantiate(LevelManager.LevelSetup.splashPrefab);
            vfx.transform.position = cardRemove[i].transform.position;
            vfx.startColor = color;
            vfx.Play();
            Destroy(cardRemove[i].gameObject);
            fillImage.fillAmount = (float)i / cardRemove.Count; 
            yield return wait;
        }
        IsRemovingCard = false;
        UpdateProgress();
        UpdateTopColor();
        LevelManager.Current.UpdateScore(1);
    }
}
