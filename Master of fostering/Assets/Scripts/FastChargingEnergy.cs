using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastChargingEnergy : MonoBehaviour
{  
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        player.EnableFastChargingEnergy();
        Debug.Log("可以快速充能");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        player.DisableFastChargingEnergy();
        Debug.Log("禁用快速充能");
    }
}
