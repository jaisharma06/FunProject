using FunProject.Characters.Common;
using FunProject.Characters.Liam.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FunProject.Characters.Liam
{
    public class LiamController : MonoBehaviour, ICharacter
    {
        public PlayerData _data;

        #region CHECKERS
        [SerializeField]
        private Transform _groundChecker;
        #endregion

        #region PRIVATE_FIELDS
        public Rigidbody rb { get; private set; }
        public StateMachine stateMachine { get; private set; }
        public LiamInputHandler _inputHandler { get; private set; }
        public bool isJumping { get; set; }
        #endregion

        public float speed { get; set; }
        public bool isGrounded { get => CheckIfGrounded(); }
        public Animator _animator { get; set; }

        #region UNITY_CALLBACKS


        private void OnEnable()
        {
            if (!_inputHandler) {
                _inputHandler = GetComponent<LiamInputHandler>();
            }
            _inputHandler.onJumpButtonPress.AddListener(Jump);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _inputHandler = GetComponent<LiamInputHandler>();
            

            InitializeStateMachineBehaviour();
        }

        private void OnDisable()
        {
            _inputHandler.onJumpButtonPress.RemoveAllListeners();
        }

        #endregion

        #region STATE_MACHINE
        private void InitializeStateMachineBehaviour()
        {
            stateMachine = GetComponent<StateMachine>();
            var states = new Dictionary<Type, State>()
            {
                { typeof(Idle_State), new Idle_State(this, "locomotion")},
                { typeof(Run_State), new Run_State(this, "locomotion")},
                { typeof(Walk_State), new Walk_State(this, "locomotion")},
                { typeof(Jump_State), new Jump_State(this, "isJumping")},
                { typeof(InAir_State), new InAir_State(this, "isInAir")},
                { typeof(Land_State), new Land_State(this, "isLanding")}
            };

            stateMachine.SetStates(states);
        }
        #endregion

        #region CHARACTER_METHODS
        public void LookInMovementDirection()
        {
            var rotation = Quaternion.LookRotation(new Vector3(_inputHandler.RawMovementInput.x, 0, _inputHandler.RawMovementInput.y));
            transform.rotation = rotation;
        }

        public void UpdateSpeed()
        {

            Vector3 direction = new Vector3(_inputHandler.RawMovementInput.x, rb.velocity.y, _inputHandler.RawMovementInput.y);
            var forward = _inputHandler.camTransform.forward;
            var right = _inputHandler.camTransform.right;


            direction = direction.x * right.normalized + direction.z * forward.normalized;

            Vector3 velocity;
            if (_inputHandler.isRunning) {
                velocity = direction * _data.runSpeed;

            } else {
                velocity = direction * _data.walkSpeed;
            }
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
            speed = rb.velocity.magnitude;

            Quaternion rotation = Quaternion.Euler(0, _inputHandler.camTransform.eulerAngles.y, 0);
            transform.rotation = rotation;

        }

        public void Jump()
        {
            if(!isJumping && isGrounded) {
                isJumping = true;
            }
        }

        public void UpdateAxisData()
        {
            var xAxis = _inputHandler.RawMovementInput.x;
            var yAxis = _inputHandler.RawMovementInput.y;

            if (_inputHandler.isRunning) {
                xAxis = Mathf.Clamp(_inputHandler.RawMovementInput.x, -1f, 1f);
                yAxis = Mathf.Clamp(_inputHandler.RawMovementInput.y, -1f, 1f);
            } else {
                xAxis = Mathf.Clamp(_inputHandler.RawMovementInput.x, -0.5f, 0.5f);
                yAxis = Mathf.Clamp(_inputHandler.RawMovementInput.y, -0.5f, 0.5f);
            }

            _animator.SetFloat("xAxis", xAxis);
            _animator.SetFloat("yAxis", yAxis);
        }

        public bool CheckIfGrounded()
        {
            return Physics.CheckSphere(_groundChecker.position, _data.groundCheckRadius, _data.groundLayer);
        }
        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundChecker.position, _data.groundCheckRadius);
        }
    }
}