using System;
using UnityEngine;
using Zenject;

namespace Game.Core.Factories
{
    public class PrefabFactory<TPrefabType> where TPrefabType : MonoBehaviour
    {
        private DiContainer _container;

        public PrefabFactory(DiContainer container)
        {
            _container = container;
        }

        public virtual TPrefabType Instantiate(TPrefabType prefab, Transform parent)
        {
            var instance = _container.InstantiatePrefabForComponent<TPrefabType>(prefab, parent);
            return instance;
        }
    }
}