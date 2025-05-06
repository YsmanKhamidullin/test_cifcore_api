using UnityEngine;

namespace Game.Core.Gameplay.Weather
{
    [CreateAssetMenu(menuName = "Game/Create WeatherSettings", fileName = "WeatherSettings", order = 0)]
    public class WeatherSettings : ScriptableObject
    {
        [field: SerializeField]
        public float UpdateTime { get; private set; } = 5f;

        [field: SerializeField]
        public Sprite DefaultIcon { get; private set; }
    }
}