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
}
