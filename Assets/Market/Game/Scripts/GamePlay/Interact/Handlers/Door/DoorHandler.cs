using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactable))]
public class DoorHandler : MonoBehaviour, IInteractHandler
{
    [SerializeField] private bool isLocked = false;
    [SerializeField] private float CooldownTime = 1f;
    [SerializeField] private Collider collider3D;
    [SerializeField] private Animation anim;

    private float LastTimeTry = 0;
    public void OnInteract(PlayerContext context)
    {
        if (isLocked)
            Debug.Log("🚪 Cửa bị khóa rồi!");
        else
            Debug.Log("🚪 Mở cửa!");

        TryToOpen();
    }
    private void TryToOpen()
    {
        if (Time.time < LastTimeTry + CooldownTime) return;

        LastTimeTry = Time.time;
        isLocked = !isLocked;

        collider3D.isTrigger = isLocked;
        Debug.Log(isLocked ? "🚪 Đóng cửa." : "🚪 Mở cửa.");

        anim["DoorOpen"].time = isLocked ? 0 : anim["DoorOpen"].length;
        anim["DoorOpen"].speed = isLocked ? 1 : -1;
        anim.Play("DoorOpen");
    }
}
