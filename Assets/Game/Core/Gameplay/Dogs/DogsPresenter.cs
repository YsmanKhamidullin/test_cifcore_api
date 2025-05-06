using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.CanvasElements;
using Game.Core.Gameplay.Windows;
using Game.Core.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.Gameplay.Dogs
{
    public class DogsPresenter : MonoBehaviour
    {
        [SerializeField]
        private DogsPanelsPresenter _dogsPanelsPresenter;

        private readonly ReactiveCollection<DogModel> _dogs = new();
        private readonly ReactiveProperty<bool> _isLoading = new();
        private readonly CompositeDisposable _disposables = new();

        [SerializeField]
        private LoadingView _loadingView;

        private DogsService _dogsService;
        private DogsSettings _dogsSettings;
        private DogView.Pool _pool;
        private CancellationTokenSource _cancellationToken = new();

        [Inject]
        public void Construct(DogsService dogsService, DogsSettings dogsSettings, DogView.Pool pool)
        {
            _pool = pool;
            _dogsService = dogsService;
            _dogsSettings = dogsSettings;
        }

        public void Initialize()
        {
            Dispose();
            _dogsPanelsPresenter.Initialize();
            
            _cancellationToken = new CancellationTokenSource();

            _isLoading
                .Subscribe(_loadingView.Set)
                .AddTo(_disposables);

            _dogs.ObserveAdd()
                .Subscribe(change => _dogsPanelsPresenter.Add(change.Value))
                .AddTo(_disposables);

            _dogs.ObserveRemove()
                .Subscribe(change => _dogsPanelsPresenter.Remove(change.Value))
                .AddTo(_disposables);

            _dogs.ObserveReset()
                .Subscribe(_ => _dogsPanelsPresenter.Clear())
                .AddTo(_disposables);
        }

        public async UniTask UpdateDogsList()
        {
            _isLoading.Value = true;
            var dogs = await _dogsService.GetDogsList(_cancellationToken.Token);

            foreach (var d in dogs)
            {
                _dogs.Add(d);
            }

            _isLoading.Value = false;
        }

        public void Dispose()
        {
            _dogsPanelsPresenter.Dispose();
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
            _cancellationToken = null;
            _disposables.Clear();
        }
    }
}