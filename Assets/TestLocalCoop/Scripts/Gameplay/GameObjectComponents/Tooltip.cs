using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Gameplay.Data
{
    public class Tooltip : MonoBehaviour
    {
        private const string TooltipPrefabId = "TooltipE";
        private IPlayerUIFactory _playerUIFactory;
        public void ShowTooltip(string tooltipText, float pivotX)
        {
            GetComponent<RectTransform>().pivot = new Vector2(pivotX, GetComponent<RectTransform>().pivot.y);
            GetComponentInChildren<TextMeshProUGUI>().text = tooltipText;
        }
        public void HideTooltip(IPlayerUIFactory playerUIFactory)
        {
            _playerUIFactory = playerUIFactory;
            _playerUIFactory.RemoveElementUI(TooltipPrefabId, gameObject.transform.parent.gameObject);
        }
    }
}
