using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    Vector2 GetMoveInput();
    bool IsRunning();
    Vector2 GetLookDelta();
}
