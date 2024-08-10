using Scripts.Services.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using TestLocalCoop.Scripts.Infra.Factories;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TestLocalCoop.Scripts.Gameplay.Data
{
    public class PlayerData : MonoBehaviour
    {
        #region Variables
        public GameObject PickupedObject;
        [SerializeField] private string interactableTooltip;
        [SerializeField] private float tooltipPivotX;
        [SerializeField] private bool isFirstPlayer;
        [SerializeField] private CharacterController _characterController;
        private float _speed = 10;
        private IInputService _inputService;
        private IPlayerUIFactory _playerUIFactory;
        #endregion

        #region Properties
        public float Speed => _speed;
        public IInputService InputService => _inputService;
        public IPlayerUIFactory PlayerUIFactory => _playerUIFactory;
        public string InteractableTooltip => interactableTooltip;
        public float TooltipPivotX => tooltipPivotX;
        public bool IsFirstPlayer => isFirstPlayer;
        public CharacterController CharacterController => _characterController;
        #endregion
        [Inject]
        private void Construct(IInputService inputService, IPlayerUIFactory playerUIFactory)
        {
            _inputService = inputService;
            _playerUIFactory = playerUIFactory;
        }

    }
}
