﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinksDeathArea : MonoBehaviour
{
    // Start is called before the first frame update
     void OnTriggerEnter2D(Collider2D other)
    {
        LockDeath player = other.GetComponent<LockDeath>();
        if(player != null)
        {
            player.DeathIsComing();
        }
    }
}
