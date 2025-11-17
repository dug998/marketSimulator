using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.homemade.pattern.observer;
using UnityEngine;

public class UIGameMobi : MonoBehaviour
{
    [SerializeField] private ControllerType[] _showControllerTypes;
    [Header("Group - Button")]
    [SerializeField] private CustomButton BtnJump;
    [SerializeField] private CustomButton BtnMechanics;
    [SerializeField] private CustomButton BtnInteract;

    private void Awake()
    {
        this.SetActive(_showControllerTypes.Any(i => i == SetupManager.ControllerType));
        BtnJump.onClick.AddListener(OnClickJump);
        BtnMechanics.onClick.AddListener(OnClickMechanics);
        BtnInteract.onClick.AddListener(OnClickInteract);
    }
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

        BtnInteract.gameObject.SetActive(param != null);

    }
    private void OnDestroy()
    {

    }

    private void OnClickJump()
    {
        this.PostEvent(EventID.PlayerEvent.PlayerJump);
    }
    private void OnClickMechanics()
    {
        this.PostEvent(EventID.PlayerEvent.PlayerMechanics);
    }
    private void OnClickInteract()
    {
        PlayerInteractor.Instance.OnInteract();
    }
}
