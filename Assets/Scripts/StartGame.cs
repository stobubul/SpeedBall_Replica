using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    public GameObject pressAnyKeyToStartText;
    
    public GameObject countdownTextObject;
    public TextMeshProUGUI countdownText;
    private float countdownDuration = 3f;
    public float countdownMessageDuration = 0.5f; // 3 2 1 mesajının ekranda kalacağı süre
    public float goMessageDuration = 0.5f; // Go mesajının ekranda kalacağı süre
    
    private bool isEnabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BallMovement>().enabled = false;
        countdownTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !isEnabled)
        {
            pressAnyKeyToStartText.SetActive(false);
            countdownTextObject.SetActive(true);
            isEnabled = true;
            StartCoroutine(StartCountdown());
        }
    }

    IEnumerator StartCountdown()
    {
        float timer = countdownDuration;

        while (timer > 0f)
        {
            // Geri sayım metnini güncelle
            countdownText.text = Mathf.CeilToInt(timer).ToString();
            
            if(timer == 3f)
                countdownText.color = new Color(1.0f, 0.0f, 0.0f); //Kırmızı
            else if (timer == 2f)
                countdownText.color = new Color(0.999279f, 1, 0);
            else if (timer == 1f)
                countdownText.color = new Color(0.004480481f, 1, 0);
            
            
            yield return new WaitForSeconds(countdownMessageDuration); //Metin arasındaki süreyi ayarlamak için parantez içini kullan.
            timer -= 1f;
        }

        //Go! yazı rengi
        if(timer == 0f)
            countdownText.color = new Color(1, 0, 0);
        
        // Geri sayım tamamlandığında "Go!" mesajını göster
        countdownText.text = "GO!";
        
        // Go mesajını belirli bir süre sonra kaldır
        yield return new WaitForSeconds(goMessageDuration);
        countdownText.text = "";
        
        GetComponent<BallMovement>().enabled = true;
        countdownTextObject.SetActive(false);
    }
}
