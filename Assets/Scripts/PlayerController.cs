using UnityEngine;
using Mirror;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)(mousePosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Send input to server
        CmdMoveAndRotate(movement, angle);
    }

    [Command]
    void CmdMoveAndRotate(Vector2 movement, float angle)
    {
        // Apply movement on the server
        MoveAndRotate(movement, angle);
    }

    [Server]
    void MoveAndRotate(Vector2 movement, float angle)
    {
        rb.velocity = movement.normalized * moveSpeed;
        rb.rotation = angle - 90f; // Adjust based on the initial orientation of your sprite
    }
}