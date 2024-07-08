using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFall = false;
    public float moveSpeed = 6f; // Tốc độ di chuyển ngang
    public float moveDuration = 2f; // Thời gian di chuyển lên trên
    public float horizontalMoveDuration = 2f; // Thời gian di chuyển sang trái
    private Vector3 startPos;
    private Vector3 endPosUp;
    private Vector3 endPosLeft;
    private float moveTimer = 0f;
    private bool moveUpCompleted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Vô hiệu hóa gravity

        startPos = transform.position;
        endPosUp = startPos + Vector3.up * 16f; // Điểm kết thúc khi di chuyển lên trên
        endPosLeft = endPosUp + Vector3.left * 100f; // Điểm kết thúc khi di chuyển sang trái
    }

    void Update()
    {
        if (isFall)
        {
            moveTimer += Time.deltaTime;
            if (!moveUpCompleted)
            {
                float t = moveTimer / moveDuration;
                transform.position = Vector3.Lerp(startPos, endPosUp, t);
                if (t >= 1f)
                {
                    moveUpCompleted = true;
                    moveTimer = 0f; // Reset moveTimer để bắt đầu di chuyển sang trái
                }
            }
            else
            {
                float t = moveTimer / horizontalMoveDuration;
                transform.position = Vector3.Lerp(endPosUp, endPosLeft, t);
                // Bạn có thể thêm logic nếu cần làm gì đó khi hoàn thành di chuyển sang trái
                if (t >= 1f)
                {
                    Destroy(gameObject); // Tự hủy đối tượng sau khi hoàn thành di chuyển
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFall)
        {
            isFall = true;
        }
    }
}
