using System;

namespace Game.Core.Gameplay.Dogs
{
    public interface IDogView
    {
        public void SetName(string label);
        public void SetLoading(bool isLoading);
    }
}