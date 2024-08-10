using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLocalCoop.Scripts.Infra.Factories;
using TestLocalCoop.Scripts.Infra.StateMachine.GameStates;
using TestLocalCoop.Scripts.Infra.StateMachine.GameStates.Interfaces;
using Zenject;

namespace TestLocalCoop.Scripts.Infra.StateMachine
{
    public class GameStateMachine : IInitializable
    {
        private readonly GameStateFactory _gameStateFactory;

        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;
        public GameStateMachine(GameStateFactory stateFactory)
        {
            _gameStateFactory = stateFactory;
        }

        public void Initialize()
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(LoadLevelState)] = _gameStateFactory.CreateState<LoadLevelState>()
            };
            Enter<LoadLevelState>();
        }
        public void Enter<TState>() where TState : class, IState => ChangeState<TState>().Enter();
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            var state = GetState<TState>();
            _currentState = state;
            return state;
        }
        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;

    }
}
