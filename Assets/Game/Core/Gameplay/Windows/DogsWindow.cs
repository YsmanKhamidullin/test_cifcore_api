using Cysharp.Threading.Tasks;
using Game.Core.Gameplay.Dogs;
using Game.Core.Gameplay.Windows.Base;
using UnityEngine;

namespace Game.Core.Gameplay.Windows
{
    public class DogsWindow : BaseWindow
    {
        [SerializeField]
        private DogsPresenter _presenter;

        protected override async UniTask OnBeforeShow()
        {
            await base.OnBeforeShow();
            _presenter.Initialize();
            _presenter.UpdateDogsList().Forget();
            await _windowAnimations.ToOutsideRight(true);
        }

        protected override async UniTask OnAfterShow()
        {
            await base.OnAfterShow();
            await _windowAnimations.ToCenter();
        }

        protected override async UniTask OnBeforeHide()
        {
            await base.OnBeforeHide();
            _presenter.Dispose();
            await _windowAnimations.ToOutsideRight();
        }
    }
}