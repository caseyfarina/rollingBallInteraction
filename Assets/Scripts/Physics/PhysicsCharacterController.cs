using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// Physics - Rigidbody-based character controller with capsule collider and animation support
/// For educational use in Animation and Interactivity class.
/// Uses Unity Input System for modern input handling.
/// </summary>
public class PhysicsCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxVelocity = 8f;
    [SerializeField] private float airControlFactor = 0.5f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer = 1;

    [Header("Character Settings")]
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [Header("Slope Settings")]
    [SerializeField] private float maxSlopeAngle = 45f;
    [SerializeField] private float slopeCheckDistance = 1f;

    [Header("Animation")]
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Transform characterMesh;

    [Header("Events")]
    public UnityEvent onGrounded;
    public UnityEvent onJump;
    public UnityEvent onLanding;
    public UnityEvent onStartMoving;
    public UnityEvent onStopMoving;
    public UnityEvent onSteepSlope;

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool wasGrounded;
    private bool jumpRequested;
    private bool isMoving;
    private Vector3 lastMoveDirection;
    private bool isOnSteepSlope;
    private Vector3 slopeNormal = Vector3.up;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        if (capsuleCollider == null)
            capsuleCollider = GetComponent<CapsuleCollider>();

        if (characterAnimator == null && characterMesh != null)
            characterAnimator = characterMesh.GetComponentInChildren<Animator>();

        rb.freezeRotation = true;
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
        CheckSlope();
        HandleMovement();
        HandleJump();
        HandleRotation();
        UpdateAnimations();
        CheckMovementEvents();
    }

    private void CheckGrounded()
    {
        wasGrounded = isGrounded;

        float capsuleBottom = transform.position.y - (capsuleCollider.height * 0.5f) + capsuleCollider.center.y;
        Vector3 checkPosition = new Vector3(transform.position.x, capsuleBottom, transform.position.z);

        isGrounded = Physics.CheckSphere(
            checkPosition,
            capsuleCollider.radius + groundCheckDistance,
            groundLayer
        );

        if (isGrounded && !wasGrounded)
        {
            onLanding.Invoke();
        }

        if (isGrounded)
        {
            onGrounded.Invoke();
        }
    }

    private void CheckSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, slopeCheckDistance, groundLayer))
        {
            slopeNormal = hit.normal;
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            bool wasOnSteepSlope = isOnSteepSlope;
            isOnSteepSlope = slopeAngle > maxSlopeAngle;

            if (isOnSteepSlope && !wasOnSteepSlope)
            {
                onSteepSlope.Invoke();
            }
        }
        else
        {
            slopeNormal = Vector3.up;
            isOnSteepSlope = false;
        }
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (inputDirection != Vector3.zero)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraRight * inputDirection.x + cameraForward * inputDirection.z);
            lastMoveDirection = moveDirection;

            // Don't apply movement force if on steep slope
            if (!isOnSteepSlope)
            {
                float currentForce = moveForce;
                if (!isGrounded)
                {
                    currentForce *= airControlFactor;
                }

                Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                if (horizontalVelocity.magnitude < maxVelocity)
                {
                    rb.AddForce(moveDirection * currentForce, ForceMode.Force);
                }
            }
        }
    }

    private void HandleJump()
    {
        if (jumpRequested && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
            onJump.Invoke();
        }
    }

    private void HandleRotation()
    {
        if (lastMoveDirection != Vector3.zero && isGrounded)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lastMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateAnimations()
    {
        if (characterAnimator != null)
        {
            Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            float speed = horizontalVelocity.magnitude;

            characterAnimator.SetFloat("Speed", speed);
            characterAnimator.SetBool("IsGrounded", isGrounded);
            characterAnimator.SetFloat("VerticalVelocity", rb.linearVelocity.y);

            bool isWalking = speed > 0.1f && isGrounded;
            characterAnimator.SetBool("IsWalking", isWalking);
        }
    }

    private void CheckMovementEvents()
    {
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        bool currentlyMoving = horizontalVelocity.magnitude > 0.1f;

        if (currentlyMoving && !isMoving)
        {
            onStartMoving.Invoke();
        }
        else if (!currentlyMoving && isMoving)
        {
            onStopMoving.Invoke();
        }

        isMoving = currentlyMoving;
    }

    public void SetMoveForce(float newForce)
    {
        moveForce = newForce;
    }

    public void SetJumpForce(float newForce)
    {
        jumpForce = newForce;
    }

    public void SetMaxVelocity(float newMax)
    {
        maxVelocity = newMax;
    }

    public bool IsGrounded => isGrounded;
    public bool IsMoving => isMoving;
    public bool IsOnSteepSlope => isOnSteepSlope;
    public float CurrentSpeed => new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;

    private void OnDrawGizmosSelected()
    {
        if (capsuleCollider == null) return;

        // Ground check visualization
        Gizmos.color = isGrounded ? Color.green : Color.red;
        float capsuleBottom = transform.position.y - (capsuleCollider.height * 0.5f) + capsuleCollider.center.y;
        Vector3 checkPosition = new Vector3(transform.position.x, capsuleBottom, transform.position.z);
        Gizmos.DrawWireSphere(checkPosition, capsuleCollider.radius + groundCheckDistance);

        // Slope check visualization
        Gizmos.color = isOnSteepSlope ? Color.red : Color.yellow;
        Gizmos.DrawRay(transform.position, Vector3.down * slopeCheckDistance);

        // Slope normal visualization
        if (slopeNormal != Vector3.up)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, slopeNormal * 2f);
        }
    }
}