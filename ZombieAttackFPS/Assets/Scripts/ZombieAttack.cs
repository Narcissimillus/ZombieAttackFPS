﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Animator player = other.GetComponentInParent<Animator>();
        if (player.GetFloat("timeSinceLastHit") > 0.25f)
        {
            player.SetBool("takeHit", true);
        }
    }
}
