using UnityEngine;
using UnityEngine.InputSystem;

namespace Korakit
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody Rb;
        private Vector2 moveInput;

        [SerializeField]
        private Animator animator;
        private bool isRunning;
        private bool isIdle;
        private bool isWalking;

        [SerializeField]
        private float NormalSpeed = 5;

        [SerializeField]
        private float RunSpeed = 10;
        private float currentSpeed;

        [SerializeField]
        private float JumpForce = 5f;

        private bool isGrounded;

        [SerializeField]
        float RotationSpeed = 1;

        [SerializeField]
        private GameObject HurtUI;

        [SerializeField]
        private float gameOverDelay = 0.2f;
        private float gameOverTimer;

        private Camera MainCamera;
        public void Awake()
        {
            MainCamera = Camera.main;

            Rb = GetComponent<Rigidbody>();
            currentSpeed = NormalSpeed;
            gameOverTimer = gameOverDelay;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
            if (moveInput != Vector2.zero)
            {
                isWalking = true;
                isIdle = false;
            }
            else
            {
                isWalking = false;
                isIdle = true;
            }

        }
        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                currentSpeed = RunSpeed;
                isRunning = true;
            }
            else if (context.canceled)
            {
                currentSpeed = NormalSpeed;
                isRunning = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && isGrounded)
            {
                Rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
        public void Update()
        {
            Vector3 cameraForward = MainCamera.transform.forward;
            Vector3 cameraRight = MainCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * moveInput.y + cameraRight * moveInput.x;

            Rb.linearVelocity = new Vector3(moveDirection.x * currentSpeed, Rb.linearVelocity.y, moveDirection.z * currentSpeed);

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                Rb.MoveRotation(
                    Quaternion.Slerp( Rb.rotation, targetRotation, RotationSpeed * Time.fixedDeltaTime )
                );
            }
        }
    }
}