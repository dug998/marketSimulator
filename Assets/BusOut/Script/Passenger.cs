using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace Bus
{
    public class Passenger : MonoBehaviour
    {
        public int idColor;
        PassengerState _state;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private Animation ani;

        public void Init(int numberColor)
        {
            idColor = numberColor;
            skinnedMeshRenderer.material = LevelSetup.Instance.GetMaterialByIdColor(idColor);
            Idle();
        }
        public void MoveToCar(CarController car)
        {

            var seat = car.GetCarSeat();
            transform.SetParent(seat);
            transform.DOScale(Vector3.one * 0.55f, 0.25f);
            Run();
            transform.DOLocalMove(Vector3.zero, Config.TIME_PASSENGER_TO_CAR).OnComplete(() =>
            {
                Idle();
                _state = PassengerState.FINISH;
            });
        }
        public void Run()
        {
            ani.Play("Walk");
        }
        public void Idle()
        {
            ani.Play("Wait1");
        }
    }
    public enum PassengerState
    {
        PREPARE,
        RUN,
        READY, // sẵn sàng lên xe
        MOVING, // Đang lên xe
        FINISH // đã lên xe
    }
}
