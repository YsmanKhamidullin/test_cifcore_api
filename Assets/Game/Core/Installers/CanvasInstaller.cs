using Game.Core.Factories;
using Game.Core.Gameplay.Dogs;
using Game.Core.Gameplay.Windows;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Core.Installers
{
    public class CanvasInstaller : MonoInstaller
    {
        [SerializeField]
        private NavigationMenu _navigationMenu;

        private DogsSettings _dogsSettings;
        public override void InstallBindings()
        {
            BindPools();
            BindFactories();
            BindNavigationMenu();
        }

        [Inject]
        public void Construct(DogsSettings dogsSettings)
        {
            _dogsSettings = dogsSettings;
        }

        private void BindPools()
        {
            Container.BindMemoryPool<DogView, DogView.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_dogsSettings.DogViewPrefab)
                .UnderTransformGroup("DogsViewPool");
        }

        private void BindNavigationMenu()
        {
            Container.Bind<NavigationMenu>().FromInstance(_navigationMenu).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<WindowsFactory>().FromNew().AsSingle().NonLazy();
        }
    }
}