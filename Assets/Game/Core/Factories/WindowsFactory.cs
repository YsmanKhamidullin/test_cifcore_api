using Game.Core.Gameplay.Windows.Base;
using Zenject;

namespace Game.Core.Factories
{
    public class WindowsFactory : PrefabFactory<BaseWindow>
    {
        public WindowsFactory(DiContainer container) : base(container)
        {
        }
    }
}