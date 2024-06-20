using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string checkpointID; // ID hoặc tên của checkpoint

    // Biến tĩnh để lưu trữ vị trí hồi sinh hiện tại và ID của checkpoint
    public static Vector2 respawnPosition;
    public static string currentCheckpointID;

    // Khởi tạo vị trí hồi sinh mặc định
    void Start()
    {
        // Nếu chưa có vị trí hồi sinh, đặt vị trí hồi sinh mặc định là vị trí của checkpoint đầu tiên
        if (respawnPosition == Vector2.zero)
        {
            respawnPosition = transform.position;
            currentCheckpointID = checkpointID;
        }
    }

    // Phương thức này được gọi khi vật thể khác va chạm với CheckPoint (sử dụng Collider2D)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu vật thể va chạm là Player
        {
            respawnPosition = transform.position; // Cập nhật vị trí hồi sinh mới
            currentCheckpointID = checkpointID; // Cập nhật ID của checkpoint hiện tại
        }
    }
}
