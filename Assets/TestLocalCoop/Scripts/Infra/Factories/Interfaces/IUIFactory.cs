using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Gameplay.GameObjectComponents;
using UnityEngine;
namespace TestLocalCoop.Scripts.Infra.Factories.Interfaces
{
    public interface IUIFactory
    {
        Task WarmUp();
        public Canvas UIRoot { get; }
        public HUDText HUD { get; }
        Task<Canvas> GetOrCreateUIRoot();
        Task<HUDText> GetOrCreateHUD();
    }
}
