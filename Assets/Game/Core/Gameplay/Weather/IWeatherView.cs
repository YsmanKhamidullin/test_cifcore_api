using UnityEngine;

namespace Game.Core.Gameplay.Weather
{
    public interface IWeatherView
    {
        public void SetIcon(Sprite sprite);
        public void SetLabel(string label);
        public void SetLoading(bool isLoading);
    }
}