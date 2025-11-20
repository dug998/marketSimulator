using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionHelper
{
    public static Vector3 GetDirectionAngle(DirectionType carDirection)
    {
        Vector3 Angle = Vector3.zero;
        switch (carDirection)
        {
            case DirectionType.LB:
                {
                    Angle = new Vector3(-180, 0, 0);
                    break;
                }
            case DirectionType.L:
                {
                    Angle = new Vector3(0, 0, 180);
                    break;
                }
            case DirectionType.LT:
                {
                    Angle = new Vector3(180, 0, 0);
                    break;
                }
            case DirectionType.T:
                {
                    Angle = new Vector3(180, 0, 0);
                    break;
                }
            case DirectionType.RT:
                {
                    Angle = new Vector3(180, 0, 0);
                    break;
                }
            case DirectionType.R:
                {
                    Angle = new Vector3(0, 0, -180);
                    break;
                }
            case DirectionType.RB:
                {
                    Angle = new Vector3(-180, 0, 0);
                    break;
                }
            case DirectionType.B:
                {
                    Angle = new Vector3(-180, 0, 0);
                    break;
                }
        }
        return Angle;
    }
    public static DirectionType GetMovementDirection(Vector3 dir)
    {
        
        var result = dir.x switch
        {
            0 when dir.z > 0 => DirectionType.T,
            0 when dir.z < 0 => DirectionType.B,
            > 0 when dir.z == 0 => DirectionType.R,
            < 0 when dir.z == 0 => DirectionType.L,
            > 0 when dir.z > 0 => DirectionType.RT,
            < 0 when dir.z > 0 => DirectionType.LT,
            > 0 when dir.z < 0 => DirectionType.RB,
            < 0 when dir.z < 0 => DirectionType.LB,
            _ => DirectionType.T
        };

        return result;
    }


    public enum DirectionType
    {
        LB,
        L,
        LT,
        T,
        RT,
        R,
        RB,
        B,
    }
}
