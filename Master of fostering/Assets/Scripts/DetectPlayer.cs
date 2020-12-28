using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    Player player;
    CheerUp cheerUp;

    void Start()
    {
        cheerUp = GetComponentInParent<CheerUp>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<Player>();
        if(player != null)
        {
            cheerUp.Cheer();
            player.SetLockCompletely();
        }
    }
}
