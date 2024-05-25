using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : Enemy
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    private bool isGoingUp = true;
    private bool isGrounded = false;
    [SerializeField] private LayerMask ground; // Lớp mask để xác định mặt đất

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        isGrounded = coll.IsTouchingLayers(ground);
        if (isGrounded)
        {
            // Khi enemy tiếp xúc với mặt đất, chuyển sang trạng thái idle
            animator.SetBool("Flying", false);
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            Fly();
        }
    }

    private void Fly()
    {
        if (isGoingUp)
        {
            if (transform.position.y < maxHeight)
            {
                rb.velocity = new Vector2(0, speed);
                animator.SetBool("Flying", true);
            }
            else
            {
                isGoingUp = false;
            }
        }
        else
        {
            if (transform.position.y > minHeight)
            {
                rb.velocity = new Vector2(0, -speed);
                animator.SetBool("Flying", true);
            }
            else
            {
                isGoingUp = true;
            }
        }
    }
}
