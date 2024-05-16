using System.Collections;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Thời gian giữa các chớp nháy
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // Đổi màu văn bản thành màu trong suốt (alpha = 0)
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
            yield return new WaitForSeconds(blinkInterval);

            // Đổi màu văn bản thành màu đầy đủ (alpha = 1)
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 1f);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
