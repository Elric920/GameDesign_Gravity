using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUp : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    float m_LiftingTime;
    float m_DuringTime;
    float m_BalancingGravity;
    bool m_LiftingProcess = false;
    [Tooltip("The force dosen't count in gravity!")] //注释
    public float liftingForce = 3.0f;
    public float liftingTime = 1.0f;
    public float duringTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        m_LiftingTime = liftingTime;
        m_DuringTime = duringTime;
        m_BalancingGravity = rigidbody2D.mass * rigidbody2D.gravityScale * 9.8f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(m_LiftingProcess)
        {
            Debug.Log("FixedUPdate!");
            Lift();
        } 
    }

    void Lift()
    {
        if(m_LiftingTime > 0) 
        {
            Debug.Log("LiftingTime!");
            m_LiftingTime -= Time.deltaTime;
            rigidbody2D.AddForce(transform.up * liftingForce);
        }
        else if(m_DuringTime > 0) 
        {   
            Debug.Log("DuringTime!");
            m_DuringTime -= Time.deltaTime;
            rigidbody2D.AddForce(transform.up * m_BalancingGravity);
        }
        else
        {
            m_LiftingProcess = false;
            m_LiftingTime = liftingTime;
            m_DuringTime = duringTime;
        } 
    }

    public void StartLift()
    {
        m_LiftingProcess = true;
    }
}
