using Assets.Scripts.Controls;
using Assets.Scripts.Stats;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerInput
    {

        //Entity Components
        private Rigidbody2D _rigidBody;
        private CapsuleCollider2D _collider;
        //private PlayerMovement _playerMovement;       
        [SerializeField] private ScriptableConstants _constants;
        private PlayerButtonPressed _buttonsPressed;



        //Movement Variables
        private bool _cachedQueryStartInColliders;
        private float _time;
       
        //IPlayerController
        private FrameInput _frameInput;
        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;
        private Vector2 _frameVelocity;
        private ControlData _controlData;

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
            _buttonsPressed = new PlayerButtonPressed();
            _controlData = new ControlData(controlType);
            _entityStats = new EntityStats(MaxHealth, Damage, Speed);
            
        }

        void Update()
        {
            GatherInput();
            _time += Time.deltaTime;
           // Debug.DrawRay(transform.position, Vector3.down * 0.35f, Color.red);
            Move();
        }
        private void FixedUpdate()
        {
            CheckCollisions();

            HandleJump();
            HandleDirection();
            HandleGravity();

            ApplyMovement();
        }

        private void OnValidate()
        {
            if(_constants == null)
            {
                Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
            }
        }


        //Movement Variables
        private float Horizontal;
        private bool IsGrounded;



        //Jump Variables
        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = _buttonsPressed.IsJumpButtonDown(_controlData.JumpControl),
                JumpHeld = Input.GetButton(_controlData.JumpControl),
                Move = new Vector2(Input.GetAxisRaw(_controlData.HorizontalControl), Input.GetAxisRaw(_controlData.VerticalControl))
            };

            if (_constants.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _constants.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _constants.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }

        private void Move()
        {
            bool horizontalButtonDown = _buttonsPressed.isHorizontalButtonDown(_controlData.HorizontalControl);
            Horizontal = _buttonsPressed.HorizontalMovement(horizontalButtonDown, _controlData.HorizontalControl);
            Debug.Log("Vx: " + _rigidBody.velocity.x + " Vy: " + _rigidBody.velocity.y);

        }

        //Collisions

        private float _frameLeftGrounded = float.MinValue;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            //Ground and ceiling
            bool groudHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction,
                0, Vector2.down, _constants.GrounderDistance, ~_constants.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction,
                               0, Vector2.up, _constants.GrounderDistance, ~_constants.PlayerLayer);

            //On hit ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            //Landed on the ground 
            if (!IsGrounded && groudHit)
            {
                IsGrounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            else if (IsGrounded && !groudHit)
            {
                IsGrounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }


        //Jumping 
        private bool HasBufferredJump() => _bufferedJumpUsable && _time < _timeJumpWasPressed + _constants.JumpBuffer;
        private bool CanUseCoyoteJump() => _coyoteUsable && !IsGrounded && _time < _frameLeftGrounded + _constants.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !IsGrounded && !_frameInput.JumpHeld && _rigidBody.velocity.y > 0) _endedJumpEarly = true;
            if (!_jumpToConsume && !HasBufferredJump()) return;
            if (IsGrounded || CanUseCoyoteJump()) ExecuteJump();
            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _constants.JumpPower;
            Jumped?.Invoke();
        }


        //Returns _frameVelocity.x
        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = IsGrounded ? _constants.GroundDeceleration : _constants.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _constants.MaxSpeed,
                    _constants.Acceleration * Time.fixedDeltaTime);
            }
        }

        //returns _frameVelocity.y 
        private void HandleGravity()
        {
            if (IsGrounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _constants.GroundingForce;
            }
            else
            {
                var inAirGravity = _constants.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _constants.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_constants.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        private void ApplyMovement() => _rigidBody.velocity = _frameVelocity;


    }
}