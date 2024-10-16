using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Spawner
{
    public class VFXPool<T>
        where T : Component
    {
        private readonly T _prefab;
        private readonly List<T> _pool = new();

        public VFXPool(T prefab)
        {
            _prefab = prefab;
        }

        public T Get()
        {
            T vfx = _pool.FirstOrDefault(v => v.gameObject.activeInHierarchy == false) ?? Create();

            vfx.gameObject.SetActive(true);

            return vfx;
        }

        private T Create()
        {
            var vfx = GameObject.Instantiate(_prefab);
            _pool.Add(vfx);
            return vfx;
        }
    }
}