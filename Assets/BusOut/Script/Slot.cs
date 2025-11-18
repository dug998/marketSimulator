using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    CarController currentCarController;

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
        bool add = true;
        while (add)
        {
            var passenger = QueuePassengerCtrl.Instance.GetFrontPassenger();
            if (passenger != null && CanPassengerGo(passenger))
            {
                add = true;
                currentCarController.AddPassenger();
                QueuePassengerCtrl.Instance.MoveToCar(passenger, currentCarController);
                if (currentCarController.IsFull())
                {
                    currentCarController.Leave();
                    currentCarController = null;
                }
            }
            else
            {
                add = false;
            }
        }

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
