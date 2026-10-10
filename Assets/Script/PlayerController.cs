using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Korakit
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody Rb;
        private Vector2 moveInput;
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

        private Camera MainCamera;

        private InteractCollectible nearbyItem;


        public void Awake()
        {
            MainCamera = Camera.main;
            animator = GetComponentInChildren<Animator>();
            Rb = GetComponent<Rigidbody>();
            currentSpeed = NormalSpeed;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
            isWalking = moveInput.sqrMagnitude > 0.01f;
            isIdle = !isWalking;
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
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            if (nearbyItem != null)
            {
                nearbyItem.Interact();
            }
        }

        public void SetNearbyItem(InteractCollectible item)
        {
            nearbyItem = item;
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

            Animation();
        }

        public void Animation()
        {
            if (animator == null)
                return;
            animator.SetBool("isGrounded", isGrounded); 
            animator.SetBool("IsWalking", isWalking && !isRunning); 
            animator.SetBool("IsRunning", isWalking && isRunning);
        }
        
    }
}