using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class player : MonoBehaviour {
    Transform players;
    Vector3 shootAngle;
    Camera cam;
    Transform hand;

    bool canjump = false;
    private Rigidbody2D rb2d;
    public float maxForce=500f;
    public float minForce = 400f;
    static private float downTime = 0f;
    static private float upTime = 0f;
    static private float force = 0;


    //MatthewChen's Code 11.28 13:17 v1.0
    public GameObject floatObject;




    void Start()
    {
        cam = Camera.main;
        players = this.GetComponent<Transform>();
        //LayerMask floorMask = 1 << 8;
        //bullet = Resources.Load<GameObject>("prefabs/bullet");
        hand = transform.Find("hand");
        rb2d=this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Turning();
        force = ForceValue(MouseDownTime(),maxForce,minForce);
        Jumping(force);


        //Matthew 11.28 13:19
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Touched!");
            LiftUp liftUp = floatObject.GetComponent<LiftUp>();
            liftUp.StartLift();
        }

    }

    void Turning()//小球的转向函数
    {
        Vector3 playerposition = cam.WorldToScreenPoint(players.transform.position);
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = playerposition.z;
        players.transform.up = mouseposition - playerposition;
    }

    void Jumping(float force)//小球的弹跳函数
    {
        if (Input.GetMouseButtonUp(0) && canjump == true)
        {
            rb2d.AddForce(-transform.up * force);
        }
        if (Physics2D.Raycast(hand.transform.position, players.up, 1f,
               1 << LayerMask.NameToLayer("Collider")))
        {
           // Debug.Log("can jump");
            canjump = true;
        }
        if (!Physics2D.Raycast(hand.transform.position, players.up, 1f,
               1 << LayerMask.NameToLayer("Collider")))
        {
           // Debug.Log("can not jump");
            canjump = false;
        }            
    }

    float MouseDownTime()//返回鼠标在允许小球弹跳的情况下按下的时间
    {
        float deltTime = 0;
        if (Input.GetMouseButtonDown(0)&& Physics2D.Raycast(hand.transform.position, players.up, 5f,
               1 << LayerMask.NameToLayer("Collider")))
            downTime = Time.time;
        if (Input.GetMouseButtonUp(0)&& Physics2D.Raycast(hand.transform.position, players.up, 5f,
               1 << LayerMask.NameToLayer("Collider")))
        {
            upTime = Time.time;
            deltTime = upTime - downTime;
        }
        if (deltTime > 0.01 && deltTime < 0.1)//如果按下鼠标的时间过短，则认为按下了0.1s
            deltTime = 0.1f;
        if (deltTime > 1)//如果按下鼠标的时间超过1s，则认为按下了1s
            deltTime = 1f;
        return deltTime;
    }

    float ForceValue(float delT,float maxF,float minF)//根据鼠标按下的时长及弹跳力范围，计算弹跳力的大小
    {
        float force=0;
        force = ((maxF-minF)/0.9f) * (delT-1) + maxF;//呈线性变化
        return force;
    }
    
    
    


    public Vector3 ShootAngle
    {
        get
        {
            return shootAngle;
        }

        set
        {
            shootAngle = players.up;
        }
    }

    //MatthewChen 11.28 Change v1

}
