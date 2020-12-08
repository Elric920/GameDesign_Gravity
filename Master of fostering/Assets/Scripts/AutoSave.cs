using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if(player != null)
        {
            SavingSystem.instance.Save(name, transform.position);
        }
    }
}
