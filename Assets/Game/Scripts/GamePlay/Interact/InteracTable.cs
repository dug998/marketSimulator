using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> handlers = new(); // chứa các IInteractHandler

    public void Interact(PlayerContext context)
    {
        foreach (var handler in handlers)
        {
            if (handler is IInteractHandler h)
                h.OnInteract(context);
        }
    }

    #region Editor
#if UNITY_EDITOR
    private void OnValidate()
    {
        handlers.Clear();
        var comps = GetComponents<MonoBehaviour>();
        foreach (var comp in comps)
        {
            if (comp is IInteractHandler && !handlers.Contains(comp))
                handlers.Add(comp);
        }
    }
#endif
    #endregion
}
