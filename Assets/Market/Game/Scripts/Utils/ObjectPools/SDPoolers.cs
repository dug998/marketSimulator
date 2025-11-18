using System.Collections.Generic;
using UnityEngine;
namespace Games.Scripts.Utils.ObjectPools
{
    public class SDPoolers : MonoSingleton<SDPoolers>
    {
        private const string SUFFIX = "_Pool";
        private readonly Dictionary<GameObject, List<GameObject>> _gameObjectPools = new();
        private readonly Dictionary<GameObject, Transform> _parentPools = new();

        private Transform _cacheTrs;

        private void Awake()
        {
            _cacheTrs = transform;
        }

        private void CleanNullEntries(GameObject currentKey)
        {
            if (!currentKey) return;
            if (!_gameObjectPools.TryGetValue(currentKey, out var list)) return;
            list.RemoveAll(item => !item);
            if (list.Count == 0) _gameObjectPools.Remove(currentKey);
        }

        public T Spawn<T>(T go, Transform parent = null, bool worldPositionStays = false) where T : Component
        {
            CleanNullEntries(go.gameObject);
            if (_gameObjectPools.ContainsKey(go.gameObject))
            {
                for (var index = 0; index < _gameObjectPools[go.gameObject].Count; index++)
                {
                    var o = _gameObjectPools[go.gameObject][index];
                    if (o.activeSelf) continue;
                    o.transform.SetParent(parent);
                    o.Show();
                    if (!parent) return o.GetComponent<T>();
                    o.transform.position = Vector3.zero;
                    o.transform.rotation = Quaternion.identity;
                    return o.GetComponent<T>();
                }

                var item = Instantiate(go, parent ? parent : _parentPools[go.gameObject], worldPositionStays);
                _gameObjectPools[go.gameObject].Add(item.gameObject);
                return item;
            }
            else
            {
                var holder = new GameObject($"{go.name}{SUFFIX}").transform;
                holder.SetParent(_cacheTrs);
                _parentPools.Add(go.gameObject, holder);
                var item = Instantiate(go, parent ? parent : _parentPools[go.gameObject], worldPositionStays);
                _gameObjectPools.Add(go.gameObject, new List<GameObject> { item.gameObject });
                return item;
            }
        }

