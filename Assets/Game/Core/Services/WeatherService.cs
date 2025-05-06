using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Gameplay.Weather;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Game.Core.Services
{
    public class WeatherService
    {
        private const string ApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        private readonly WebService _webService;

        public WeatherService(WebService webService)
        {
            _webService = webService;
        }

        public async UniTask<IList<WeatherModel>> GetWeatherModelAsync(CancellationToken cancelToken)
        {
            var response = await _webService
                .Get<JObject>(ApiUrl, cancelToken)
                .SuppressCancellationThrow();

            if (cancelToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            if (response.Result == null)
            {
                Debug.LogWarning($"Weather server error");
                return new List<WeatherModel>();
            }

            return ParseWeatherJson(response.Result);
        }

        public async UniTask<Sprite> GetWeatherSprite(string iconUrl, CancellationToken cancelToken)
        {
            var response = await _webService
                .GetSprite(iconUrl, cancelToken)
                .SuppressCancellationThrow();

            if (cancelToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            if (response.Result == null)
            {
                return null;
            }

            return response.Result;
        }

        /// <summary>
        /// Парсинг данных прогноза погоды из JSON.
        /// </summary>
        /// <param name="response">Объект JSON.</param>
        /// <returns>Список прогнозов погоды.</returns>
        private static IList<WeatherModel> ParseWeatherJson(JObject response)
        {
            var periods = response["properties"]["periods"];
            var models = new List<WeatherModel>();

            foreach (var period in periods)
            {
                var model = new WeatherModel
                {
                    Name = period["name"]?.ToString(),
                    StartTime = DateTime.Parse(period["startTime"]?.ToString()),
                    EndTime = DateTime.Parse(period["endTime"]?.ToString()),
                    Temperature = int.Parse(period["temperature"]?.ToString() ?? string.Empty),
                    TemperatureUnit = period["temperatureUnit"]?.ToString(),
                    Icon = period["icon"]?.ToString(),
                };
                models.Add(model);
            }

            return models;
        }
    }
}