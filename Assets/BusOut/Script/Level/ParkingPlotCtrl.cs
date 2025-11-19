using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
namespace Bus
{
    public class ParkingPlotCtrl : MonoSingleton<ParkingPlotCtrl>
    {
        public CarController carPrefab_4, carPrefab_6, carPrefab_10;
        public ParkingPath _parkingPath;

        public void Init(ParkingPlotData parkingPlotData)
        {
            parkingPlotData.Cars.ForEach(car =>
            {
                var prefab = carPrefab_4;
                if (car.Size == 6)
                {
                    prefab = carPrefab_6;
                }
                else if (car.Size == 10)
                {
                    prefab = carPrefab_10;
                }
                var obj = Instantiate(prefab, transform);
                obj.Init(car);
            });
        }
        public List<Vector3> GenerateParkingExitPath(Vector3 direction, GameObject collision, Vector3 collisionPosition, Vector3 targetPosition)
        {
            return _parkingPath.GenerateParkingExitPath(direction, collision, collisionPosition, targetPosition);
        }
        public float GetZTop()
        {
            return _parkingPath.GetZTop();
        }
    }
}