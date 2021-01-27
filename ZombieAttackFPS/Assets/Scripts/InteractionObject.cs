using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public float radius = 1f;
    public Transform interactionPoint;
    Transform interactionObject;
    bool done = false;

    //metoda abstracta, speficica fiecarui tip de interactiuni
    public virtual void Interaction()
    {
        Debug.Log("Interacting with base class.");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(interactionObject.position, interactionPoint.position);

        //if (distance <= radius && !done) // avem interactiune cu obiectul, pot sa afisez informatii: de ex "Press E to use"
        //{
        //    done = true;
        //    Interaction();
        //}
    }
}
