using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core.Gameplay.Windows.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Gameplay.Windows
{
    public class ConfirmPopUp : BaseWindow
    {
        [SerializeField]
        private TMP_Text _titleLabel;

        [SerializeField]
        private TMP_Text _descriptionLabel;

        [SerializeField]
        private Button _confirm;

        [SerializeField]
        private Button _bgButton;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        public void Initialize(string title, string mainText)
        {
            _titleLabel.text = title;
            _descriptionLabel.text = mainText;

            _confirm.onClick.RemoveAllListeners();
            _confirm.onClick.AddListener(() => { TryHide().Forget(); });

            _bgButton.onClick.RemoveAllListeners();
            _bgButton.onClick.AddListener(() => { TryHide().Forget(); });
        }

        protected override async UniTask OnBeforeShow()
        {
            await base.OnBeforeShow();
            _canvasGroup.alpha = 0f;
        }

        protected override async UniTask OnAfterShow()
        {
            await base.OnAfterShow();
            await _canvasGroup.DOFade(1f, 0.75f).SetEase(Ease.OutQuad).ToUniTask();
        }

        protected override async UniTask OnBeforeHide()
        {
            await base.OnBeforeHide();
            await _canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InQuad).ToUniTask();
        }

        protected override async UniTask OnAfterHide()
        {
            await base.OnAfterHide();
        }
    }
}