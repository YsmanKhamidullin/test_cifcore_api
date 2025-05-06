using Cysharp.Threading.Tasks;
using Game.Core.Gameplay.Weather;
using Game.Core.Gameplay.Windows.Base;
using UnityEngine;

namespace Game.Core.Gameplay.Windows
{
    public class WeatherWindow : BaseWindow
    {
        [SerializeField]
        private WeatherPresenter _presenter;

        [SerializeField]
        private WeatherView _view;

        protected override async UniTask OnBeforeShow()
        {
            await base.OnBeforeShow();
            await _windowAnimations.ToOutsideLeft(true);

            _presenter.Initialize(_view);
            _presenter.StartUpdate();
        }

        protected override async UniTask OnAfterShow()
        {
            await base.OnAfterShow();
            await _windowAnimations.ToCenter();
        }

        protected override async UniTask OnBeforeHide()
        {
            await base.OnBeforeHide();
            _presenter.StopUpdate();
            await _windowAnimations.ToOutsideLeft();
        }
    }
}