using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisible : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Tắt SpriteRenderer lúc đầu
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Phương thức này được gọi khi một Collider khác đi vào Trigger Collider của object này
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true; // Bật SpriteRenderer khi tiếp xúc với "Player"
            }
        }
    }

}
