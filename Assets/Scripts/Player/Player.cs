using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {

        private Rigidbody2D _RigidBody;
        private float Horizontal;
        private bool IsGrounded;
        PlayerMovement playerMovement;

        [SerializeField]
        private float Speed = 5;
        [SerializeField]
        private float JumpForce = 10;

        void Start()
        {
            _RigidBody = GetComponent<Rigidbody2D>();
            playerMovement = new PlayerMovement();
        }

        void Update()
        {
            Debug.DrawRay(transform.position, Vector3.down * 0.4f, Color.red);
            float vertical = Input.GetAxisRaw("Vertical2");
            IsGrounded = playerMovement.IsNotJumping(transform);

            Move();
        }

        private void Move()
        {
            bool horizontalKeyDown = playerMovement.isHorizontalKeyDown("Horizontal2");
            Horizontal = playerMovement.HorizontalMovement(horizontalKeyDown, "Horizontal2");
            playerMovement.Jump(_RigidBody, JumpForce, IsGrounded, Input.GetKeyDown(KeyCode.W));
        }

        private void FixedUpdate()
        {
            _RigidBody.velocity = new Vector2(Horizontal, _RigidBody.velocity.y);
        }
    }
}