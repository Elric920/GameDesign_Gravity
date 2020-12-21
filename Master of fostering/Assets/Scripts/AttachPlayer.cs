using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    GameObject player;
    public float definedLockTime = 0.01f;
    bool locked;
    float lockTime;

    void Start()
    {
        locked = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(lockTime > 0) lockTime -= Time.deltaTime;
        else locked = false;
    }

    public void SetLock()
    {
        locked = true;
        lockTime = definedLockTime;
    }

    void BuildAttachment()   //黏住
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        player.transform.parent = this.transform;   //通过子对象实现的
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(locked) return;
        if(other.transform == player.transform)
        {
            BuildAttachment();
            Debug.Log("Attachment build!");
        }
    }

}
