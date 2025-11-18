using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Vector3 GetPassengerPosition(float index)
    {
        var pIdx = index;

        float cellX = pIdx - Config.WIDTH_QUEUE_PASSENGER > 0 ? Config.WIDTH_QUEUE_PASSENGER : pIdx;
        float cellY = pIdx - Config.WIDTH_QUEUE_PASSENGER > 0 ? pIdx - Config.WIDTH_QUEUE_PASSENGER : 0;

        float x = cellX * Config.DISTANCE_PASSENGER,
            y = cellY * Config.DISTANCE_PASSENGER;
        return new Vector3(x, 0, y);
    }
}
