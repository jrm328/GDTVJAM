using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class IsometricPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;

    private float currentSpeed;
    private Vector2 moveInput;
    private bool isSprinting;

    private Rigidbody rb;
    private PlayerInputActions inputActions;
    private DynamicCameraZoom cameraZoom;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions();
        cameraZoom = Object.FindFirstObjectByType<DynamicCameraZoom>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Sprint.started += ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled += ctx => isSprinting = false;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        inputActions.Player.Sprint.started -= ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled -= ctx => isSprinting = false;

        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // ✅ Correct isometric direction conversion (matches 45° camera)
        Vector3 isometricDirection = new Vector3(
            moveInput.x + moveInput.y,
            0f,
            moveInput.y - moveInput.x
        ).normalized;

        Vector3 targetVelocity = isometricDirection * currentSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

        // ✅ Smoothly rotate player to face movement direction
        if (isometricDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(isometricDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // ✅ Trigger camera zoom logic
        bool isCurrentlyMoving = moveInput.sqrMagnitude > 0.01f;
        cameraZoom?.SetMoving(isCurrentlyMoving);
    }
}
