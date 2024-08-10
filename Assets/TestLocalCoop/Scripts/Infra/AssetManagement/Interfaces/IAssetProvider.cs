using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestLocalCoop.Scripts.Infra.AssetManagement.Interfaces
{
    public interface IAssetProvider
    {
        public Task<T> Load<T>(string key) where T : class;
        public void Release(string key);
        public void Cleanup();
    }
}


