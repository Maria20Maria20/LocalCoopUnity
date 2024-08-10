using System.Collections;
using System.Collections.Generic;
using TestLocalCoop.Scripts.Infra.StateMachine;
using TestLocalCoop.Scripts.Infra.StateMachine.GameStates;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStates();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        }
        private void BindStates()
        {
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
        }
    }
}
