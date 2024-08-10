using System.Collections;
using System.Collections.Generic;
using TestLocalCoop.Scripts.Infra.StateMachine.GameStates.Interfaces;
using UnityEngine;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Factories
{
    public class GameStateFactory
    {
        private readonly DiContainer _container;

        public GameStateFactory(DiContainer container) => _container = container;
        public T CreateState<T>() where T : IExitableState => _container.Resolve<T>();
    }

}
