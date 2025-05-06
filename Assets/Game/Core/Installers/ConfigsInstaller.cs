using Game.Core.Gameplay.Weather;
using Game.Core.Gameplay.Windows.Base;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Game/Installers/ConfigsInstaller")]
public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
{
    [field: SerializeField]
    public WeatherSettings WeatherSettings { get; private set; }

    [field: SerializeField]
    public WindowsDatabaseConfig WindowsDatabaseConfig { get; private set; }

    public override void InstallBindings()
    {
        Container.Bind<WeatherSettings>().FromInstance(WeatherSettings).AsSingle().NonLazy();
        Container.Bind<WindowsDatabaseConfig>().FromInstance(WindowsDatabaseConfig).AsSingle().NonLazy();
    }
}