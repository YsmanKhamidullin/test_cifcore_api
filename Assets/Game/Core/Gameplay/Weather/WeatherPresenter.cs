using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.Gameplay.Weather
{
    public class WeatherPresenter : MonoBehaviour, IDisposable
    {
        private readonly ReactiveProperty<string> _weatherLabel = new();
        private readonly ReactiveProperty<bool> _isLoading = new();
        private readonly ReactiveProperty<Sprite> _icon = new();
        private readonly CompositeDisposable _disposables = new();

        private WeatherService _weatherService;
        private WeatherSettings _weatherSettings;
        private WeatherView _view;

        private CancellationTokenSource _cancelToken;

        [Inject]
        public void Construct(WeatherService weatherService,
            WeatherSettings weatherSettings)
        {
            _weatherService = weatherService;
            _weatherSettings = weatherSettings;
        }

        public void Initialize(WeatherView view)
        {
            _view = view;

            _weatherLabel
                .Subscribe(view.SetLabel)
                .AddTo(_disposables);
            _isLoading
                .Subscribe(view.SetLoading)
                .AddTo(_disposables);
            _icon
                .Subscribe(view.SetIcon)
                .AddTo(_disposables);
        }

        public void StartUpdate()
        {
            _cancelToken?.Dispose();
            _cancelToken = new CancellationTokenSource();

            RequestUpdateCurrentWeather();
            Observable.Interval(TimeSpan.FromSeconds(_weatherSettings.UpdateTime))
                .Subscribe(_ => RequestUpdateCurrentWeather())
                .AddTo(_disposables);
        }

        public void StopUpdate()
        {
            _cancelToken?.Cancel();
            _cancelToken?.Dispose();
            _cancelToken = null;
            _disposables.Clear();

            _isLoading.Value = false;
        }

        private void RequestUpdateCurrentWeather()
        {
            _isLoading.Value = true;
            UpdateCurrentWeatherAsync().Forget();
        }

        private async UniTask UpdateCurrentWeatherAsync()
        {
            var weather = await _weatherService
                .GetWeatherModelAsync(_cancelToken.Token)
                .SuppressCancellationThrow();

            if (weather.IsCanceled)
            {
                return;
            }

            var todayWeather = weather.Result.FirstOrDefault();
            if (todayWeather != null)
            {
                _weatherLabel.Value = $"{todayWeather.Name} - {todayWeather.Temperature}{todayWeather.TemperatureUnit}";

                var icon = await _weatherService
                    .GetWeatherSprite(todayWeather.Icon, _cancelToken.Token)
                    .SuppressCancellationThrow();

                if (icon.IsCanceled)
                {
                    return;
                }

                _icon.Value = icon.Result;
                _isLoading.Value = false;
            }
            else
            {
                _icon.Value = _weatherSettings.DefaultIcon;
                _weatherLabel.Value = "Try again later...";
                _isLoading.Value = false;
            }
        }

        public void Dispose()
        {
            StopUpdate();
        }
    }
}