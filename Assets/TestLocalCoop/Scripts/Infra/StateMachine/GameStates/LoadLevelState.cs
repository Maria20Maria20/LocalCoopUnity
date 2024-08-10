using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using TestLocalCoop.Scripts.Infra.StateMachine.GameStates.Interfaces;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.StateMachine.GameStates
{
    public class LoadLevelState : IState
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly ICameraFactory _cameraFactory;
        private readonly IUIFactory _UIFactory;
        public LoadLevelState(IPlayerFactory playerFactory, ICameraFactory cameraFactory, IUIFactory uIFactory)
        {
            _playerFactory = playerFactory;
            _cameraFactory = cameraFactory;
            _UIFactory = uIFactory;
        }

        public async void Enter()
        {
            await _playerFactory.WarmUp();
            await _cameraFactory.WarmUp();

            await InitUIRoot();
            await InitUI();

            CreatePlayerCamera("Player1", -0.5f, "MainCamera1", "Player2");
            CreatePlayer("Player1", Vector3.zero);

            CreatePlayerCamera("Player2", 0.5f, "MainCamera2", "Player1");
            CreatePlayer("Player2", Vector3.zero + new Vector3(1, 0, 0));
        }
        private async Task InitUIRoot()
        {
            await _UIFactory.GetOrCreateUIRoot();
        }
        private async Task InitUI()
        {
            await _UIFactory.GetOrCreateHUD();
        }
        private async void CreatePlayerCamera(string layerMask, float rectViewportX, string nameMainCamera, string cullingMask)
        {
            await _cameraFactory.Create(layerMask);
            await _cameraFactory.CreateMainCamera(rectViewportX, nameMainCamera, cullingMask);
        }
        private async void CreatePlayer(string prefabId, Vector3 at)
        {
            await _playerFactory.GetOrCreatePlayer(prefabId, at);
        }
        public void Exit()
        {

        }


    }
}
