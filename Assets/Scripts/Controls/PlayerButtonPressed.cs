using System;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class PlayerButtonPressed
    {
        public bool IsJumpButtonDown(string axisName)
        {
            return Input.GetButtonDown(axisName);
        }

        public float HorizontalMovement(bool isKeyUp, String horizontalAxisName)
        {
            if (isKeyUp)
            {
                return 0f;
            }
            else
            {
                return Input.GetAxisRaw(horizontalAxisName);
            }
        }

        public bool isHorizontalButtonDown(String horizontalAxisName)
        {
            return Input.GetButtonDown(horizontalAxisName);
        }


    }
}