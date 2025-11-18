using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerPersonController : MonoSingleton<PlayerPersonController>
{

    public float MoveSpeed = 4.0f;
    public GameObject Camera;
    public IPlayerInput InputSystem => _inputSystem;

    private CharacterController _controller;
    private Vector2 _input;
    private float _speed;
    private float _verticalVelocity;
    private IPlayerInput _inputSystem;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        InitController();
    }
    private void InitController()
    {
        switch (SetupManager.ControllerType)
        {
            case ControllerType.PC:
                _inputSystem = gameObject.GetComponent<PcPlayerInput>();
                break;

            case ControllerType.Mobile:
                _inputSystem = gameObject.GetComponent<MobilePlayerInput>();
                break;
        }
    }
    private void Update()
    {
        Move();
    }
    private void LateUpdate()
    {
        RotationUpdate();
    }

    private void RotationUpdate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.transform.eulerAngles.y, transform.eulerAngles.z);
    }
    private void Move()
    {
        float targetSpeed = 5f;
        _input = _inputSystem.GetMoveInput();
        if (_input == Vector2.zero) targetSpeed = 0.0f;

        _speed = Mathf.Lerp(_speed, targetSpeed, Time.deltaTime * 10);
        _speed = Mathf.Round(_speed * 1000) / 1000;
        Vector3 inputDirection = new Vector3(_input.x, 0.0f, _input.y).normalized;
        if (_input != Vector2.zero)
        {
            inputDirection = transform.right * _input.x + transform.forward * _input.y;

        }
        if (_controller.enabled)
        {
            _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }
    }

}
