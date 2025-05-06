using Game.Core.Factories;
using Game.Core.Gameplay.Windows;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Core.Installers
{
    public class CanvasInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_navBar")]
        [SerializeField]
        private NavigationMenu _navigationMenu;


        public override void InstallBindings()
        {
            BindFactories();
            Container.Bind<NavigationMenu>().FromInstance(_navigationMenu).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<WindowsFactory>().FromNew().AsSingle().NonLazy();
        }
    }
}