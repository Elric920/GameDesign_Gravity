using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDeath : MonoBehaviour
{
    [Tooltip("死亡时需要多少秒的锁定时间")]
    public float lockTime = 3.0f;
    [Tooltip("每次颜色切换间隔为多长时间")]
    public float colorDuringTime = 0.5f;
    public Color[] colorArray;
    float m_LeftColorDuringTime;
    float m_LeftLockTime;
    bool m_DeathIsComing;
    Player player;
    SpriteRenderer spriteRenderer;
    int m_ArrayLength;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        m_LeftColorDuringTime = colorDuringTime;
        m_LeftLockTime = lockTime;
        m_DeathIsComing = false;
        player = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_ArrayLength = colorArray.Length;
        count = 0;
        if(m_ArrayLength == 0) Debug.LogError("No Color Selected!");
    }

    // Update is called once per frame
    void Update()
    {
        if(m_DeathIsComing)
        {
            m_LeftColorDuringTime -= Time.deltaTime;
            m_LeftLockTime -= Time.deltaTime;
            if(m_LeftLockTime <= 0) 
            {
                m_LeftColorDuringTime = colorDuringTime;
                m_LeftLockTime = lockTime;
                m_DeathIsComing = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                player.Death();
                spriteRenderer.color = Color.white;
                return;
            }
            if(m_LeftColorDuringTime <= 0)
            {
                spriteRenderer.color = colorArray[count % m_ArrayLength];
                count += 1;
                m_LeftColorDuringTime = colorDuringTime;   
            }
        }
    }

    public void DeathIsComing()
    {
        m_DeathIsComing = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
