using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public GameObject eye;
    bool status = true;
    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        // Tìm và lưu trữ danh sách các đối tượng Enemy
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            enemies.Add(enemy);
        }
    }

    // Coroutine để xử lý logic khi Player va chạm với Eye
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && status)
        {
            eye.GetComponent<SpriteRenderer>().enabled = false;

            // Hiển thị tất cả các đối tượng Enemy
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null && enemy.activeSelf) // Kiểm tra xem enemy có tồn tại và đang hoạt động không
                {
                    SpriteRenderer enemyRenderer = enemy.GetComponent<SpriteRenderer>();
                    if (enemyRenderer != null)
                    {
                        enemyRenderer.enabled = true;
                    }
                }
            }

            status = false;
            yield return new WaitForSeconds(15f);

            // Sau 15 giây, ẩn lại Eye và các đối tượng Enemy
            eye.GetComponent<SpriteRenderer>().enabled = true;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null && enemy.activeSelf) // Kiểm tra xem enemy có tồn tại và đang hoạt động không
                {
                    SpriteRenderer enemyRenderer = enemy.GetComponent<SpriteRenderer>();
                    if (enemyRenderer != null)
                    {
                        enemyRenderer.enabled = false;
                    }
                }
            }

            status = true;
        }
    }
}
