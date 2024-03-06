using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float Speed = 1;
    public GameObject deadText;
    public GameObject finishText;
    public GameObject scoreText;

    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        deadText.SetActive(false);
        finishText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(-1, 0, 0);
        transform.position += Vector3.left * Speed;

        if (Speed != 0)
        {
            score += 1;
            scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString().PadLeft(6,'0');
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && Speed != 0)
        {
            Debug.Log("Sol oka basıldı.");

            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && Speed != 0)
        {
            Debug.Log("Sağ oka basıldı.");
            
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            Debug.Log("Çarpışma gerçekleşti.");
            
            Speed = 0; //Çarpışınca öldürecek kod.
            deadText.SetActive(true); //ÖLÜNCE ZORTLADIN YAZISI
        }
        
        if (collision.collider.tag == "Finish")
        {
            Debug.Log(("Oyun bitti."));
            
            Speed = 0; //Oyun bitince duracak.
            finishText.SetActive(true); //OYUN BİTİNCEKİ YAZI
        }
    }
    
}
