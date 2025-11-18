using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Reflection.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CarController : MonoBehaviour
{
    public CarState State => _state;
    public CarDirection _carDirection;
    public int _idColor;



    private CarState _state = CarState.PARKING;
    private Vector2 oldPosition;
    private Slot _targetSlot;
    int currentPassengerNum = 0;
    CarData _data;
    [SerializeField] private MeshRenderer _meshRenderer;
    public void Init(CarData data)
    {
        _data = data;
        _idColor = data.Color;
        _carDirection = (CarDirection)data.Direction;
        transform.localPosition = new Vector3(data.Position.x, 0, data.Position.y);
        transform.localRotation = Quaternion.Euler(0, DirectionHelper.GetDirectionAngle(_carDirection), 0);
        _meshRenderer.material = LevelSetup.Instance.GetMaterialByIdColor(data.Color);

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

            Vector3[] pathPositions = GetArcPath(transform.position, _targetSlot.transform.position);
            //pathPositions[0] = transform.position;
            //pathPositions[1] = other.ClosestPoint(transform.position);
            //pathPositions[2] = _targetSlot.transform.position;



            transform.DOPath(pathPositions, Config.VEC_CAR_MOVE, PathType.CatmullRom).SetSpeedBased().OnComplete(() =>
            {
                _carDirection = CarDirection.parking;
                transform.localRotation = Quaternion.Euler(0, DirectionHelper.GetDirectionAngle(_carDirection), 0);
                _targetSlot.READY();
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

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMove(oldPosition, Config.TIME_CAR_MOVE_BACK)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _state = CarState.PARKING;
                }));
        }


    }

    private Vector3[] GetArcPath(Vector3 start, Vector3 end)
    {
        float minX = -4f;  // vùng cấm bên trái
        float maxX = 4f;
        Vector3 midPoint = (start + end) / 2f;

        if (midPoint.x > minX && midPoint.x < maxX)
        {
            // chọn đẩy ra bên trái hoặc bên phải
            if (midPoint.x - minX < maxX - midPoint.x)
                midPoint.x = minX - 0.5f; // lùi ra ngoài trái
            else
                midPoint.x = maxX + 0.5f; // ra ngoài phải
        }
        return new Vector3[] { start, midPoint, end };
    }
    public void Crash()
    {
    }
    public void Leave()
    {
        // Xe di chuyen rời đi -------------
        _state = CarState.LEAVE;
        Destroy(gameObject);

    }
    public void AddPassenger()
    {
        currentPassengerNum += 1;
    }
    public bool IsFull()
    {
        return currentPassengerNum == (int)_data.Size;
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