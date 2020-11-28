using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public Transform player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform == player)
        {
            UISystem.instance.AddKey();
            //播放音效
            //播放动画
            Destroy(gameObject);
        }
    }
}
