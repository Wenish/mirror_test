using UnityEngine;
using Mirror;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 previousMovement;
    private float previousAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        HandleInput();
    }

    [Client]
    void HandleInput()
    {
        // Capture input for movement and rotation
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)(mousePosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Check if movement input has changed
        if (movement != previousMovement)
        {
            Debug.Log("handle input move");
            // Send movement input to server
            CmdMove(movement);

            // Update previous movement value
            previousMovement = movement;
        }

        // Check if rotation input has changed
        if (Mathf.Abs(angle - previousAngle) > Mathf.Epsilon)
        {
            // Send rotation input to server
            CmdRotate(angle);

            // Update previous angle value
            previousAngle = angle;
        }
    }

    [Command]
    void CmdMove(Vector2 movement)
    {
        Debug.Log("movment");
        // Apply movement on the server
        Move(movement);
    }

    [Command]
    void CmdRotate(float angle)
    {
        // Apply rotation on the server
        Rotate(angle);
    }

    [Server]
    void Move(Vector2 movement)
    {
        rb.velocity = movement.normalized * moveSpeed;
    }

    [Server]
    void Rotate(float angle)
    {
        rb.rotation = angle - 90f; // Adjust based on the initial orientation of your sprite
    }
}