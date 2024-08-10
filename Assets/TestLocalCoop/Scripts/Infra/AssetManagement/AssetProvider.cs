using System.Collections.Generic;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Infra.AssetManagement.Interfaces;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace TestLocalCoop.Scripts.Infra.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new(); 

        public void Initialize() =>
            Addressables.InitializeAsync();

        public async Task<T> Load<T>(string key) where T : class
        {
            if (_completedCache.TryGetValue(key, out var completedHandle))
                return completedHandle.Result as T; 

            var handle = Addressables.LoadAssetAsync<T>(key); 

            return await RunWithCacheOnComplete(handle: handle, cacheKey: key); 
        }
        public void Release(string key) 
        {
            if (!_handles.ContainsKey(key)) 
                return;

            foreach (var handle in _handles[key])
                Addressables.Release(handle); 

            _completedCache.Remove(key);
            _handles.Remove(key);
        }

        public void Cleanup() 
        {
            if (_handles.Count == 0)
                return;

            foreach (var resourceHandles in _handles.Values)
                foreach (var handle in resourceHandles)
                    Addressables.Release(handle);

            _completedCache.Clear();
            _handles.Clear();
        }


        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey)
            where T : class 
        {
            handle.Completed += completeHandle =>
                            _completedCache[cacheKey] = completeHandle;

            AddHandle(cacheKey, handle);
            return await handle.Task;
        }


        private void AddHandle(string key, AsyncOperationHandle handle) 
        {
            if (!_handles.TryGetValue(key, out var resourceHandles)) 
            {
                resourceHandles = new List<AsyncOperationHandle>(); 
                _handles[key] = resourceHandles; 
            }
            resourceHandles.Add(handle); 
        }
    }
}