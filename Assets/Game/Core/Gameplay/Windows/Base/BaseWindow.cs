using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Core.Gameplay.Windows.Base
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _windowContent;

        protected WindowAnimations _windowAnimations;

        protected virtual async void Awake()
        {
            _windowAnimations = new WindowAnimations();
            _windowAnimations.Initialize(_windowContent);
        }

        protected virtual void Start()
        {
        }

        public async UniTask TryShow()
        {
            if (gameObject.activeSelf)
            {
                return;
            }

            await OnBeforeShow();
            gameObject.SetActive(true);
            await OnAfterShow();
        }

        protected virtual async UniTask OnBeforeShow()
        {
        }

        protected virtual async UniTask OnAfterShow()
        {
        }

        public virtual async UniTask TryHide()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            await OnBeforeHide();
            gameObject.SetActive(false);
            await OnAfterHide();
        }

        protected virtual async UniTask OnBeforeHide()
        {
        }

        protected virtual async UniTask OnAfterHide()
        {
        }
    }
}