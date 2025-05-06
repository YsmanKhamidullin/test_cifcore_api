using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Factories;
using Game.Core.Gameplay.Windows.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Core.Gameplay.Windows
{
    public class NavigationMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform _windowsParent;

        [SerializeField]
        private Transform _tabsParent;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        [SerializeField]
        private HorizontalLayoutGroup _horizontalLayoutGroup;

        private List<WindowTab> _windowTabs = new();

        private WindowsFactory _windowsFactory;
        private BaseWindow _currentWindow;

        [Inject]
        public void Construct(WindowsFactory windowsFactory)
        {
            _windowsFactory = windowsFactory;
        }

        public void Initialize(List<WindowTab> windowTabPrefabs)
        {
            CreateWindowTabs(windowTabPrefabs);
        }

        private void CreateWindowTabs(List<WindowTab> windowsDatabaseConfig)
        {
            ClearWindows();
            foreach (var windowTabPrefab in windowsDatabaseConfig)
            {
                var tab = CreateWindowTab(windowTabPrefab);
                _windowTabs.Add(tab);

                tab.Toggled
                    .AsObservable()
                    .Subscribe(_ => SwitchWindow(tab).Forget())
                    .AddTo(tab.Window);
            }

            SortTabs().Forget();
        }

        private async UniTask SortTabs()
        {
            _horizontalLayoutGroup.enabled = true;
            await UniTask.WaitForEndOfFrame(this);
            _horizontalLayoutGroup.enabled = false;
        }

        private void ClearWindows()
        {
            foreach (var windowTab in _windowTabs)
            {
                Destroy(windowTab.Window.gameObject);
                Destroy(windowTab.Toggle.gameObject);
            }

            _windowTabs.Clear();
        }

        private async UniTask SwitchWindow(WindowTab windowTab)
        {
            if (windowTab.Toggle.isOn)
            {
                await windowTab.Window.TryShow();
            }
            else
            {
                await windowTab.Window.TryHide();
            }
        }

        private WindowTab CreateWindowTab(WindowTab windowTabPrefab)
        {
            var window = _windowsFactory.Instantiate(windowTabPrefab.Window, _windowsParent);
            var toggle = Instantiate(windowTabPrefab.Toggle, _tabsParent);
            toggle.group = _toggleGroup;
            window.gameObject.SetActive(false);
            var result = new WindowTab(window, toggle);
            return result;
        }
    }
}