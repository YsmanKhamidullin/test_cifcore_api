using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Factories;
using Game.Core.Gameplay.Windows;
using Game.Core.Gameplay.Windows.Base;
using Game.Core.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.Gameplay.Dogs
{
    public class DogsPanelsPresenter : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private Transform _panelsParent;


        private Dictionary<string, DogView> _panels = new();
        private CompositeDisposable _disposables = new();

        private ConfirmPopUp _currentDogInfoPopUp;
        private DogsService _dogsService;
        private DogsSettings _dogsSettings;
        private WindowsFactory _windowsFactory;
        private DogView.Pool _pool;
        private WindowsDatabaseConfig _windowsDatabaseConfig;

        private CancellationTokenSource _dogInfoCancellationToken = new();


        [Inject]
        public void Construct(DogsService dogsService,
            DogsSettings dogsSettings,
            DogView.Pool pool,
            WindowsFactory windowsFactory,
            WindowsDatabaseConfig windowsDatabaseConfig)
        {
            _pool = pool;
            _dogsService = dogsService;
            _dogsSettings = dogsSettings;
            _windowsFactory = windowsFactory;
            _windowsDatabaseConfig = windowsDatabaseConfig;
        }

        public void Initialize()
        {
            Dispose();
            Clear();
            _dogInfoCancellationToken = new CancellationTokenSource();
        }

        public void Add(DogModel dog)
        {
            var panel = _pool.Spawn(_panelsParent, dog);
            _panels.Add(dog.Id, panel);

            var label = $"{_panels.Count.ToString()}. {dog.Name}";
            panel.SetName(label);
            panel.Button.onClick.AddListener(() => ShowSpecificDogInfo(dog, panel));
        }

        public void Remove(DogModel dog)
        {
            if (_panels.TryGetValue(dog.Id, out var element))
            {
                _pool.Despawn(element);
                _panels.Remove(dog.Id);
            }
        }

        public void Clear()
        {
            foreach (var element in _panels.Values)
            {
                _pool.Despawn(element);
            }

            _panels.Clear();
        }


        private void ShowSpecificDogInfo(DogModel model, IDogView view)
        {
            _dogInfoCancellationToken?.Cancel();
            _dogInfoCancellationToken?.Dispose();
            _dogInfoCancellationToken = new CancellationTokenSource();
            LoadSpecificDog(model, view, _dogInfoCancellationToken.Token).Forget();
        }

        private async UniTask LoadSpecificDog(DogModel model, IDogView view, CancellationToken token)
        {
            view.SetLoading(true);
            DogInfoModel dog = await _dogsService.GetDogById(model.Id, token);
            view.SetLoading(false);

            ConfirmPopUp popUp = _windowsFactory.Instantiate(_windowsDatabaseConfig.ConfirmPopUpPrefab, transform);
            string title = dog.Name;
            string hypoallergenicText = "Hypoallergenic: " + (dog.Hypoallergenic ? "Yes" : "No");
            string description = $"{dog.Description}\n{hypoallergenicText}.";
            popUp.Initialize(title, description);
            await popUp.TryShow();
        }

        public void Dispose()
        {
            _dogInfoCancellationToken?.Cancel();
            _dogInfoCancellationToken?.Dispose();
            _dogInfoCancellationToken = null;
            _disposables.Clear();
            if (_currentDogInfoPopUp != null)
            {
                Destroy(_currentDogInfoPopUp);
            }
        }
    }
}