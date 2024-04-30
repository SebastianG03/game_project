using Assets.Scripts.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {

        //Entity Components
        private Rigidbody2D _rigidBody;
        private CapsuleCollider2D _collider;
        private PlayerMovement _playerMovement;       
        private ControlData _controlData;

        //Movement Variables
        private float Horizontal;
        private bool IsGrounded;
        private int RemainingJumps;
        private readonly int MaxJumps = 2;
        private bool _cachedQueryStartInColliders;
        private float _time;
       
        //Frame Input
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        //Player Stats
        private EntityStats _entityStats;
        public PlayerControlType controlType;
        public float JumpForce = 10;
        public float MaxHealth = 100;
        public float Damage = 10;
        public float Speed = 5;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }


        void Start()
        {
            _controlData = new ControlData(controlType);  
            _playerMovement = new PlayerMovement();
            _entityStats = new EntityStats(MaxHealth, Damage, Speed);
            RemainingJumps = MaxJumps;
            
        }

        void Update()
        {
            _time += Time.deltaTime;
            Debug.DrawRay(transform.position, Vector3.down * 0.35f, Color.red);
            Move();
            // float vertical = Input.GetAxisRaw("_controlData.VerticalControl");
            IsGrounded = _playerMovement.IsNotJumping(transform);
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
            };
        }

        private void FixedUpdate()
        {
            _rigidBody.velocity = new Vector2(Horizontal, _rigidBody.velocity.y);
            Jump();
             
        }

        private void Jump()
        {
            if (IsGrounded && _rigidBody.velocity.x != 0 && _rigidBody.velocity.y == 0 ||
                IsGrounded && _rigidBody.velocity.x == 0 && _rigidBody.velocity.y == 0 ||
                IsGrounded && _rigidBody.velocity.x != 0 && _rigidBody.velocity.y != 0
                )
            {
                RemainingJumps = MaxJumps;
            }

            _playerMovement.Jump(_rigidBody, JumpForce, IsGrounded, Input.GetButtonDown(_controlData.JumpControl), RemainingJumps);
            RemainingJumps -= 1;
        }

        private void Move()
        {
            bool horizontalButtonDown = _playerMovement.isHorizontalButtonDown(_controlData.HorizontalControl);
            Horizontal = _playerMovement.HorizontalMovement(horizontalButtonDown, _controlData.HorizontalControl);
            Debug.Log("Vx: " + _rigidBody.velocity.x + " Vy: " + _rigidBody.velocity.y);
            
        }

    }
}