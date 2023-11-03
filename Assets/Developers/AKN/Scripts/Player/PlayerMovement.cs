using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float runSpeedMultiplier = 1.5f;
    private Rigidbody2D rb;

    private Vector2 movement;
    private bool isRunning;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = PlayerController.Instance.PlayerInput.GetMovementInput();
        isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? moveSpeed * runSpeedMultiplier : moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * currentSpeed;
    }

    public void ReduceMovementSpeed(float multiplier)
    {
        currentSpeed -= currentSpeed * multiplier;
        Debug.Log(currentSpeed);
    }
}