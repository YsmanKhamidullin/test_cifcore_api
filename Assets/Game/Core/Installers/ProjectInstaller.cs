using Game.Core.Services;
using Zenject;

namespace Game.Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<WebService>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WeatherService>().FromNew().AsSingle().NonLazy();
        }
    }
}