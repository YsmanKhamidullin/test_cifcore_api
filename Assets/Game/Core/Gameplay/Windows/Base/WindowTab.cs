using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Gameplay.Windows.Base
{
    [Serializable]
    public class WindowTab
    {
        [field: SerializeField]
        public BaseWindow Window { get; private set; }

        [field: SerializeField]
        public Toggle Toggle { get; private set; }
        
        public IObservable<bool> Toggled => Toggle.OnValueChangedAsObservable();

        public void Select()
        {
            Toggle.isOn = true;
        }

        public WindowTab(BaseWindow window, Toggle toggle)
        {
            Window = window;
            Toggle = toggle;
        }
    }
}