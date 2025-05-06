using Game.Core.Gameplay.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// This file is auto-generated. Do not modify manually.

public static class GameResources
{
    public static class Configs
    {
        public static ConfigsInstaller ConfigsInstaller => Resources.Load<ConfigsInstaller>("Configs/ConfigsInstaller");
        public static Game.Core.Gameplay.Dogs.DogsSettings DogsSettings => Resources.Load<Game.Core.Gameplay.Dogs.DogsSettings>("Configs/DogsSettings");
        public static Game.Core.Gameplay.Weather.WeatherSettings WeatherSettings => Resources.Load<Game.Core.Gameplay.Weather.WeatherSettings>("Configs/WeatherSettings");
        public static Game.Core.Gameplay.Windows.Base.WindowsDatabaseConfig WindowsDatabaseConfig => Resources.Load<Game.Core.Gameplay.Windows.Base.WindowsDatabaseConfig>("Configs/WindowsDatabaseConfig");
    }
    public static class Prefabs
    {
        public static class Canvas
        {
            public static class Elements
            {
                public static Image BaseTabToggle => Resources.Load<Image>("Prefabs/Canvas/Elements/BaseTabToggle");
                public static Image Button => Resources.Load<Image>("Prefabs/Canvas/Elements/Button");
                public static Image Loading => Resources.Load<Image>("Prefabs/Canvas/Elements/Loading");
                public static TextMeshProUGUI Text__TMP_ => Resources.Load<TextMeshProUGUI>("Prefabs/Canvas/Elements/Text (TMP)");
            }
            public static class PopUp
            {
                public static ConfirmPopUp ConfirmPopUp => Resources.Load<ConfirmPopUp>("Prefabs/Canvas/PopUp/ConfirmPopUp");
            }
        }
        public static class Dogs
        {
            public static Image DogsTab => Resources.Load<Image>("Prefabs/Dogs/DogsTab");
            public static DogsWindow DogsWindow => Resources.Load<DogsWindow>("Prefabs/Dogs/DogsWindow");
            public static Image DogViewPanel => Resources.Load<Image>("Prefabs/Dogs/DogViewPanel");
        }
        public static class Weather
        {
            public static Image WeatherTab => Resources.Load<Image>("Prefabs/Weather/WeatherTab");
            public static WeatherWindow WeatherWindow => Resources.Load<WeatherWindow>("Prefabs/Weather/WeatherWindow");
        }
    }
    public static class Textures
    {
        public static Sprite dog_icon => Resources.Load<Sprite>("Textures/dog_icon");
        public static Sprite loading_icon => Resources.Load<Sprite>("Textures/loading_icon");
        public static Sprite square => Resources.Load<Sprite>("Textures/square");
        public static Sprite square_borders => Resources.Load<Sprite>("Textures/square_borders");
        public static Sprite tab => Resources.Load<Sprite>("Textures/tab");
        public static Sprite weather_icon => Resources.Load<Sprite>("Textures/weather_icon");
    }
    public static ProjectContext ProjectContext => Resources.Load<ProjectContext>("ProjectContext");
}
