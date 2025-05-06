using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Core.Gameplay.Dogs;
using Game.Core.Gameplay.Weather;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Game.Core.Services
{
    public class DogsService
    {
        private readonly WebService _webService;

        public DogsService(WebService webService)
        {
            _webService = webService;
        }

        public async UniTask<List<DogModel>> GetDogsList(CancellationToken cancelToken)
        {
            string url = "https://dogapi.dog/api/v2/breeds";

            var response = await _webService
                .Get<JObject>(url, cancelToken)
                .SuppressCancellationThrow();

            if (cancelToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            if (response.Result == null)
            {
                Debug.LogWarning($"Dogs server error");
                return new List<DogModel>();
            }


            return ParseJson(response.Result);
        }

        public async UniTask<DogInfoModel> GetDogById(string id, CancellationToken cancelToken)
        {
            string url = $"https://dogapi.dog/api/v2/breeds/{id}";

            var response = await _webService
                .Get<JObject>(url, cancelToken)
                .SuppressCancellationThrow();

            if (cancelToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            if (response.Result == null)
            {
                Debug.LogWarning($"Dogs server error");
                return new DogInfoModel();
            }


            return ParseDogInfoJson(response.Result);
        }

        private List<DogModel> ParseJson(JObject response)
        {
            var data = response["data"];
            var models = new List<DogModel>();

            foreach (var element in data)
            {
                var model = new DogModel()
                {
                    Id = element["id"]?.ToString(),
                    Name = element["attributes"]?["name"]?.ToString(),
                    Description = element["attributes"]?["description"]?.ToString(),
                };
                models.Add(model);
            }

            return models;
        }

        private DogInfoModel ParseDogInfoJson(JObject response)
        {
            var data = response["data"];
            var model = new DogInfoModel()
            {
                Id = data["id"]?.ToString(),
                Name = data["attributes"]?["name"]?.ToString(),
                Description = data["attributes"]?["description"]?.ToString(),
                Hypoallergenic =
                    bool.TryParse(data["attributes"]?["hypoallergenic"]?.ToString(), out bool hypoallergenic) &&
                    hypoallergenic
            };
            return model;
        }
    }
}