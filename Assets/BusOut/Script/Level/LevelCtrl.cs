using System.Collections;
using System.Collections.Generic;
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
    }
    private void OnDisable()
    {


    }
    public void OnSelectCar(GameObject SelectedCar)
    {
        if (SelectedCar == null) return;
        var car = SelectedCar.GetComponent<CarController>();

        if (car.State == CarState.PARKING && SlotCtrl.Instance.CheckEmptySlot())
        {
            var target = SlotCtrl.Instance.GetFirstEmptySlot();
            car.SetMove(target);
            target.SetCar(car);
        }
    }
}
