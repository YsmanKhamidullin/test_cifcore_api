using System;
using System.Collections.Concurrent;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Core.Services
{
    public class WebService : IDisposable
    {
        private readonly ConcurrentQueue<Func<UniTask>> _requestQueue = new ConcurrentQueue<Func<UniTask>>();
        private bool _isProcessing;

        public async UniTask<T> Get<T>(string url, CancellationToken cancellationToken) where T : class
        {
            Debug.Log($"Web.Get: {url}");
            var taskCompletionSource = new UniTaskCompletionSource<T>();
            _requestQueue.Enqueue(async () =>
            {
                var result = await GetParsed<T>(url, cancellationToken).SuppressCancellationThrow();

                if (result.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                    return;
                }

                taskCompletionSource.TrySetResult(result.Result);
            });

            if (!_isProcessing)
            {
                ProcessQueue().Forget();
            }

            return await taskCompletionSource.Task;
        }

        public async UniTask<Sprite> GetSprite(string url, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new UniTaskCompletionSource<Sprite>();
            _requestQueue.Enqueue(async () =>
            {
                var request = UnityWebRequestTexture.GetTexture(url);
                var result = await request.SendWebRequest()
                    .WithCancellation(cancellationToken)
                    .SuppressCancellationThrow();

                if (result.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                    return;
                }

                var texture = DownloadHandlerTexture.GetContent(result.Result);
                if (texture == null)
                {
                    taskCompletionSource.TrySetCanceled();
                    Debug.LogWarning($"Can't parse texture: {url}");
                    return;
                }

                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                taskCompletionSource.TrySetResult(sprite);
            });

            if (!_isProcessing)
            {
                ProcessQueue().Forget();
            }

            return await taskCompletionSource.Task;
        }

        public void Dispose()
        {
            _requestQueue.Clear();
            _isProcessing = false;
        }

        private async UniTaskVoid ProcessQueue()
        {
            _isProcessing = true;
            while (_requestQueue.TryDequeue(out var request))
            {
                await request();
            }

            _isProcessing = false;
        }

        private static async UniTask<T> GetParsed<T>(string url, CancellationToken cancellationToken)
            where T : class
        {
            var request = UnityWebRequest.Get(url);
            var operation = await request.SendWebRequest().WithCancellation(cancellationToken);

            if (operation.result == UnityWebRequest.Result.Success)
            {
                return JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
            }

            Debug.LogWarning($"Request {url} failed: {operation.result}");
            return null;
        }
    }
}