        public T Spawn<T>(T go, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : Component
        {
            CleanNullEntries(go.gameObject);
            if (_gameObjectPools.ContainsKey(go.gameObject))
            {
                for (var index = 0; index < _gameObjectPools[go.gameObject].Count; index++)
                {
                    var o = _gameObjectPools[go.gameObject][index];
                    if (o.activeSelf) continue;
                    o.transform.SetParent(parent ? parent : _parentPools[o]);
                    o.transform.position = position;
                    o.transform.rotation = rotation;
                    o.Show();
                    return o.GetComponent<T>();
                }

                var item = Instantiate(go, position, rotation, parent ? parent : _parentPools[go.gameObject]);
                _gameObjectPools[go.gameObject].Add(item.gameObject);
                return item;
            }
            else
            {
                var holder = new GameObject($"{go.name}{SUFFIX}").transform;
                holder.SetParent(_cacheTrs);
                _parentPools.Add(go.gameObject, holder);

                var item = Instantiate(go, position, rotation, parent ? parent : _parentPools[go.gameObject]);
                _gameObjectPools.Add(go.gameObject, new List<GameObject> { item.gameObject });
                return item;
            }
        }

        public GameObject Spawn(GameObject go, Transform parent = null, bool worldPositionStays = false)
        {
            CleanNullEntries(go.gameObject);
            if (_gameObjectPools.ContainsKey(go))
            {
                for (var index = 0; index < _gameObjectPools[go].Count; index++)
                {
                    var o = _gameObjectPools[go][index];
                    if (o.activeSelf) continue;
                    o.transform.SetParent(parent ? parent : _parentPools[go.gameObject]);
                    if (parent) o.transform.localPosition = Vector3.zero;

                    o.Show();
                    return o;
                }

                var item = Instantiate(go, parent ? parent : _parentPools[go.gameObject], worldPositionStays);
                if (parent) item.transform.localPosition = Vector3.zero;

                _gameObjectPools[go].Add(item);

                return item;
            }
            else
            {
                var holder = new GameObject($"{go.name}{SUFFIX}").transform;
                holder.SetParent(_cacheTrs);
                _parentPools.Add(go.gameObject, holder);

                var item = Instantiate(go, parent ? parent : _parentPools[go.gameObject], worldPositionStays);
                _gameObjectPools.Add(go, new List<GameObject> { item });
                if (parent) item.transform.localPosition = Vector3.zero;

                return item;
            }
        }

        public GameObject Spawn(GameObject go, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            CleanNullEntries(go.gameObject);
            if (_gameObjectPools.ContainsKey(go))
            {
                for (var index = 0; index < _gameObjectPools[go].Count; index++)
                {
                    var o = _gameObjectPools[go][index];
                    if (o.activeSelf) continue;
                    o.transform.SetParent(parent ? parent : _parentPools[go]);
                    o.transform.position = position;
                    o.transform.rotation = rotation;
                    o.Show();
                    return o;
                }

                var item = Instantiate(go, position, rotation, parent ? parent : _parentPools[go]);
                _gameObjectPools[go].Add(item);
                return item;
            }
            else
            {
                var holder = new GameObject($"{go.name}{SUFFIX}").transform;
                holder.SetParent(_cacheTrs);
                _parentPools.Add(go.gameObject, holder);
                var item = Instantiate(go, position, rotation, parent ? parent : _parentPools[go.gameObject]);
                _gameObjectPools.Add(go, new List<GameObject> { item });
                return item;
            }
        }

        public void DeSpawn<T>(T go) where T : Component
        {
            var key = GetKeyFromClone(go.gameObject);
            if (key)
            {
                go.Hide();
                go.transform.SetParent(_parentPools[key]);
            }
            else
            {
                go.Hide();
            }
        }

        public void DeSpawn(GameObject go)
        {
            var key = GetKeyFromClone(go);
            if (key)
            {
                go.transform.SetParent(_parentPools[key]);
                go.Hide();
            }
            else
            {
                go.Hide();
            }
        }

        public void DeSpawn<T>(T go, float timeDelay) where T : Component
        {
            this.TimeOut(() => { DeSpawn(go); }, timeDelay);
        }

        public void DeSpawn(GameObject go, float timeDelay)
        {
            if (timeDelay > 0)
                this.TimeOut(() => { DeSpawn(go); }, timeDelay);
            else
                DeSpawn(go);
        }

        public void DeSpawnAll<T>(T go) where T : Component
        {
            if (!_gameObjectPools.TryGetValue(go.gameObject, out var pool)) return;
            foreach (var component in pool)
            {
                component.Hide();
                component.transform.SetParent(_cacheTrs);
            }
        }

        public void DeSpawnAll(GameObject go)
        {
            if (!_gameObjectPools.TryGetValue(go, out var pool)) return;
            for (var index = 0; index < pool.Count; index++)
            {
                var item = pool[index];
                item.Hide();
                item.transform.SetParent(_parentPools[go]);
            }
        }

        public void DestroyObject(GameObject go)
        {
            var key = GetKeyFromClone(go);
            if (!key) return;
            _gameObjectPools[key].Remove(go);
            Destroy(go);
        }

        public void DestroyObject<T>(T go) where T : Component
        {
            var key = GetKeyFromClone(go.gameObject);
            if (!key) return;
            _gameObjectPools[key].Remove(go.gameObject);
            Destroy(go);
        }

        private GameObject GetKeyFromClone(GameObject clone)
        {
            foreach (var pool in _gameObjectPools)
                if (pool.Value.Contains(clone.gameObject))
                    return pool.Key;

            return null;
        }
    }
}