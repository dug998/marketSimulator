using System.Collections;
using System.Collections.Generic;
using com.homemade.pattern.observer;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoSingleton<PlayerInteractor>
{
    [SerializeField] private float distance = 2f;
    [SerializeField] private LayerMask mask;
    private PlayerContext context;

    private Interactable currentInteractable;
    private void Awake()
    {
        context = GetComponent<PlayerContext>();
    }
    private void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        Ray ray = PlayerCameraController.Instance.camera.ScreenPointToRay(UIPopupGame.Instance.ObjCrosshair.transform.position);

        if (!Physics.Raycast(ray, out var hit, distance, mask))
        {
            if (currentInteractable != null)
            {
                currentInteractable = null;
                this.PostEvent(EventID.PlayerEvent.PlayerInteractor, null);
            }
            return;
        }

        hit.collider.TryGetComponent(out Interactable interactable);
        if (currentInteractable == interactable) return;

        currentInteractable = interactable;
        this.PostEvent(EventID.PlayerEvent.PlayerInteractor, interactable);
    }
    public void OnInteract()
    {
        if (currentInteractable == null) return;
        currentInteractable.Interact(context);
    }
}
