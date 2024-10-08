using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using TestLocalCoop.Scripts.Gameplay.Data;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using System;

namespace Scripts.Services.Input
{
    public class InputService : IInputService, Controls.IPlayer1Actions, Controls.IPlayer2Actions
    {
        private readonly IUIFactory _UIFactory;
        Controls _controls;
        Vector2 _directionMove;
        PlayerData _playerData;
        public Controls Controls { get => _controls; set => _controls = value; }
        public Vector2 DirectionMove { get => _directionMove; set => _directionMove = value; }
        public Action OnInteractable { get; set; }

        public InputService(PlayerData playerData, IUIFactory UIFactory)
        {
            _UIFactory = UIFactory;
            _playerData = playerData;
            _controls ??= new Controls();
            _controls.Enable();
            SetActionMap(_playerData.ActionMap);
        }
        public void SetActionMap(ActionMap actionMap)
        {
            switch (actionMap)
            {
                case ActionMap.Player1:
                    Controls.Player1.SetCallbacks(this);
                    break;

                case ActionMap.Player2:
                    Controls.Player2.SetCallbacks(this);
                    break;
            }
        }
        public void OnMovementTwo(InputAction.CallbackContext context)
        {
            _directionMove = context.ReadValue<Vector2>();
        }

        public void OnMovementOne(InputAction.CallbackContext context)
        {
            _directionMove = context.ReadValue<Vector2>();
        }

        public void OnInteractableOne(InputAction.CallbackContext context)
        {
            if (context.started && CanPickup())
            {
                OnInteractable?.Invoke();
            }
        }

        public void OnInteractableTwo(InputAction.CallbackContext context)
        {
            if (context.started && CanPickup())
            {
                OnInteractable?.Invoke();
            }
        }
        private bool CanPickup() => _playerData.PlayerUIFactory.Tooltip != null && _playerData.PlayerUIFactory.Tooltip.activeInHierarchy;
        ~InputService()
        {
            _controls.Disable();
        }

    }

}
