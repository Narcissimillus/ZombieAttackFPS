using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : InteractionObject
{
    [HideInInspector] public PlayerController playerController;
    public override void Interaction()
    {
        base.Interaction(); // se apeleaza metoda parinte, in caz ca avem ceva generic
        Debug.Log("Interacting with Magazine.");
        //mecanica
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerController.totalBullets += 30;
        }
    
        //distrugem obiectul
        Destroy(gameObject);

    }
}
