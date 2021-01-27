using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyCameraRotation : MonoBehaviour
{
    Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, camTransform.eulerAngles.y, transform.eulerAngles.z);
    }
}
