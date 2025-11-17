using System.Collections;
using System.Collections.Generic;
using com.homemade.pattern.observer;
using TMPro;
using UnityEngine;

public class UIDetails : MonoBehaviour
{
    public GameObject ObjCrosshair => objCrosshair;

    [SerializeField] private GameObject objCrosshair;
    [SerializeField] private TextMeshProUGUI detailInteractTxt;
    private void OnEnable()
    {
        this.RegisterListener(EventID.PlayerEvent.PlayerInteractor, OnInteractor);
    }
    private void OnDisable()
    {
        this.RemoveListener(EventID.PlayerEvent.PlayerInteractor, OnInteractor);
    }
    private void OnInteractor(object param = null)
    {
        if (param != null)
        {
            detailInteractTxt.text = (param as Interactable).name;
            detailInteractTxt.gameObject.SetActive(true);
        }
        else
        {
            detailInteractTxt.text = string.Empty;
            detailInteractTxt.gameObject.SetActive(false);
        }

    }
}
