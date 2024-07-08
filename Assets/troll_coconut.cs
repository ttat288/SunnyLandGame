using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troll_coconut : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFall = false;
    private bool startDestroyTimer = false;
    private float destroyTimer = 0f;
    public float moveSpeed = 6f; // Tốc độ di chuyển ngang
    public float moveDuration = 2f; // Thời gian di chuyển
    public Vector3 moveDirection = Vector3.left; // Hướng di chuyển sang trái
    public float fallSpeed = 5f;
    private Vector3 startPos;
    private Vector3 endPos;
    private float moveTimer = 0f;
    private float rotationSpeed = -90f; // Tốc độ quay trên trục Z
    public float fallDistance = 5f; // Khoảng cách rơi trên trục Y
    public float rotationDuration;
    public float timeBeforeDestroy = 1f; // Thời gian đợi trước khi tự hủy

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Vô hiệu hóa gravity

        startPos = transform.position;
        endPos = startPos + moveDirection * 8f; // Điểm kết thúc, điều chỉnh theo hướng di chuyển
    }

    void Update()
    {
        if (isFall)
        {
            moveTimer += Time.deltaTime;
            float t = moveTimer / moveDuration;
            t = Mathf.Exp(t) - 1;

            // Đảm bảo t không vượt quá 1
            t = Mathf.Clamp01(t);

            // Cập nhật vị trí
            transform.position = Vector3.Lerp(startPos, endPos, t);

            Vector3 newPosition = Vector3.Lerp(startPos, endPos, t);
            newPosition.y = Mathf.Lerp(startPos.y, startPos.y - fallDistance, t);
            transform.position = newPosition;

            // Cập nhật rotation trên trục Z
            float rotationTimer = moveTimer / rotationDuration;
            float newZRotation = Mathf.Lerp(0, rotationSpeed, rotationTimer);
            transform.rotation = Quaternion.Euler(0, 0, newZRotation);

            // Nếu đã di chuyển xong
            if (t >= 1f)
            {
                isFall = false;
                moveTimer = 0f;
                startDestroyTimer = true; // Bắt đầu đếm thời gian để hủy
            }
        }

        if (startDestroyTimer) // Chỉ khi isFall là true mới bắt đầu đếm thời gian hủy
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= timeBeforeDestroy)
            {
                Destroy(gameObject);
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
