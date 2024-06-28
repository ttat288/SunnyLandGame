using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxenemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFall = false;
    //public Transform restorePoint;
    public float moveSpeed = 6f; // Tốc độ di chuyển ngang
    public float moveDuration = 2f; // Thời gian di chuyển
    private Vector3 startPos;
    private Vector3 endPos;
    private float moveTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Vô hiệu hóa gravity

        startPos = transform.position;
        endPos = startPos + Vector3.right * 8f; // Điểm kết thúc, điều chỉnh theo cần thiết
    }

    void Update()
    {
        if (isFall)
        {
            moveTimer += Time.deltaTime;
            float t = moveTimer / moveDuration;
            transform.position = Vector3.Lerp(startPos, endPos, t);

            //if (t >= 1f)
            //{
            //    //Restore();
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFall)
        {
            isFall = true;
        }
    }

    //private void Restore()
    //{
    //    transform.position = restorePoint.position;
    //    isFall = false;
    //}
}
