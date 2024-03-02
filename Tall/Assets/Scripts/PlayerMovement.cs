using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static void SetMovementStatus(bool status) => canMove = status;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float accelerationInSeconds = .5f;
    [SerializeField] private float speed = 1.0f;
    private Vector2 targetVelocity;
    private static bool canMove = true;

    private void Update()
    {
        BoundsCheck(LevelGeneration.Bounds);
        Vector2 direction = new Vector2();
        if (canMove)
        {
            float inputH = Input.GetAxisRaw("Horizontal");
            float inputV = Input.GetAxisRaw("Vertical");
            direction.x = inputH;
        }
        else
        {
            direction = Vector2.zero;
        }


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
    }

    private void FixedUpdate()
    {
        rb.velocity = targetVelocity * speed;
    }

    private Vector2 rfv0;
}
