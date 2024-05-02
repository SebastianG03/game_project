using Assets.Scripts.Entities.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement
{
    public class PlayerInput : IPlayerInput
    {
        public Vector2 FrameInput => throw new NotImplementedException();

        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;
    }

}

