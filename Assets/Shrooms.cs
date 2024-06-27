using System.Collections;
using UnityEngine;

public class MushroomExplosion : MonoBehaviour
{
    public AudioClip explosionSound; // Âm thanh của vụ nổ (tùy chọn)
    private AudioSource audioSource;
    private Animator animator;
    private bool hasExploded = false; // Để đảm bảo nấm chỉ nổ một lần

    void Start()
    {
        // Kiểm tra và thêm AudioSource nếu không có
        audioSource = gameObject.AddComponent<AudioSource>();
        // Lấy Animator từ đối tượng
        animator = GetComponent<Animator>();
    }

    // Phát hiện va chạm
    //void OnTriggerEnter2D(Collision2D collision)
    //{
    //    // Kiểm tra va chạm với đối tượng khác (có thể là nhân vật hoặc đạn)
        
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasExploded && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet"))
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true; // Đánh dấu đã nổ

        // Kích hoạt hiệu ứng nổ
        animator.SetTrigger("Explode");

        // Phát âm thanh nổ nếu có
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        // Phá hủy đối tượng nấm sau khi animation kết thúc
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Chờ cho đến khi animation nổ kết thúc
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
