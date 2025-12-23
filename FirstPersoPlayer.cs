using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersoPlayer : MonoBehaviour
{
    public float lookSpeedX = 2f; // ความเร็วในการหมุนกล้องตามแกน X (ซ้าย-ขวา)
    public float lookSpeedY = 2f; // ความเร็วในการหมุนกล้องตามแกน Y (ขึ้น-ลง)
    private float rotationX = 0f;  // การหมุนในแนวแกน Y (ขึ้น-ลง)
    private Transform playerBody; 

    public float cameraDistance = 0f; 
    public float cameraMinDistance = 2f; 

    private void Start()
    {
        playerBody = transform.parent; // ใช้เพื่อหาตำแหน่งของตัวละคร (Player) ที่กล้องติดอยู่
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; // ซ่อนเมาส์ ซึ่งอาจจะทำให้เกิดบัคเมื่อผู่เล่นตายผมเลยเเก้โดยใส่ mouseControlขึ้นมาใน Scene ที่ต้องการเเทน (จะเป็นอีก สคีปนึ่ง)
    }

    private void Update()
    {
        //------------ การหมุนของกกล้องทั้งหมด ซ้าย-ขวา บน-ล่างตามเเนวเเกน XY  ------------
        float mouseX = Input.GetAxis("Mouse X"); // การเคลื่อนไหวเมาส์ในแกน X (ซ้าย-ขวา)
        float mouseY = Input.GetAxis("Mouse Y"); // การเคลื่อนไหวเมาส์ในแกน Y (ขึ้น-ลง)

        
        playerBody.Rotate(Vector3.up * mouseX * lookSpeedX);

       
        rotationX -= mouseY * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // ป้องกันกล้องทะลุกำแพง
        PreventCameraClipping();
    }
    //------------ ป้องกันกล้องทะลุกำแพง ------------
    private void PreventCameraClipping()
    {
        // ----------------- ใช้ Raycas เพื่อเช็คระยะของ playerเเละ Wall -----------------------
        RaycastHit hit;
        Vector3 direction = transform.position - playerBody.position; 

        
        if (Physics.Raycast(playerBody.position, direction.normalized, out hit, cameraDistance))
        {
            
            float distanceToObstacle = hit.distance;
            cameraDistance = Mathf.Clamp(distanceToObstacle, cameraMinDistance, Mathf.Infinity);
        }
        else
        {
            
            cameraDistance = 1.5f;
        }

       
        transform.localPosition = new Vector3(0f, 1f, -cameraDistance);
    }
}

