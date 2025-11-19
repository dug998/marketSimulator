
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class CarController : MonoBehaviour
{
    public CarState State => _state;
    public CarDirection _carDirection;
    public int _idColor;

    public CarSeat _seat;
    private CarState _state = CarState.PARKING;
    private Vector2 oldPosition;
    private Slot _targetSlot;
    int currentPassengerNum = 0;
    CarData _data;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshRenderer _meshRendererSlot;

    private List<Passenger> passengerToCards = new List<Passenger>();
    public void Init(CarData data)
    {
        _data = data;
        _idColor = data.Color;
        _carDirection = (CarDirection)data.Direction;
        transform.localPosition = new Vector3(data.Position.x, 0, data.Position.y);
        transform.localRotation = Quaternion.Euler(0, DirectionHelper.GetDirectionAngle(_carDirection), 0);
        _meshRenderer.material = LevelSetup.Instance.GetMaterialByIdColor(data.Color);
        _meshRendererSlot.material = LevelSetup.Instance.GetMaterialByIdColor(data.Color);

        _meshRendererSlot.gameObject.SetActive(false);
        _meshRenderer.gameObject.SetActive(true);
        passengerToCards.Clear();

    }
    private void OnMouseDown()
    {
        EventGame.Car.OnSelectCar.Invoke(gameObject);
    }



    public void SetMove(Slot slot)
    {
        //   var cData = Data as CarData;
        oldPosition = transform.localPosition;
        _targetSlot = slot;
        //  target = slot.transform.position;
        _state = CarState.START_MOVE;

    }
    void Update()
    {
        switch (_state)
        {
            case CarState.START_MOVE:
                {
                    var v = (Vector3)DirectionHelper.GetDirectionVector(_carDirection) * Config.VEC_CAR_MOVE;
                    transform.position += v * Time.deltaTime;
                    break;
                }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_state != CarState.START_MOVE) return;

        if (other.CompareTag(Const.Wall))
        {
            _state = CarState.MOVE_TO_SLOT;
            var collisionPoint = transform.position + DirectionHelper.GetDirectionVector(_carDirection);
            Vector3[] pathPositions = ParkingPlotCtrl.Instance.GenerateParkingExitPath(DirectionHelper.GetDirectionVector(_carDirection), other.gameObject, collisionPoint, _targetSlot.transform.position).ToArray();


            transform.DOPath(pathPositions, Config.VEC_CAR_MOVE, PathType.CatmullRom).SetSpeedBased().OnComplete(() =>
            {
                _carDirection = CarDirection.parking;
                transform.localRotation = Quaternion.Euler(0, DirectionHelper.GetDirectionAngle(_carDirection), 0);
                _targetSlot.READY();

                _meshRendererSlot.gameObject.SetActive(true);
                _meshRenderer.gameObject.SetActive(false);
            }).SetLookAt(0.01f);

        }
        if (other.CompareTag(Const.Car))
        {
            var otherCar = other.gameObject;
            var otherController = otherCar.GetComponent<CarController>();

            if (otherController.State != CarState.PARKING
                  && otherController.State != CarState.CRASHING)
            {
                return;
            }
            if (otherController.State == CarState.PARKING)
            {
                otherController.Crash();
            }
            _state = CarState.MOVE_BACK;

            _targetSlot.SetStateDefault();
            _targetSlot.SetCar(null);
            
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMove(oldPosition, Config.TIME_CAR_MOVE_BACK)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _state = CarState.PARKING;
                }));
        }
    }
    public void Crash()
    {
    }
    public void Leave()
    {
        if (_state == CarState.LEAVE) return;
        // Xe di chuyen rời đi -------------
        _state = CarState.LEAVE;


        StartCoroutine(ILeave());

    }
    private IEnumerator ILeave()
    {
        yield return new WaitForSeconds(Config.TIME_PASSENGER_TO_CAR);
        Sequence sequence = DOTween.Sequence();

        Vector3[] pathPositions = new Vector3[2];

        var point1 = new Vector3(transform.position.x, 0, ParkingPlotCtrl.Instance.GetZTop());
        var point2 = point1 + Vector3.right * 30;
        pathPositions[0] = point1;
        pathPositions[1] = point2;

        transform.DOPath(pathPositions, Config.VEC_CAR_MOVE, PathType.CatmullRom).SetSpeedBased().OnComplete(() =>
        {
            Destroy(gameObject);
        }).SetLookAt(0.01f);
    }

    public void AddPassenger(Passenger passenger)
    {
        passengerToCards.Add(passenger);
        currentPassengerNum += 1;
    }
    public bool IsFull()
    {
        return currentPassengerNum == (int)_data.Size;
    }
    public Transform GetCarSeat()
    {
        return _seat.GetSeat(currentPassengerNum - 1);
    }
}
public enum CarState
{
    PARKING,
    CHECK_START_MOVE,
    START_MOVE,
    MOVE_TO_SLOT,
    MOVE_BACK,
    CRASHING,
    READY,
    LEAVE
}