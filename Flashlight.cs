using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// ----------------- Flashlight  เพื่อใช้กับไฟฉาย ของ player -----------------------
public class Flashlight : AbObj
{
    public override void PickUp()
    {
        isHeld = true;
        transform.SetParent(GameManager.instance.handPosition);  // ตั้งให้ไฟฉายอยู่ในมือผู้เล่น
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void Drop()
    {
        isHeld = false;
        transform.SetParent(null);  // ปล่อยไฟฉายจากมือผู้เล่น
    }

    public void ToggleLight()
    {
        
        Light light = GetComponent<Light>();
        if (light != null)
        {
            light.enabled = !light.enabled;  
        }
    }
}