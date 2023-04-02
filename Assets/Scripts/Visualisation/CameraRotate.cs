using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    float sensitivity = -1;
    float x;
    float y;
    Vector3 rotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        x = Input.GetAxis("Mouse Y");
        y = Input.GetAxis("Mouse X");
        rotation = new Vector3(x, y * sensitivity, 0);
        transform.eulerAngles = transform.eulerAngles - rotation;
    }
}
