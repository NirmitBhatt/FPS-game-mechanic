using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    const string Mouse_X = "Mouse X";
    const string Mouse_Y = "Mouse Y";

    [SerializeField]
    private float mouseSensitivity = 10f;

    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    public Transform playerBody;
    public Transform cameraBody;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCameraWithMouse();
    }

    private void RotateCameraWithMouse()
    {
        RotateCameraOnXAxis();
        RotateCameraOnYAxis();
    }

    private void RotateCameraOnYAxis()
    {
        mouseY = Input.GetAxis(Mouse_Y) * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void RotateCameraOnXAxis()
    {
        mouseX = Input.GetAxis(Mouse_X) * mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
