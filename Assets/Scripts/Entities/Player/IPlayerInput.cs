using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    public interface IPlayerInput
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}