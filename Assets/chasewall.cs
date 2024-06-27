using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasewall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFall = false;
    public Transform restorePoint;
    public float moveSpeed = 6f; // Tốc độ di chuyển ngang
    public float distance = 8f; // Khoảng cách di chuyển ngang
    public float moveDuration = 2f; // Thời gian di chuyển
    private Vector3 initialPosition; // Vị trí ban đầu của đối tượng
    private Vector3 endPos;
    private float moveTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Vô hiệu hóa gravity

        initialPosition = transform.position; // Lưu lại vị trí ban đầu
        endPos = initialPosition + Vector3.right * distance; // Điểm kết thúc, điều chỉnh theo cần thiết
    }

    void Update()
    {
        if (isFall)
        {
            moveTimer += Time.deltaTime;
            float t = moveTimer / moveDuration;
            transform.position = Vector3.Lerp(initialPosition, endPos, t);

            if (t >= 1f)
            {
                Restore();
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

    private void Restore()
    {
        transform.position = initialPosition; // Khôi phục về vị trí ban đầu
        isFall = false;
        moveTimer = 0f; // Đặt lại thời gian di chuyển
    }
}
