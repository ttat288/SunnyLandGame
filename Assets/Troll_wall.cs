using UnityEngine;

public class Troll_wall : MonoBehaviour
{
    public float moveDistance = 1f; // Khoảng cách di chuyển
    public float moveSpeed = 1f; // Tốc độ di chuyển

    private Vector3 originalPosition;
    private bool isMoving = false;
    private bool hasMoved = false; // Biến để theo dõi đã di chuyển lần đầu tiên hay chưa

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isMoving && !hasMoved) // Chỉ di chuyển nếu chưa di chuyển lần nào trước đó
        {
            // Di chuyển object xuống dưới mỗi frame
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            // Kiểm tra nếu đã di chuyển đủ khoảng cách thì dừng
            if (transform.position.y <= originalPosition.y - moveDistance)
            {
                isMoving = false;
                hasMoved = true; // Đánh dấu đã di chuyển lần đầu tiên
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Xử lý khi object chạm vào trigger
        if (collision.CompareTag("Player") && !hasMoved) // Thêm điều kiện !hasMoved để chỉ di chuyển lần đầu tiên
        {
            isMoving = true;
        }
    }
}
