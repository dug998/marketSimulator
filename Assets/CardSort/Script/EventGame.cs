using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventGame 
{
    public static class Game
    {
        public static Action<Slot> OnTapSlot;
        public static Action OnTapDealButton;

        public static Action OnUpdateScoreLevel;

        public static Action OnStartLevel;

        public static Action OnWinLevel;
        public static Action OnLoseLevel;

    }
    public static class UI
    {
      
        public static Action OnNextLevel;
        public static Action OnReplayLevel;
    }

}
