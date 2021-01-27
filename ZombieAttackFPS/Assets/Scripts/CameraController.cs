using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float yaw = 0f;
    float pitch = 0f;
    public float minPitch = -45f;
    public float maxPitch = 45f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speed * Input.GetAxis("Mouse X");
        pitch -= speed * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
