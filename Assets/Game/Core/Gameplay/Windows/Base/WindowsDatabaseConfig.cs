using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Gameplay.Windows.Base
{
    [CreateAssetMenu(menuName = "Game/Create WindowsDatabaseConfig", fileName = "WindowsDatabaseConfig", order = 0)]
    public class WindowsDatabaseConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<WindowTab> WindowTabPrefabs { get; private set; }

        [field: SerializeField]
        public ConfirmPopUp ConfirmPopUpPrefab { get; private set; }
    }
}