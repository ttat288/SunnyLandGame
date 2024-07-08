using UnityEngine;

public class final_wall : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Tốc độ di chuyển của bức tường
    public float moveDistance = 10.0f; // Khoảng cách tối đa bức tường có thể di chuyển
    private bool isMoving = false; // Biến kiểm tra trạng thái di chuyển
    private Vector3 startPosition; // Vị trí ban đầu của bức tường
    private Vector3 targetPosition; // Vị trí đích khi di chuyển
    private bool hasMovedUp = false; // Biến kiểm tra xem bức tường đã di chuyển lên chưa

    void Start()
    {
        // Lưu lại vị trí ban đầu của bức tường
        startPosition = transform.position;
        // Tính toán vị trí đích khi di chuyển
        targetPosition = startPosition + Vector3.left * moveDistance;
    }

    void Update()
    {
        if (isMoving)
        {
            // Di chuyển bức tường đến vị trí đích
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // Kiểm tra khoảng cách di chuyển so với vị trí ban đầu
            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                // Đạt đến khoảng cách tối đa và vẫn tiếp xúc với Player, giữ nguyên vị trí
                if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
                {
                    // Giữ nguyên vị trí
                    transform.position = transform.position;
                }
                else
                {
                    // Ngừng di chuyển và đặt lại trạng thái
                    isMoving = false;
                    hasMovedUp = true; // Đánh dấu là đã di chuyển lên
                }
            }
        }
        else
        {
            // Nếu không di chuyển, quay trở lại vị trí ban đầu
            if (hasMovedUp)
            {
                // Nếu đã di chuyển lên thì không quay lại vị trí ban đầu
                transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            }
            else
            {
                // Nếu chưa di chuyển lên thì quay trở lại vị trí ban đầu
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Bắt đầu di chuyển bức tường khi đối tượng có tag "Player" chạm vào
            isMoving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // Thay "Ground" bằng tag của collider không phải trigger
        {
            // Di chuyển lên trên 1 đoạn khi chạm vào collider không phải trigger
            transform.Translate(Vector3.up * 1.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ngừng di chuyển bức tường khi đối tượng có tag "Player" rời khỏi
            isMoving = false;
        }
    }
}
