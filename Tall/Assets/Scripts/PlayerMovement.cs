using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static void SetMovementStatus(bool status) => canMove = status;
    public static void SetVerticalStatus(bool status) => canVertical = status;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float accelerationInSeconds = .5f;
    [SerializeField] private float speed = 1.0f;
    private Vector2 targetVelocity;
    private static bool canMove = true;
    private static bool canVertical = false;

    private void Update()
    {
        BoundsCheck(LevelGeneration.Bounds);
        Vector2 direction = new Vector2();
        float inputH = canMove? Input.GetAxisRaw("Horizontal") : 0;
        float inputV = canVertical ? Input.GetAxisRaw("Vertical") : 0;
        direction.x = inputH;
        direction.y = inputV;


        targetVelocity = Vector2.SmoothDamp(targetVelocity, direction, ref rfv0, accelerationInSeconds);
    }

    public void BoundsCheck(Vector2 bounds)
    {
        if(rb.position.x < -bounds.x)
        {
            Vector2 newPosition = rb.position;
            newPosition.x += bounds.x * 2;
            rb.MovePosition(newPosition);
        } else if (rb.position.x > bounds.x)
        {
            Vector2 newPosition = rb.position;
            newPosition.x -= bounds.x * 2;
            rb.MovePosition(newPosition);
        }

        if (rb.position.y < -bounds.y)
        {
            Vector2 newPosition = rb.position;
            newPosition.y += bounds.y * 2;
            rb.MovePosition(newPosition);
        }
        else if (rb.position.y > bounds.y)
        {
            Vector2 newPosition = rb.position;
            newPosition.y -= bounds.y * 2;
            rb.MovePosition(newPosition);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = targetVelocity * speed;
    }

    private Vector2 rfv0;
}
