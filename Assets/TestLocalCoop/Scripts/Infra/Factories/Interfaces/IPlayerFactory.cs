using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace TestLocalCoop.Scripts.Infra.Factories.Interfaces
{
    public interface IPlayerFactory
    {
        Task WarmUp();
        GameObject Player { get; }
        Task<GameObject> GetOrCreatePlayer(string prefabId, Vector3 at);
    }
}
