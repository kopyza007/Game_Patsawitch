using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerContrrol : MonoBehaviour 
    // 1. การประกาศการเคลื่อนที่ ของตัว player เราจะอิงค์จากโค้ดที่เรียนมา 
    //------------ การเคลื่อนที่------------
    public float moveSpeed = 5f;                        // ความเร็วปกติ
    public float sprintSpeed = 10f;                    //ความเร็วเมื่อกด Shift
    public float jumpForce = 7f;                      //ความแรงในการกระโดด
    private Rigidbody rb;                            
    private bool isGrounded;                        // เช็คว่าตัวละครอยู่บนพื้นหรือไม่ (นำไปใช้กับการกระโดดได้อีกครั้งเมื่อตัวละครถองพื้น)
    private float currentSpeed;  
                                 
    //------------ คูลดาวน์การเร่งความเร็ว (Sprint)------------
    public float sprintCooldown = 5f;                // คูลดาวน์
    private float lastSprintTime = -Mathf.Infinity;  

    //------------ เลือด ------------
    public float health = 3f;      // เลือด
    public Image[] heartImages;   
    public Sprite heartFull;     
    public Sprite heartEmpty;

    //------------ เสียง ------------
    public AudioClip walkSound;           // เสียงเดิน
    public AudioClip runSound;           // เสียงวิ่ง
    private AudioSource audioSource;  
    private CharacterController characterController;  

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  
        UpdateHealthUI();

        
        characterController = GetComponent<CharacterController>();  
        audioSource = GetComponent<AudioSource>();  
    }

    private void Update()
    {
        // ------------เช็คTime เพื่อเป็นคูลดาวให้กับ Playern ++++++++++
        float timeSinceLastSprint = Time.time - lastSprintTime;

        // -------------ความเร็วด้วยการกด Shift และคูลดาวน ---------------- 
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && timeSinceLastSprint >= sprintCooldown)
        {
            currentSpeed = sprintSpeed; 
        }
        else
        {
            currentSpeed = moveSpeed;  
        }

        //  --------------การเคลื่อนที่จากปุ่ม W, A, S, D หรือปุ่มลูกศร---------
        float moveX = Input.GetAxis("Horizontal");  
        float moveZ = Input.GetAxis("Vertical");    

        //-------------- เคลื่อนที่ตัวละคร --------------
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * currentSpeed * Time.deltaTime);

        // --------------เป็นการเช็คว่าจะกระโดดเมื่อกด Space และอยู่บนพื้น --------------
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // -------------- ใช้เพื่อเช็คว่่เราไม่ได้กด Shift ตอนไหนเวลาจะเริ่มคูลดาว (ไม่ได้ใข้)
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            lastSprintTime = Time.time;  
        }
        PlayMovementSound();
    }

    // -------------- ใช้เพื่อเช็ค เสียงของผู่เล่น ----------------
    void PlayMovementSound()
    {
        if (currentSpeed == sprintSpeed && audioSource.clip != runSound) // วิ่ง 
        {
            audioSource.clip = runSound;
            audioSource.Play(); 
            Debug.Log("Playing Run Sound");
        }
        else if (currentSpeed == moveSpeed && audioSource.clip != walkSound)  // เเละ ดิน
        {
            audioSource.clip = walkSound;
            audioSource.Play();  // เล่นเสียงเดิน
            Debug.Log("Playing Walk Sound");
        }
    }
    // -------------- ใช้เพื่อเช็ค ว่าผู่เล่นโดนดาเมจหรือไม่? ----------------
    public void TakeDamage(float damage)
    {
        health -= damage;  
        if (health <= 0)
        {
            Die();  
        }
        UpdateHealthUI();
    }
    //-------------- ใช้เพื่อ set รูปภาพ  ----------------
    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < health)
            {
                heartImages[i].sprite = heartFull;  
            }
            else
            {
                heartImages[i].sprite = heartEmpty; 
            }
        }
    }
    //-------------- ใช้เพื่อเช็คว่าผู่เล่นตายหรือไม่  ----------------
    private void Die()
    {
        
        SceneManager.LoadScene("GameOverScene"); 
        
        Debug.Log("Player Dead");
    }
   

    //-------------- ใช้เพื่อเช็คว่าพื้น ----------------
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

