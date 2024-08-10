using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Infra.AssetManagement.Interfaces;
using TestLocalCoop.Scripts.Infra.Extensions;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Factories
{
    public class CameraFactory : ICameraFactory
    {
        [CanBeNull] public GameObject Camera { get; private set; }

        [CanBeNull] public GameObject MainCamera { get; private set; }
        private readonly IAssetProvider _assetProvider;
        private readonly string CameraPrefabId = "PlayerCamera";
        private readonly string MainCameraPrefabId = "MainCamera";
        private readonly DiContainer _container;
        public CameraFactory(IAssetProvider assetProvider, DiContainer container)
        {
            _assetProvider = assetProvider;
            _container = container;
        }
        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(CameraPrefabId);
            await _assetProvider.Load<GameObject>(MainCameraPrefabId);
        }
        public async Task<GameObject> Create(string layerMask)
        {
            var prefab = await _assetProvider.Load<GameObject>(CameraPrefabId);
            var camera = Object.Instantiate(prefab);
            camera.layer = LayerMask.NameToLayer(layerMask);
            _container.InjectGameObject(camera);
            return Camera = camera;
        }
        public async Task<GameObject> CreateMainCamera(float rectViewportX, string nameMainMenu, string cullingMask)
        {
            var prefab = await _assetProvider.Load<GameObject>(MainCameraPrefabId);
            var camera = Object.Instantiate(prefab).With(c => c.name = nameMainMenu);
            var cameraComponent = camera.GetComponent<Camera>();
            cameraComponent.rect = new Rect(rectViewportX, camera.GetComponent<Camera>().rect.y,
                 camera.GetComponent<Camera>().rect.width, camera.GetComponent<Camera>().rect.height);
            cameraComponent.cullingMask &= ~(1 << LayerMask.NameToLayer(cullingMask)); //hide select culling mask
            return MainCamera = camera;
        }
    }
}
