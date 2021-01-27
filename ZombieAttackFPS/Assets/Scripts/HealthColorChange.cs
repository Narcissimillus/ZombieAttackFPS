using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthColorChange : MonoBehaviour
{
    public Gradient gradient;
    Image fill;
    // Start is called before the first frame update
    void Start()
    {
        fill = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
