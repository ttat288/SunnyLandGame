using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Enemy
{
    [SerializeField] private float moveDistance = 2f; // Khoảng cách di chuyển
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float delayTime;
    private float delayStart;
    private Collider2D coll;
    private Animator anim;
    private bool facingLeft = true;
    private Vector2 startPos; // Vị trí bắt đầu

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        startPos = transform.position; // Lưu vị trí bắt đầu
    }

    private void Update()
    {
        if (coll.IsTouchingLayers(ground))
        {
            Move();
        }
    }

    private void Move()
    {
        // Tính toán vị trí mới dựa trên khoảng cách di chuyển
        Vector2 targetPos = facingLeft ? startPos + Vector2.left * moveDistance : startPos + Vector2.right * moveDistance;

        if (facingLeft)
        {
            if (transform.position.x > targetPos.x)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
                transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Run", false);
                anim.SetBool("Idle", true);
                anim.Update(0);  // Force animator update
                if (Time.time - delayStart > delayTime)
                {
                    facingLeft = false;
                    delayStart = Time.time;
                }
            }
        }
        else
        {
            if (transform.position.x < targetPos.x)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
                transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Run", false);
                anim.SetBool("Idle", true);
                anim.Update(0);  // Force animator update
                if (Time.time - delayStart > delayTime)
                {
                    facingLeft = true;
                    delayStart = Time.time;
                }
            }
        }
    }
}
