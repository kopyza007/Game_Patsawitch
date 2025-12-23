using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    // ----------------- ให้มีเมาส์เกิดขึ้นมาใน Secene ที่กำหนดขึ้นมา -----------------------
    void Start()
    {
        Cursor.visible = true;  
        Cursor.lockState = CursorLockMode.None;  
    }
}

