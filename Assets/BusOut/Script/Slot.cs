using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public CarController currentCarController;

    [SerializeField] private SlotState _state;
    private SlotState _stateDefault;
    public void Init()
    {
        _stateDefault = _state;
    }
    public void SetState(SlotState state)
    {
        _state = state;
    }
    public void SetStateDefault()
    {
        _state = _stateDefault;
    }
    public void SetCar(CarController car)
    {
        currentCarController = car;

    }
    public bool CheckEmpty()
    {
        return currentCarController == null;
    }
    public bool IsStateNone()
    {
        return _state == SlotState.None;
    }
    public void READY()
    {
        _state = SlotState.READY;
        EventGame.Game.OnSlotUpdate.Invoke();
    }
    public bool CanPassengerGo(Passenger passenger)
    {
        return currentCarController._idColor == passenger.idColor;
    }
}
public enum SlotState
{
    None,
    WaitCar,
    READY,
    LOCK,
    VIP
}
