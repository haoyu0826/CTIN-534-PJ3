using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 200f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    public bool canMove = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}