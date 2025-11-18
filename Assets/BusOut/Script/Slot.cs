using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
   public CarController currentCarController;

    public void Init()
    {

    }
    public void SetCar(CarController car)
    {
        currentCarController = car;

    }
    public bool CheckEmpty()
    {
        return currentCarController == null;
    }
    public bool IsReady()
    {
        return true;
        // return _state == SlotState.READY;
    }
    public void READY()
    {
        EventGame.Game.OnSlotUpdate.Invoke();
    }
    public bool CanPassengerGo(Passenger passenger)
    {
        return currentCarController._idColor == passenger.idColor;
    }
}
public enum SlotState
{
    READY,
    LOCK,
    VIP
}
