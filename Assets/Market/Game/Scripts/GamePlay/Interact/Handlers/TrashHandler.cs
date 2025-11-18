using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactable))]
public class TrashHandler : MonoBehaviour, IInteractHandler
{
    public void OnInteract(PlayerContext context)
    {
        Debug.Log("TrashHandler OnInteract");
    }
}
