using UnityEngine;
using Zenject;

namespace Game.Core.Installers
{
    public class BootStrapInstaller : MonoInstaller
    {
        [SerializeField]
        private BootStrap _bootStrap;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootStrap>().FromInstance(_bootStrap).AsSingle().NonLazy();
        }
    }
}