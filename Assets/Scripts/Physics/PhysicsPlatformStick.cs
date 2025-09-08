using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Makes objects stick to moving platforms while preserving their ability to move independently.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// Physics - Makes objects stick to moving platforms using physics forces
/// For educational use in Animation and Interactivity class.
/// </summary>
public class PhysicsPlatformStick : MonoBehaviour
{
    [Header("Platform Detection")]
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private string platformTag = "movingPlatform";

    [Header("Movement Settings")]
    [SerializeField] private float platformForce = 2f;

    private Rigidbody rb;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;
    private Vector3 platformVelocity;
    private bool isOnPlatform;

    private void OnEnable()
    {
        #if UNITY_EDITOR
        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;
        #endif
    }

    private void OnDisable()
    {
        #if UNITY_EDITOR
        SceneView.duringSceneGui -= OnSceneGUI;
        #endif
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CheckForPlatform();
        
        if (isOnPlatform && currentPlatform != null)
        {
            // Calculate platform velocity
            platformVelocity = (currentPlatform.position - lastPlatformPosition) / Time.fixedDeltaTime;
            
            // Apply platform movement as a force
            Vector3 targetVelocity = platformVelocity;
            Vector3 velocityDiff = targetVelocity - rb.linearVelocity;
            rb.AddForce(velocityDiff * platformForce, ForceMode.Acceleration);

            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void CheckForPlatform()
    {
        RaycastHit hit;
        bool foundPlatform = Physics.Raycast(
            transform.position,
            Vector3.down,
            out hit,
            groundCheckDistance,
            platformLayer
        );

        if (foundPlatform && hit.collider.CompareTag(platformTag))
        {
            if (currentPlatform != hit.transform)
            {
                // Just stepped onto platform
                currentPlatform = hit.transform;
                lastPlatformPosition = currentPlatform.position;
            }
            isOnPlatform = true;
        }
        else
        {
            isOnPlatform = false;
            currentPlatform = null;
        }
    }

    #if UNITY_EDITOR
    private void OnSceneGUI(SceneView sceneView)
    {
        DrawGizmos();
    }

    private void DrawGizmos()
    {
        // Draw the main raycast
        Handles.color = isOnPlatform ? Color.green : Color.yellow;
        Vector3 rayStart = transform.position;
        Vector3 rayEnd = rayStart + Vector3.down * groundCheckDistance;
        
        // Draw the main line
        Handles.DrawDottedLine(rayStart, rayEnd, 2f);

        // Draw sphere at check point
        float sphereSize = 0.1f;
        Handles.SphereHandleCap(
            0,
            rayEnd,
            Quaternion.identity,
            sphereSize * 2f,
            EventType.Repaint
        );

        // Draw detection area
        Handles.color = new Color(1f, 1f, 0f, 0.2f);
        Handles.DrawSolidDisc(rayEnd, Vector3.up, sphereSize);

        // Add labels
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 12;
        style.fontStyle = FontStyle.Bold;
        
        string statusText = isOnPlatform ? "On Platform" : "No Platform";
        Handles.Label(rayStart + Vector3.up * 0.1f, statusText, style);
    }
    #endif

    /// <summary>
    /// Returns true if the object is currently on a moving platform.
    /// </summary>
    public bool IsOnPlatform => isOnPlatform;

    /// <summary>
    /// Returns the transform of the current platform, or null if not on a platform.
    /// </summary>
    public Transform CurrentPlatform => currentPlatform;
}