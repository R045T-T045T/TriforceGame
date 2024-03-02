using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] private float horizontal;
    [SerializeField] private bool isFacingRight;

    [SerializeField] private float tallSpeed;
    private bool isSlowed = false;
    private bool isMoreSlowed = false;

    [SerializeField] float aloneSpeed;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] public bool isAlone;
    [SerializeField] private bool isTall;
    [SerializeField] public bool isMoving;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = aloneSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlowed)
        {
            isSlowed = false;
            moveSpeed = tallSpeed;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();

        if(rb.velocity.x <= 0.1f)
        {
            isMoving = true;
        }
        else if (Mathf.Approximately(rb.velocity.x, 0f))
        {
            isMoving = false;
        }
    }

    // Updating with physics Engine
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    public void ApplySlowdown()
    {
        if(isAlone == false)
        {
            isSlowed = true;
            isTall = true;
            moveSpeed = tallSpeed;
        }
    }

    public void ApplySpeed()
    {
        if(isTall == true)
        {
            isAlone = true;
            isSlowed = false;
            isTall = false;
            moveSpeed = aloneSpeed;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
