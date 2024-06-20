using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region variable

    private PhysicsMaterial2D originalMaterial;
    public Rigidbody2D rb;
    public Animator anim;
    private Collider2D col;
    private bool shouldRestoreMaterial = false;
    private bool onLadder;
    private enum State { idle, running, jumping, falling, hurt, climb, idleclimb}
    private State state = State.idle;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float climbspeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource jumpSound;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        originalMaterial = rb.sharedMaterial;
    }
    private void Update()
    {
        if (onLadder)
        {
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, climbspeed);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, -climbspeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        // Kiểm tra nếu cần khôi phục lại trường Material và hàm OnCollisionEnter2D đã kết thúc
        if (shouldRestoreMaterial && state != State.hurt)
        {
            RestoreOriginalMaterial();
            shouldRestoreMaterial = false; // Đặt lại biến trạng thái
        }

        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherryText.text = cherries.ToString();
        }
        if (collision.gameObject.tag == "ladder")
        {
            onLadder = true;
            rb.gravityScale = 0; // Tắt trọng lực khi leo thang
        }
    }
    private void RestoreOriginalMaterial()
    {
        // Khôi phục lại giá trị ban đầu của trường Material
        rb.sharedMaterial = originalMaterial;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.sharedMaterial = null;
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.sharedMaterial = null;
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }

                // Thiết lập biến để báo hiệu rằng cần khôi phục lại trường Material
                shouldRestoreMaterial = true;
            }
        }

        if (other.gameObject.tag == "HiddenItem")
        {
            TilemapRenderer tilemapRenderer = other.gameObject.GetComponent<TilemapRenderer>();
            if (tilemapRenderer != null)
            {
                tilemapRenderer.enabled = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ladder")
        {
            onLadder = false;
            rb.gravityScale = 5; // Bật lại trọng lực khi rời khỏi thang
        }
    }
    private void Movement()
    {
        float hDirection = Input.GetAxisRaw("Horizontal");
        if (!IsTouchingWall())
        {
            if (hDirection < 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
            else if (hDirection > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y); // Dừng vận tốc khi không có input
            }

            // Kiểm tra nếu người chơi đang giữ phím Space và nhân vật đang chạm vào mặt đất
            if (Input.GetKey(KeyCode.Space) && col.IsTouchingLayers(ground) && state != State.jumping)
            {
                Jump();
            }
        }
    }


    private bool IsTouchingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.1f, ground);
        return hit.collider != null;
    }

    private void Jump()
    {
        jumpSound.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (col.IsTouchingLayers(ground))
            {
                state = State.idle;
                footstep.Play();
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
        if (state == State.running || state == State.idle)
        {
            if (!col.IsTouchingLayers(ground) && !onLadder)
            {
                state = State.falling;
                if (col.IsTouchingLayers(ground))
                {
                    footstep.Play();
                }
            }
        }
        if (onLadder)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                state = State.climb;
            }
            else
            {
                state = State.idleclimb;
            }
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }
    public void Respawn()
    {
        transform.position = CheckPoint.respawnPosition;
        rb.velocity = Vector2.zero;
        state = State.idle;
        shouldRestoreMaterial = false;
    }
    public float PlayerLocaltion()
    {
        return transform.position.x;
    }
}