using Game.Core.Gameplay.Windows;
using Game.Core.Gameplay.Windows.Base;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public class BootStrap : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private NavigationMenu _navigationMenu;

        private WindowsDatabaseConfig _windowsDatabaseConfig;

        [Inject]
        public void Construct(WindowsDatabaseConfig windowsDatabaseConfig)
        {
            _windowsDatabaseConfig = windowsDatabaseConfig;
        }

        public void Initialize()
        {
            _navigationMenu.Initialize(_windowsDatabaseConfig.WindowTabPrefabs);
        }
    }
}