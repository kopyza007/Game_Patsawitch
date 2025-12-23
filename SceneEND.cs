using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ----------------- SceneEND เพื่อใช้กับ secenจบเมื่อผ่เล่นเดินไปเหยียบหรือเตะ  -----------------------
public class SceneEND : MonoBehaviour
{
    
    public string sceneName; 

   
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
           
            SceneManager.LoadScene("EngScene");
        }
    }
}

