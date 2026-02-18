using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private float _mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private void Start()
    {
        //锁定光标
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        FreeLook();

    }
    //鼠标控制视角
    void FreeLook()
	{
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);
        Debug.Log(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 40f);// limit the angle
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
