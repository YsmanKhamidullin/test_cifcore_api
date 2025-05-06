using UnityEngine;

namespace Game.Core.Gameplay.Dogs
{
    [CreateAssetMenu(menuName = "Game/Create DogsSettings", fileName = "DogsSettings", order = 0)]
    public class DogsSettings : ScriptableObject
    {
        [field: SerializeField]
        public DogView DogViewPrefab;
    }
}