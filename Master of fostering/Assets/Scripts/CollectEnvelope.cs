using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEnvelope : MonoBehaviour
{
    public Transform player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform == player)
        {
            UISystem.instance.AddEnvelope();
            //播放音效
            //播放动画
            Destroy(gameObject);
        }
    }
}
