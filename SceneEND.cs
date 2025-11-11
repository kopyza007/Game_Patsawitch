using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ----------------- SceneEND เพื่อใช้กับ secenจบเมื่อผ่เล่นเดินไปเหยียบหรือเตะ  -----------------------
public class SceneEND : MonoBehaviour
{
    
    public string sceneName; 

    // เมื่อผู้เล่นเข้า Trigger
    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าเป็นผู้เล่นหรือไม่ ถ้าใช้ ให้โหลด secene ขึ้น 
        if (other.CompareTag("Player"))
        {
           
            SceneManager.LoadScene("EngScene");
        }
    }
}
