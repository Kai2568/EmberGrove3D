using System.Diagnostics.Contracts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;

    [Header("References")]
    [SerializeField] private Transform visual;

    private Rigidbody dragonRigidbody;
    private Vector3 movementInput;
    private Vector3 facingDirection = Vector3.back;

    public bool IsMoving { get; private set; }

    public Vector3 FacingDirection
    {
        get
        {
            return facingDirection;
        }
    }

    private void Awake()
    {
        dragonRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadMovementInput();
        UpdateFacingDirection();
        FlipVisual();
    }

    private void FixedUpdate()
    {
        MoveDragon();
    }

    private void ReadMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(
            horizontalInput,
            0f,
            verticalInput
            );

        if (movementInput.magnitude > 1f)
        {
            movementInput.Normalize();
        }

        IsMoving = movementInput.sqrMagnitude > 0.01f;
    }

    private void MoveDragon()
    {
        Vector3 movementAmount = movementInput * moveSpeed * Time.fixedDeltaTime;

        Vector3 targetPosition = dragonRigidbody.position + movementAmount;

        dragonRigidbody.MovePosition(targetPosition);
    }

    private void UpdateFacingDirection()
    {
        if (!IsMoving)
        {
            return;
        }

        facingDirection = movementInput;
    }

    private void FlipVisual()
    {
        if (visual == null)
        {
            return;
        }

        Vector3 visualScale = visual.localScale;

        if (facingDirection.x < -0.01f)
        {
            visualScale.x = -Mathf.Abs(visualScale.x);
        }
        else if (facingDirection.x > 0.01f)
        {
            visualScale.x = Mathf.Abs(visualScale.x);
        }

        visual.localScale = visualScale;
    }
}
