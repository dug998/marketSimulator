using System;
using Games.Scripts.Utils.DataHelper;
using UnityEngine;

namespace Games.Scripts.Model
{
    [Serializable]
    public abstract class BaseDataSave<T> where T : BaseDataSave<T>, new()
    {
        protected virtual string Key => typeof(T).Name;
        protected abstract void InitDefault();
        protected abstract void InitHasKey();

        public static T Create()
        {
            var instance = new T();
            var key = instance.Key;

            if (LocalStorageUtils.HasKey(key))
            {
                var data = JsonUtility.FromJson<T>(LocalStorageUtils.GetString(key));
                data.InitHasKey();
                data.Save();
                return data;
            }
            else
            {
                instance.InitDefault();
                instance.Save();
                return instance;
            }
        }

        public static T CreateWithInit(Action<T> initCallback)
        {
            var instance = new T();
            var key = instance.Key;

            if (LocalStorageUtils.HasKey(key))
            {
                var data = JsonUtility.FromJson<T>(LocalStorageUtils.GetString(key));
                data.InitHasKey();
                data.Save();
                return data;
            }
            else
            {
                initCallback?.Invoke(instance);
                instance.Save();
                return instance;
            }
        }

        protected void Save()
        {
            LocalStorageUtils.SetString(Key, JsonUtility.ToJson(this));
        }
    }
}