using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Bus
{
    public static class DirectionHelper
    {
        public static float GetDirectionAngle(CarDirection carDirection)
        {
            float Angle = 0;
            switch (carDirection)
            {
                case CarDirection.LB:
                    {
                        Angle = -135;
                        break;
                    }

                case CarDirection.L:
                    {
                        Angle = -90;
                        break;
                    }

                case CarDirection.LT:
                    {
                        Angle = -45;
                        break;
                    }

                case CarDirection.T:
                    {
                        Angle = 0;
                        break;
                    }

                case CarDirection.RT:
                    {
                        Angle = 45;
                        break;
                    }

                case CarDirection.R:
                    {
                        Angle = 90;
                        break;
                    }

                case CarDirection.RB:
                    {
                        Angle = 135;
                        break;
                    }

                case CarDirection.B:
                    {
                        Angle = 180;
                        break;
                    }
                case CarDirection.parking:
                    {
                        Angle = -22;
                        break;
                    }
            }
            return Angle;
        }
        public static Vector3 GetDirectionVector(CarDirection carDirection)
        {
            Vector3 direction = Vector3.zero;
            switch (carDirection)
            {
                case CarDirection.LB:
                    {
                        direction = Vector2.left + Vector2.down;
                        break;
                    }

                case CarDirection.L:
                    {
                        direction = Vector2.left;
                        break;
                    }

                case CarDirection.LT:
                    {
                        direction = Vector2.left + Vector2.up;
                        break;
                    }

                case CarDirection.T:
                    {
                        direction = Vector2.up;
                        break;
                    }

                case CarDirection.RT:
                    {
                        direction = Vector2.right + Vector2.up;
                        break;
                    }

                case CarDirection.R:
                    {
                        direction = Vector2.right;
                        break;
                    }

                case CarDirection.RB:
                    {
                        direction = Vector2.right + Vector2.down;
                        break;
                    }

                case CarDirection.B:
                    {
                        direction = Vector2.down;
                        break;
                    }
            }
            return new Vector3(direction.normalized.x, 0, direction.normalized.y);
        }

    }
    public enum CarDirection
    {
        LB = 1,
        L = 2,
        LT = 3,
        T = 5,
        RT = -3,
        R = -2,
        RB = -1,
        B = 4,
        parking = 6
    }
}