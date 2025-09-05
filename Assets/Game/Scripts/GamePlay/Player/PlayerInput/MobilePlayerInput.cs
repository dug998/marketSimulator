using System.Collections;
using System.Collections.Generic;
using com.homemade.pattern.observer;
using UnityEngine;

public class MobilePlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private Vector2 _direction;
    [SerializeField] private Vector2 _directionLookDelta;
    [SerializeField] private bool _isJump;
    private void Awake()
    {
        this.RegisterListener(EventID.PlayerEvent.PlayMove, OnMove);
        this.RegisterListener(EventID.PlayerEvent.PlayerJump, OnJump);
        this.RegisterListener(EventID.PlayerEvent.PlayerLookDelta, OnLookDelta);
    }
    private void OnDestroy()
    {
        this.RemoveListener(EventID.PlayerEvent.PlayMove, OnMove);
        this.RemoveListener(EventID.PlayerEvent.PlayerJump, OnJump);
        this.RemoveListener(EventID.PlayerEvent.PlayerLookDelta, OnLookDelta);
    }
    private void OnMove(object obj = null)
    {
        _direction = (Vector2)obj;
    }
    private void OnLookDelta(object obj = null)
    {
        _directionLookDelta = (Vector2)obj;
    }
    private void OnJump(object obj = null)
    {
        _isJump = (bool)obj;
    }
    public Vector2 GetMoveInput()
    {
        return _direction;
    }

    public bool IsRunning()
    {
        return false;
    }
    public Vector2 GetLookDelta()
    {
        return _directionLookDelta * Time.deltaTime;
    }
}
