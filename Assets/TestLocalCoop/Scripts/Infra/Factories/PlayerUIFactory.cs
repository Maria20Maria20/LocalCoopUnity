using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Gameplay.Data;
using TestLocalCoop.Scripts.Infra.AssetManagement.Interfaces;
using TestLocalCoop.Scripts.Infra.Extensions;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Factories
{

    public class PlayerUIFactory : IPlayerUIFactory
    {
        private const string TooltipPrefabId = "Tooltip";
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _UIFactory;
        private GameObject _tooltip;
        [CanBeNull] public GameObject Tooltip { get => _tooltip; set => _tooltip = value; }

        public PlayerUIFactory(IAssetProvider assetProvider, DiContainer container, IUIFactory uIFactory)
        {
            _assetProvider = assetProvider;
            _container = container;
            _UIFactory = uIFactory;
        }

        public async Task WarmUp()
        {
            await Task.WhenAll(
                _assetProvider.Load<GameObject>(TooltipPrefabId));

        }
        public async Task<GameObject> GetOrCreateTooltip(string nameTooltip, string tooltipText, float pivotX)
        {
            if (_tooltip != null && _tooltip.name == nameTooltip)
            {
                _tooltip.SetActive(true);
                return _tooltip;
            }
            _tooltip = _container.InstantiatePrefab(
                await _assetProvider.Load<GameObject>(TooltipPrefabId), _UIFactory.UIRoot.transform)
            .With(tooltip => _container.InjectGameObject(tooltip.gameObject))
            .With(tooltip => tooltip.name = nameTooltip);
            _tooltip.SetActive(true);
            _tooltip.GetComponentInChildren<Tooltip>().ShowTooltip(tooltipText, pivotX);
            return _tooltip;
        }

        public void RemoveElementUI(string nameElementUI, GameObject gameObjectElement)
        {
            _assetProvider.Release(nameElementUI);
            gameObjectElement.SetActive(false);
        }
    }
}
