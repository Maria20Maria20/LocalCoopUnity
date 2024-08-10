using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Gameplay.GameObjectComponents;
using TestLocalCoop.Scripts.Infra.AssetManagement.Interfaces;
using TestLocalCoop.Scripts.Infra.Extensions;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;

        private const string UIRootPrefabId = "UIRoot";
        private const string HudPrefabId = "HUD";
        private const string GameplaySectionName = "Gameplay";

        private Canvas _uiRoot;
        private HUDText _hud;

        public Canvas UIRoot => _uiRoot;

        public HUDText HUD => _hud;

        public UIFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }
        public async Task WarmUp()
        {
            await Task.WhenAll(
                _assetProvider.Load<GameObject>(UIRootPrefabId));
        }
        public async Task<Canvas> GetOrCreateUIRoot() =>
                _uiRoot ??= _container.InstantiatePrefab(await _assetProvider.Load<GameObject>(UIRootPrefabId),
                    _container.DefaultParent.Find(GameplaySectionName))
                    .With(root => root.name = "UI Root")
                    .GetComponent<Canvas>();

        public async Task<HUDText> GetOrCreateHUD() =>
            _hud ??= _container.InstantiatePrefab(await _assetProvider.Load<GameObject>(HudPrefabId), _uiRoot.transform)
            .GetComponentInChildren<HUDText>();
    }
}
