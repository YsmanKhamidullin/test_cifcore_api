using Game.Core.CanvasElements;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Core.Gameplay.Weather
{
    public class WeatherView : MonoBehaviour, IWeatherView
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private LoadingView _loadingView;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetLabel(string label)
        {
            _label.text = label;
        }

        public void SetLoading(bool isLoading)
        {
            _loadingView.SetActive(isLoading);
        }
    }
}