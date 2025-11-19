using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bus
{
    public static class EventGame
    {
        public static class Car
        {
            public static Action<GameObject> OnSelectCar;

        }
        public static class Game
        {
            public static Action OnSlotUpdate;
            public static Action OnQueuePassenger;
            public static Action<int> OnUpdateNumberPassenger;
        }
    }
}