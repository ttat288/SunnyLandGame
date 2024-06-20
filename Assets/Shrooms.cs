using UnityEngine;

public class MushroomExplosion : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab của hiệu ứng nổ
    public AudioClip explosionSound; // Âm thanh của vụ nổ (tùy chọn)
    private AudioSource audioSource;

    void Start()
    {
        // Kiểm tra và thêm AudioSource nếu không có
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Phát hiện va chạm
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với đối tượng khác (có thể là nhân vật hoặc đạn)
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    void Explode()
    {
        // Hiển thị hiệu ứng nổ
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

        // Phát âm thanh nổ nếu có
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        // Phá hủy đối tượng nấm
        Destroy(gameObject);

        // Phá hủy hiệu ứng nổ sau khi animation kết thúc
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        float explosionDuration = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosion, explosionDuration);
    }
}
