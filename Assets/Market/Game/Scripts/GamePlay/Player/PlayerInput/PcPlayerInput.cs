using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcPlayerInput : MonoBehaviour, IPlayerInput
{
    public Vector2 GetMoveInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    public Vector2 GetLookDelta()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

}
