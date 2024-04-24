using System;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Player
{
    public class PlayerMovement
    {
        public void Jump(Rigidbody2D rigidBody, float jumpForce, bool isJumping) 
        {
            if (Input.GetKeyDown(KeyCode.W) && isJumping)
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        public bool IsJumping(UnityEngine.Transform transform)
        {
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.4f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public float HorizontalMovement(bool isKeyUp)
        {
            if(isKeyUp)
            {
                return 0f;
            } else
            {
                return Input.GetAxisRaw("Horizontal");
            }
        }

        public bool isHorizontalKeyDown()
        {
            return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D);
        }


    }
}