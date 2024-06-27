using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFaceBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFall = false;
    public Transform restorePoint;
    public float dropSpeed = 12f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = dropSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isFall)
        {
            rb.isKinematic = false;
           
            isFall = true;
            Invoke("Restore", 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //checkpoint
        }
    }

    //private void Restore()
    //{
    //    rb.isKinematic = true;
    //    rb.velocity = Vector2.zero;
    //    rb.angularDrag = 0;
    //    transform.position = restorePoint.position;
    //    isFall = false;
    //}
}
