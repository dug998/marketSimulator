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

    private void Awake()
    {

        this.SetActive(_showControllerTypes.Any(i => i == SetupManager.ControllerType));
        BtnJump.onClick.AddListener(OnClickJump);
        BtnMechanics.onClick.AddListener(OnClickMechanics);


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
}
