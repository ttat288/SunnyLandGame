using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource death;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isAlive = true;
    private Collider2D col;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        death = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void JumpedOn()
    {
        anim.SetTrigger("Death");
        death.Play();
        rb.velocity = Vector3.zero;
        isAlive = false;
    }

    private void Death()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false); // Hide the enemy instead of destroying it
        col.enabled = false;
    }
    public void ResetEnemy()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        anim.ResetTrigger("Death");
        anim.Play("Idle");
        rb.velocity = Vector3.zero;
        isAlive = true;
        col.enabled = true;
        gameObject.SetActive(true);
    }
}
