using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoSingleton<SetupManager>
{
    [SerializeField] private ControllerType _controllerType;

    public static ControllerType ControllerType => Instance._controllerType;

}
public enum ControllerType
{
    PC,
    Mobile
}
