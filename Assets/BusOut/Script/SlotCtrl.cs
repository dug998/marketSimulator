using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Bus
{
    public class SlotCtrl : MonoSingleton<SlotCtrl>
    {
        public List<Slot> slots;

        public void Init()
        {
            slots.ForEach(x => x.Init());
        }
        public bool IsFullReady()
        {
            return slots.All(x => !x.CheckEmpty());
        }
        public bool CheckEmptySlot()
        {
            foreach (var slot in slots)
            {
                if (slot.CheckEmpty())
                {
                    return true;
                }
            }
            return false;
        }
        public Slot GetFirstEmptySlot()
        {
            foreach (var slot in slots)
            {
                if (slot.CheckEmpty() == true && slot.IsStateNone())
                {
                    return slot;
                }
            }
            Debug.LogWarning("SlotManager: don't have EmptySlot");
            return null;
        }


    }
}