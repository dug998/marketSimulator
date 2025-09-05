using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (!_Instance)
            {
                Debug.Log($"Dont has InstanceOf {typeof(T).Name} class");
                _Instance = FindObjectOfType<T>(true);
                if (!_Instance)
                {
                    _Instance = new GameObject(typeof(T).Name + " Singleton").AddComponent<T>();
                }
            }
            return _Instance;
        }
    }
}