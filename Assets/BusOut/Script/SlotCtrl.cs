using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCtrl : MonoSingleton<SlotCtrl>
{
    public List<Slot> slots;

    public void Init()
    {
        slots.ForEach(x => x.Init());
    }
    public bool CheckEmptySlot()
    {

        return true;
    }
    public Slot GetFirstEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.CheckEmpty() == true && slot.IsReady())
            {
                return slot;
            }
        }
        Debug.LogWarning("SlotManager: don't have EmptySlot");
        return null;
    }
}
