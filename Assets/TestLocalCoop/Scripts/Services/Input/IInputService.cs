using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Services.Input
{
    public interface IInputService
    {
        Controls Controls { get; set; }
        Vector2 DirectionMove {  get; set; }
        Action OnInteractable { get; set; }
        void SetCallback(Controls.Player1Actions playerActions);
        void SetCallback(Controls.Player2Actions playerActions);
    }

}