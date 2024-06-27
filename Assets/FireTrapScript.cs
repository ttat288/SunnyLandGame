using System.Collections;
using UnityEngine;

public class FireTrapScript : MonoBehaviour
{
    public float damage;
    public float triggerInterval;
    private bool isTriggered = false;
    private ParticleSystem fireParticle;
    [SerializeField] private AudioSource audio;

    void Start()
    {
        fireParticle = GetComponent<ParticleSystem>();
        if (fireParticle == null)
        {
            Debug.LogError("No ParticleSystem component found on the GameObject.");
            return;
        }

        // Khởi động cơ chế phun lửa theo chu kỳ
        InvokeRepeating("ToggleFire", 0f, triggerInterval);
    }

    void ToggleFire()
    {
        isTriggered = !isTriggered;

        var emission = fireParticle.emission;
        emission.enabled = isTriggered; // Bật hoặc tắt hiệu ứng lửa
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered && other.CompareTag("Player"))
        {
            // Gây sát thương cho nhân vật
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                audio.Play();
            }
        }
    }
}
