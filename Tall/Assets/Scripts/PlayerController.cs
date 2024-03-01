using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private float moveHorizontal;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        Flip();
    }

    // Updating with physics Engine
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && moveHorizontal < 0f || !isFacingRight && moveHorizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
