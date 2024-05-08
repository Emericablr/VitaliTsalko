using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera; 
    [Header("Rotation")] 
    public float MaxYRotation = 20; 
    public float MinYRotation = -15; 
    [Header("Config")] 
    public float height = 2.0f; 
    public float mouseSensitivity = 2.0f; 
    public float ScrollSensitivity = 2.0f; 

    public bool UseMouse = true; 

    [HideInInspector] 
    public float mouseX; 
    [HideInInspector] 
    public float mouseY; 


    void Update()
    {
        if (UseMouse && Cursor.lockState == CursorLockMode.Locked) 
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSensitivity; 
            mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity; 
        }

        mouseY = Mathf.Clamp(mouseY, MinYRotation, MaxYRotation); 

        RotateCamera(mouseX, mouseY); 
    }

    void RotateCamera(float X, float Y)
    {
        Y = Mathf.Clamp(Y, MinYRotation, MaxYRotation); 
        Quaternion rotation = Quaternion.Euler(Y, X, 0); 
        transform.rotation = rotation; 
    }
}
