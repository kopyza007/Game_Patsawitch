using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerContrrol : MonoBehaviour
{
    // 1. การประกาศการเคลื่อนที่ ของตัว player เราจะอิงค์จากโค้ดที่เรียนมา 
    //------------ การเคลื่อนที่------------
    public float moveSpeed = 5f;                        // 1.ความเร็วปกติ
    public float sprintSpeed = 10f;                    //2. ความเร็วเมื่อกด Shift
    public float jumpForce = 7f;                      //3. ความแรงในการกระโดด(ส่วนนี้ปรับเป็น0)
    private Rigidbody rb;                            // ตัวแปร Rigidbody
    private bool isGrounded;                        // เช็คว่าตัวละครอยู่บนพื้นหรือไม่ (นำไปใช้กับการกระโดดได้อีกครั้งเมื่อตัวละครถงพื้น)
    private float currentSpeed;  
                                 
    //------------ คูลดาวน์การเร่งความเร็ว (Sprint)-------------
    public float sprintCooldown = 5f;                // คูลดาวน์ในหน่วยวินาที
    private float lastSprintTime = -Mathf.Infinity;  // เวลาในการใช้ Sprint ครั้งล่าสุด

    //------------ เลือด ------------
    public float health = 3f;      // เลือด
    public Image[] heartImages;   // Array เก็บรูปภาพของ UI Images (หัวใจ)
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
        // ------------เช็คTime เพื่อเป็นคูลดาวให้กับ Player (ปรับให้เป็น 0 ตอนนี้) +++++++++++++++
        float timeSinceLastSprint = Time.time - lastSprintTime;

        // -------------ตรวจสอบการเร่งความเร็วด้วยการกด Shift และคูลดาวน์ ---------------- 
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && timeSinceLastSprint >= sprintCooldown)
        {
            currentSpeed = sprintSpeed; 
        }
        else
        {
            currentSpeed = moveSpeed;  
        }

        //  --------------การเคลื่อนที่จากปุ่ม W, A, S, D หรือปุ่มลูกศร---------
        float moveX = Input.GetAxis("Horizontal");  // A, D หรือ Left, Right Arrow  เพื่อให้สามารถเดินซ้าย ขวา ได้ 
        float moveZ = Input.GetAxis("Vertical");    // W, S หรือ Up, Down Arrow เพื่อให้สามารถหน้าหลังได้ 

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
            lastSprintTime = Time.time;  // บันทึกเวลาที่ปล่อย Shift
        }
        PlayMovementSound();
    }

    // -------------- ใช้เพื่อเช็ค เสียงของผู่เล่น ----------------
    void PlayMovementSound()
    {
        if (currentSpeed == sprintSpeed && audioSource.clip != runSound) // วิ่ง 
        {
            audioSource.clip = runSound;
            audioSource.Play();  // เล่นเสียงวิ่ง
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
