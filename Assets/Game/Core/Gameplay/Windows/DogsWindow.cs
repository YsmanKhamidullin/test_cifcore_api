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

        [SerializeField]
        private DogsView _view;
        
        protected override async UniTask OnBeforeShow()
        {
            await base.OnBeforeShow();
            await _windowAnimations.ToOutsideRight(true);

            _presenter.Initialize(_view);
            _presenter.GetDogs();
        }

        protected override async UniTask OnAfterShow()
        {
            await base.OnAfterShow();
            await _windowAnimations.ToCenter();
        }

        protected override async UniTask OnBeforeHide()
        {
            await base.OnBeforeHide();
            _presenter.Cancel();
            await _windowAnimations.ToOutsideRight();
        }
    }
}