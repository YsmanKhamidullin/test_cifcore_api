using System;
using Game.Core.Gameplay.Windows.Base;
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

        public virtual T Instantiate<T>(T prefab, Transform parent) where T : BaseWindow
        {
            var instance = _container.InstantiatePrefabForComponent<T>(prefab, parent);
            return instance;
        }
    }
}