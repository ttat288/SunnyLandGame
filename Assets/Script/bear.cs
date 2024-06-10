using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bear : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float speed = 10f;
    private Collider2D coll;

    private bool facingLeft = true;
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (facingLeft)
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
