using Scripts.Services.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using TestLocalCoop.Scripts.Gameplay.Data;
using TestLocalCoop.Scripts.Infra.Factories.Interfaces;
using UnityEngine;
using Zenject;

namespace TestLocalCoop.Scripts.Gameplay.GameObjectComponents
{
    public class PlayerCollisions : MonoBehaviour
    {
        IUIFactory _UIFactory;
        PlayerData _playerData;
        [Inject]
        private void Construct(IUIFactory UIFactory, PlayerData playerData)
        {
            _UIFactory = UIFactory;
            _playerData = playerData;
            _playerData.InputService.OnInteractable += Interaction;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                _playerData.PickupedObject = other.gameObject;
                _playerData.PlayerUIFactory.GetOrCreateTooltip("Tooltip" + _playerData.InteractableTooltip,
                    _playerData.InteractableTooltip, _playerData.TooltipPivotX);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                _playerData.PickupedObject = null;
                _playerData.PlayerUIFactory.RemoveElementUI("Tooltip" + _playerData.InteractableTooltip,
                    _playerData.PlayerUIFactory.Tooltip);
            }
        }
        private void Interaction()
        {
            _UIFactory.HUD.ChangeText(GameObject.FindGameObjectsWithTag("Interactable").Length - 1);
            Destroy(_playerData.PickupedObject);
            _playerData.PlayerUIFactory.RemoveElementUI("Tooltip" + _playerData.InteractableTooltip,
                _playerData.PlayerUIFactory.Tooltip);
        }
        private void OnDisable()
        {
            _playerData.InputService.OnInteractable -= Interaction;
        }

    }
}
