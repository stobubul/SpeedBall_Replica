using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallMovement : MonoBehaviour
{
    public float Speed = 1;
    public GameObject deadText;
    public GameObject finishText;
    public GameObject scoreText;

    public Rigidbody rb;
    
    public float jumpForce = 5f; // Zıplama kuvveti
    public float groundCheckDistance = 0.2f; // Zemin kontrol mesafesi
    public LayerMask groundMask; // Zemin katmanı
    private bool isGrounded; // Karakterin zeminde olup olmadığını belirten değişken

    private int score = 0;
    
    public SphereCollider groundCollider; // SphereCollider kullanarak zemin temasını kontrol etmek için
    
    // Start is called before the first frame update
    void Start()
    {
        deadText.SetActive(false);
        finishText.SetActive(false);

        rb = GetComponent<Rigidbody>();
        
        // SphereCollider bileşenini al
        groundCollider = GetComponent<SphereCollider>();
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

            //transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            GetComponent<Rigidbody>().AddForce(Vector3.back * 10f, ForceMode.Impulse);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && Speed != 0)
        {
            Debug.Log("Sağ oka basıldı.");
            
            //transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
            GetComponent<Rigidbody>().AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        }

        // Zıplama işlemi için upArrow tuşuna basılırsa Jump() fonksiyonunu çağır
        if (Input.GetKeyDown(KeyCode.UpArrow) && Speed != 0)
        {
            Jump();
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
    
    void Jump()
    {
        if (IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 4.5f, ForceMode.Impulse);
        }
    }

    // SphereCollider kullanarak zemin temasını kontrol etmek için
    bool IsGrounded()
    {
        float radius = groundCollider.radius * transform.localScale.y; // Kürenin yarıçapı
        float distanceToGround = radius + 0.1f; // Zemin ile karakterin altı arasındaki mesafe

        // Kürenin merkeziyle zemin arasında bir ışın fırlat
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround);
    }
}
