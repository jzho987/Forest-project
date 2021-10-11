using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform camAnchor;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        camAnchor.Rotate(Vector3.up, mouseX);
    }

    /**
     * shake the camera upon landing
     * follow the equation y = 2x^2 - 2x
     */
    public IEnumerator landingShake(float magnitude, float duration)
    {
        float elpasedTime = 0f;

        while(elpasedTime < duration)
        {
            //calculate porabolic y from y = 2x^2 - 2x
            float x = elpasedTime / duration;
            float y = 2 * (Mathf.Pow(x,2)) - 2 * x;
            transform.localPosition = Vector3.up * y;

            elpasedTime += Time.deltaTime;
            yield return null;
        }
    }
}
