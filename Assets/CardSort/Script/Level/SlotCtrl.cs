using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCtrl : MonoBehaviour
{
    public List<Slot> slots;
    public void Init()
    {
        slots.ForEach(x => x.Init());

    }
    public List<Slot> SlotActions()
    {
        return slots;
    }
}
