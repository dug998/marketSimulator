using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
namespace Bus
{
    public class QueuePassengerCtrl : MonoSingleton<QueuePassengerCtrl>
    {
        public GameObject PassengerPrefab;
        public bool IsUpdatePassenger;

        private Queue<Passenger> currentQueuePassenger = new Queue<Passenger>();
        public void Init(QueuePassengerData queuePassengerData)
        {
            int index = 0;
            queuePassengerData.QueuePassenger.List.ForEach(data =>
            {
                for (int i = 0; i < data.num; i++)
                {
                    var obj = Instantiate(PassengerPrefab, transform);

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
            passenger.MoveToCar(car);
            currentQueuePassenger.Dequeue();
            if (currentQueuePassenger.Count == 0)
            {
                Debug.Log("win game");

            }

            UpdatePassenger();
        }
        private void UpdatePassenger()
        {
            IsUpdatePassenger = true;
            EventGame.Game.OnUpdateNumberPassenger.Invoke(currentQueuePassenger.Count);
            // currentQueuePassenger.Dequeue();
            int index = 0;
            Sequence sequence = DOTween.Sequence();
            foreach (var controller in currentQueuePassenger)
            {
                controller.Run();
                sequence.Join(controller.transform.DOLocalMove(Util.GetPassengerPosition(index), 0.25f));
                index++;
            }
            sequence.OnComplete(() =>
            {
                currentQueuePassenger.ForEach(x => x.Idle());
                IsUpdatePassenger = false;
                EventGame.Game.OnQueuePassenger.Invoke();

            });
        }
        public bool IsReady()
        {
            return !IsUpdatePassenger;
        }
    }
}