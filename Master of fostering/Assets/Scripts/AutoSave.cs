using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    ParticleSystem.MainModule mainModule;

    private void Start()
    {
        mainModule = GetComponent<ParticleSystem>().main;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            SavingSystem.instance.Save(name, transform.position);

            mainModule.startColor = Color.yellow;
        }
    }
}
