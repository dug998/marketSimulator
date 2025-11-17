using System.Collections;
using System.Collections.Generic;
using Games.Scripts.Utils.ObjectPools;
using Sirenix.OdinInspector;
using UnityEngine;
[RequireComponent(typeof(Interactable))]
public class OrderBoxHandler : MonoBehaviour, IInteractHandler
{
    private bool isHolding = false;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private ArrayableArea arrayableArea;

    [SerializeField] private GameObject sellableObjectPrefab;
    [Button]
    public void Init()
    {
        for (int i = 0; i < arrayableArea.PlacablePoints.Length; i++)
        {
            GameObject item = SDPool.Spawn(sellableObjectPrefab, arrayableArea.PlacablePoints[i].transform.position, Quaternion.identity, transform);
            item.transform.localEulerAngles = Vector3.zero;
            arrayableArea.PlacablePoints[i].ObjectToGrab = item;
            arrayableArea.PlacablePoints[i].IsAvailable = false;
        }
    }
    public void OnInteract(PlayerContext context)
    {
        if (context == null) return;
        if (!isHolding)
        {
            isHolding = true;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            transform.parent = PlayerCameraController.Instance.transform;
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            transform.localPosition = new Vector3(0, -0.35f, 0.4f);
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            transform.GetComponent<BoxCollider>().enabled = false;
        }

    }


}
