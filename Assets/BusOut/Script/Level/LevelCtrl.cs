using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCtrl : MonoSingleton<LevelCtrl>
{
    public SlotCtrl slotCtrl;
    public QueuePassengerCtrl queuePassengerCtrl;
    public ParkingPlotCtrl parkingPlotCtrl;
    [ContextMenu("Init Level")]
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        int level = 0;
        var levelData = LevelLoader.Instance.LoadLevel(level);
        slotCtrl.Init();
        queuePassengerCtrl.Init(levelData.QueuePassengerData);
        parkingPlotCtrl.Init(levelData.ParkingPlotData);
    }

    private void OnEnable()
    {
        EventGame.Car.OnSelectCar += OnSelectCar;
        EventGame.Game.OnSlotUpdate += OnSlotUpdate;
        EventGame.Game.OnQueuePassenger += OnQueuePassenger;
    }
    private void OnDisable()
    {


    }
    public void OnSelectCar(GameObject SelectedCar)
    {
        if (SelectedCar == null) return;
        if (slotCtrl.IsFullReady())
        {
            Debug.Log("Lose Game");
            return;
        }
        var car = SelectedCar.GetComponent<CarController>();

        if (car.State == CarState.PARKING && SlotCtrl.Instance.CheckEmptySlot())
        {
            var target = SlotCtrl.Instance.GetFirstEmptySlot();
            target.SetCar(car);
            target.SetState(SlotState.WaitCar);
            car.SetMove(target);
        }
    }
    public void OnSlotUpdate()
    {
        TryPassengerGo();
    }
    public void OnQueuePassenger()
    {
        TryPassengerGo();
    }
    public void TryPassengerGo()
    {
        if (!QueuePassengerCtrl.Instance.IsReady()) return;

        var slots = SlotCtrl.Instance.slots;
        foreach (var slot in slots)
        {
            if (slot.CheckEmpty())
            {
                continue;
            }
            if (slot.currentCarController.IsFull())
            {
                slot.currentCarController.Leave();
                slot.SetCar(null);
                continue;
            }
            var passenger = QueuePassengerCtrl.Instance.GetFrontPassenger();
            if (passenger != null && slot.CanPassengerGo(passenger))
            {
                slot.currentCarController.AddPassenger(passenger);
                QueuePassengerCtrl.Instance.MoveToCar(passenger, slot.currentCarController);
                if (slot.currentCarController.IsFull())
                {
                    slot.currentCarController.Leave();
                    slot.SetCar(null);
                    slot.SetStateDefault();
                }
                return;
            }
        }
        if (slotCtrl.IsFullReady())
        {
            Debug.Log("Lose");
        }
    }
}
