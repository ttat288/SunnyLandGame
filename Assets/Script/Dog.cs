using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float delayTime;
    private float delayStart;
    private Collider2D coll;
    private Animator anim;
    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
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
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
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
            if (transform.position.x < rightCap)
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
