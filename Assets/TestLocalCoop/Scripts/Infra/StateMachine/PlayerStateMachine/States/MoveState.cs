using Scripts.Services.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestLocalCoop.Scripts.Gameplay.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.StateMachine.PlayerStateMachine.States
{
    public class MoveState : ITickable
    {
        PlayerData _playerData;
        Vector3 directionMove;
        public MoveState(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void Tick()
        {
            directionMove = new(_playerData.InputService.DirectionMove.x, 0, _playerData.InputService.DirectionMove.y);
            _playerData.CharacterController.Move(Time.deltaTime * directionMove * _playerData.Speed);
        }
    }
}
