using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class ParkingPlotCtrl : MonoBehaviour
{
    public CarController carPrefab;
    public void Init(ParkingPlotData parkingPlotData)
    {
        parkingPlotData.Cars.ForEach(car =>
        {
            var obj = Instantiate(carPrefab, transform);
            obj.Init(car);
        });
    }
}
