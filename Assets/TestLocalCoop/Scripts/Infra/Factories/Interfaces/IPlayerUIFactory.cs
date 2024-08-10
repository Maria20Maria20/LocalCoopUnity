using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace TestLocalCoop.Scripts.Infra.Factories.Interfaces
{
    public interface IPlayerUIFactory
    {
        Task WarmUp();
        Task<GameObject> GetOrCreateTooltip(string nameTooltip, string tooltipText, float pivotX);
        void RemoveElementUI(string nameElementUI, GameObject gameObjectElement);
        public GameObject Tooltip { get; set; }

    }
}
