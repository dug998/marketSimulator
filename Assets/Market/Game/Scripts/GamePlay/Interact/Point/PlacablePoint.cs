using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacablePoint : MonoBehaviour
{
    public bool IsAvailable = true;
    public GameObject ObjectToGrab
    {
        get => objectToGrab;
        set
        {
            objectToGrab = value;
        }
    }


    [SerializeField] private bool isAvailable = true;
    [SerializeField] private GameObject objectToGrab;
}
