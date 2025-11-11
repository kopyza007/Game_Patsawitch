using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 


public class ButtRestart : MonoBehaviour
{

    // ----------------- กำหนดปุ่มต่างๆ ที่ใช้ในเกม -----------------------
    public PlayerContrrol player;

    // โหลด Scene ที่ชื่อว่า "StartScene" เพื่อเริ่มเกมใหม่หรือกลัยไปหน้าเริ่มต้น 
    public void RestartGame()
    {
        if (player != null)
        {
            player.health = 10; 
        }

        
        SceneManager.LoadScene("StartScene");
    }
    //ใช้ เพื่อ ให้ออกนอกเกม 
    public void QuitGame()
    {
        // ออกจากเกม
        Debug.Log("Game is exiting...");
        Application.Quit();  

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Letplay()
    {
        if (player != null)
        {
            player.health = 10; 
        }

        // โหลด Scene ที่ชื่อว่า "SampleScene" เพื่อเริ่มเกมใหม่
        SceneManager.LoadScene("SampleScene");
    }
}


