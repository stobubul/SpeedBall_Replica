using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI blinkingText; // Gözüken metin nesnesi
    public float blinkDuration = 1.0f; // Yanıp sönme süresi
    public float blinkInterval = 0.5f; // Yanıp sönme aralığı

    private bool isBlinking = false; // Yanıp sönme durumu
    
    // Start is called before the first frame update
    void Start()
    {
        SetTextAlpha(0f); // Metni ilk başta tamamen saydam yaparak gizle
        
        StartBlinking();
    }
    
    void StartBlinking()
    {
        // Eğer zaten yanıp sönen bir işlem başlatılmışsa, tekrar başlatmayın
        if (isBlinking)
            return;

        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        isBlinking = true;

        while (true)
        {
            FadeTextAlpha(1f);

            yield return new WaitForSeconds(blinkDuration);

            FadeTextAlpha(0f);

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void SetTextAlpha(float alpha)
    {
        Color blinkingTextColor = blinkingText.color;
        blinkingTextColor.a = alpha;
        blinkingText.color = blinkingTextColor;
    }

    void FadeTextAlpha(float targetAlpha)
    {
        SetTextAlpha(targetAlpha);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
