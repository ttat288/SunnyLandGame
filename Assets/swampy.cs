using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swampy : MonoBehaviour
{
    private float reducedSpeed = 2f; // Tốc độ giảm xuống
    private float defaultSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                defaultSpeed = player.speed;
                // Giảm tốc độ di chuyển của người chơi
                player.speed = reducedSpeed;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // Khôi phục lại tốc độ di chuyển ban đầu khi rời khỏi vùng trigger
                player.speed = defaultSpeed; // Chỉnh lại tên biến tốc độ mặc định của người chơi trong PlayerController
            }
        }
    }
}
