using Scripts.Services.Input;
using System.Collections;
using System.Collections.Generic;
using TestLocalCoop.Scripts.Gameplay.Data;
using TestLocalCoop.Scripts.Infra.Factories;
using TestLocalCoop.Scripts.Infra.StateMachine.PlayerStateMachine.States;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerData playerData;
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerUIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<MoveState>().AsSingle();
        }
    }
}
