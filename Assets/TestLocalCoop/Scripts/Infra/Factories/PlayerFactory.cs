using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Infra.AssetManagement.Interfaces;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private const string FirstPlayerPrefabId = "Player1";
        private const string SecondPlayerPrefabId = "Player2";
        public GameObject Player { get; private set; }
        private readonly IAssetProvider _assetProvider;
        private readonly ICameraFactory _cameraFactory;
        private readonly DiContainer _container;
        public PlayerFactory(IAssetProvider assetProvider, ICameraFactory cameraFactory, DiContainer container)
        {
            _assetProvider = assetProvider;
            _cameraFactory = cameraFactory;
            _container = container;
        }
        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(FirstPlayerPrefabId);
            await _assetProvider.Load<GameObject>(SecondPlayerPrefabId);
        }
        public async Task<GameObject> GetOrCreatePlayer(string prefabId, Vector3 at)
        {
            var prefab = await _assetProvider.Load<GameObject>(prefabId);
            var hero = Object.Instantiate(prefab, at, Quaternion.identity);
            _container.InjectGameObject(hero); //for PlayerData Inject (add component default gameobject context in player instance)

            _cameraFactory.Camera.GetComponent<CinemachineVirtualCamera>().Follow = hero.transform;
            _cameraFactory.Camera.GetComponent<CinemachineVirtualCamera>().LookAt = hero.transform;

            return Player = hero;
        }
    }
}
