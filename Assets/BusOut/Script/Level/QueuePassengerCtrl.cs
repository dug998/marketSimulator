using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class QueuePassengerCtrl : MonoSingleton<QueuePassengerCtrl>
{
    public GameObject PassengerPrefab;
    private Queue<Passenger> currentQueuePassenger = new Queue<Passenger>();
    public void Init(QueuePassengerData queuePassengerData)
    {
        int index = 0;
        queuePassengerData.QueuePassenger.List.ForEach(data =>
        {
            for (int i = 0; i < data.num; i++)
            {
                var obj = Instantiate(PassengerPrefab,transform);

                obj.transform.localPosition = Util.GetPassengerPosition(index);
                obj.GetComponent<Passenger>().Init(data.color);
                index++;
                currentQueuePassenger.Enqueue(obj.GetComponent<Passenger>());
            }
        });
    }
    public Passenger GetFrontPassenger()
    {
        if (currentQueuePassenger.Count == 0)
        {
            return null;
        }
        return currentQueuePassenger.Peek();
    }
    public void MoveToCar(Passenger passenger, CarController car)
    {
        currentQueuePassenger.Dequeue();
        passenger.MoveToCar(car);
        Invoke("AfterMoveToCar", Config.TIME_PASSENGER_TO_CAR);
    }
    private void AfterMoveToCar()
    {
       // currentQueuePassenger.Dequeue();
        foreach (var controller in currentQueuePassenger)
        {
            controller.Run();
        }
        return;
    }
}
