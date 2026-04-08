using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        input = Vector2.zero;

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Arrow keys
        if (keyboard.leftArrowKey.isPressed)
            input.x = -1;
        if (keyboard.rightArrowKey.isPressed)
            input.x = 1;
        if (keyboard.upArrowKey.isPressed)
            input.y = 1;
        if (keyboard.downArrowKey.isPressed)
            input.y = -1;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(input.x, 0f, input.y);

        rb.linearVelocity = movement * speed;
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            Quaternion smoothRotation = Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

            rb.MoveRotation(smoothRotation);
        }
    }
}