using System;
using Game.Core.CanvasElements;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Core.Gameplay.Dogs
{
    public class DogView : MonoBehaviour, IDogView
    {
        public Button Button => _button;

        [SerializeField]
        private TMP_Text _labelText;

        [SerializeField]
        private LoadingView _loadingView;

        [SerializeField]
        private Button _button;

        public void SetName(string label)
        {
            _labelText.text = label;
        }

        public void SetLoading(bool isLoading)
        {
            _loadingView.Set(isLoading);
        }

        public class Pool : MonoMemoryPool<Transform, DogModel, DogView>
        {
            protected override void Reinitialize(Transform parent, DogModel model, DogView item)
            {
                item.transform.SetParent(parent, false);
                item.SetName(model.Name);
                item._button.onClick.RemoveAllListeners();
            }
        }
    }
}