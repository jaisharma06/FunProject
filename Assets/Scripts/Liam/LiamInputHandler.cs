using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace FunProject.Characters.Liam
{
    public class LiamInputHandler : MonoBehaviour
    {
        private PlayerInput playerInput;
        public Camera cam { get; private set; }
        public Transform camTransform { get; private set; }
        public Vector2 RawMovementInput;
        public Vector2 RawDashDirectionInput { get; private set; }
        public int NormInputX { get; private set; }
        public int NormInputY { get; private set; }
        public bool isRunning { get; private set; }

        public UnityEvent onJumpButtonPress;

        private void Awake()
        {
            if(onJumpButtonPress == null) {
                onJumpButtonPress = new UnityEvent();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            cam = Camera.main;
            camTransform = cam.transform;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnWalkInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();

            NormInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormInputY = Mathf.RoundToInt(RawMovementInput.y);
        }

        public void OnRunInput(InputAction.CallbackContext context)
        {
            if (context.started) {
                isRunning = true;
            } else if (context.canceled) {
                isRunning = false;
            }
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started) {
                onJumpButtonPress?.Invoke();
            }
        }
    }
}