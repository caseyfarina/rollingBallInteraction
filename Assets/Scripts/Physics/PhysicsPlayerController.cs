using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// Physics - Ball-based player controller with camera-relative movement
/// For educational use in Animation and Interactivity class.
/// Uses Unity Input System for modern input handling.
/// </summary>
public class PhysicsPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxVelocity = 20f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool jumpRequested;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

    }

    public void Update()
    {
        
    }



    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
 
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
        HandleJump();
    }

    private void CheckGrounded()
    {
        // Check if there's ground below us using a sphere cast
        isGrounded = Physics.CheckSphere(
            transform.position - new Vector3(0f, 0.1f, 0f),
            groundCheckRadius,
            groundLayer
        );
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (inputDirection != Vector3.zero)
        {
            // Convert input direction to be relative to camera
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;

            // Project camera's forward and right vectors onto the horizontal plane
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate the final movement direction
            Vector3 moveDirection = (cameraRight * inputDirection.x + cameraForward * inputDirection.z);

            // Apply force only if below max velocity
            if (rb.linearVelocity.magnitude < maxVelocity)
            {
                rb.AddForce(moveDirection * moveForce, ForceMode.Force);
            }
        }
    }

    private void HandleJump()
    {
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    // Optional: Visualize the ground check radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            transform.position - new Vector3(0f, 0.1f, 0f),
            groundCheckRadius
        );
    }
}
