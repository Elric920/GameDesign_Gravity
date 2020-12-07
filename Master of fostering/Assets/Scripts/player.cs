using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class player : MonoBehaviour {

    Transform players;
    Vector3 shootAngle;
    Camera cam;
    Transform hand;
    private Rigidbody2D rb2d;


    //小球状态相关变量
    //记录小球碰撞的物体类型：[0]稳定石、[1]半稳定石、[2]不稳定石
    private int[] collisionType = {0,0,0};
    


    //小球弹跳相关变量
    public float maxForce=500f;
    public float minForce = 400f;
    [Tooltip("小球最长蓄力时长(单位为s)：")]
    public float maxPressTime = 1.0f;
    //static private float downTime = 0f;//Du 2020 1207 2034
    //static private float upTime = 0f;//Du 2020 1207 2034
    //static private float force = 0f;//Du 2020 1207 2034

    //MatthewChen's Code 11.28 13:17 v1.0
    public GameObject floatObject;
    public GameObject doorToNextLevel;
    //MatthewChen's Code 11.28 23:30 v1.0
    bool m_DoorIsDetected = false;
    float m_PressDuringTime;

    //小球刹车相关量 Du 2020 1207 2034
    Vector2 playerVelocety;//记录玩家速度
    public float breakAcceleration;//设定刹车加速度
    bool playerBreaking=false;//记录小球是否刹车



    void Start()
    {
        cam = Camera.main;
        players = this.GetComponent<Transform>(); 
        hand = transform.Find("hand");
        rb2d=this.GetComponent<Rigidbody2D>();
        m_PressDuringTime = 0.0f;
        playerVelocety=new Vector2(0,0);
    }

    void Update()
    {
        Turning();
        TestMouseButton0();  //检测鼠标左键输入，控制小球弹跳

        //Du 2020 1207 2034
        RightMouseButtonDetection();//检测鼠标右键情况
        if (playerBreaking==true) PlayerBreak();//刹车


        //Matthew 11.28 13:19
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LiftUp liftUp = floatObject.GetComponent<LiftUp>();
            liftUp.StartLift();
        }

        //Matthew 11.28 23:30
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(m_DoorIsDetected) 
            {
                DoorController doorController = doorToNextLevel.GetComponent<DoorController>();
                if(doorController != null)
                {
                    doorController.DisplayDialog();
                }
                else Debug.Log("Door Detected but there is no right script attached to it.");
            }
        }

    }

    void Turning()//小球的转向函数
    {
        Vector3 playerposition = cam.WorldToScreenPoint(players.transform.position);
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = playerposition.z;
        players.transform.up = mouseposition - playerposition;
    }

    private void OnTriggerEnter2D(Collider2D collision)//碰撞发生，将对应元素置1
    {
        if (collision.tag == "stableStone")
        {
            collisionType[0] = 1;
            
        }
            
        else if (collision.tag == "semiStableStone")
        {
            collisionType[1] = 1;
        }
            
        else if (collision.tag == "nonStableStone")
        {
            collisionType[2] = 1;
        }

        else if(collision.tag == "door")
        {
            m_DoorIsDetected = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)//碰撞结束，将对应元素置0
    {
        if (collision.tag == "stableStone")
        {
            collisionType[0] = 0;
        }
            
        else if (collision.tag == "semiStableStone")
        {
            collisionType[1] = 0;
        }
            
        else if (collision.tag == "nonStableStone")
        {
            collisionType[2] = 0;
        }

        else if(collision.tag == "door")
        {
            m_DoorIsDetected = false;
        }
            
    }

    void Jumping(float force)//小球的弹跳函数
    {
        if (collisionType[0] == 1)//小球碰到了稳定石
        {
            rb2d.AddForce(-transform.up * force);
        }
        if (collisionType[1] == 1)//小球碰到了半稳定石 20201130Du
        {
            rb2d.AddForce(-transform.up * force);//暂定与稳定石运动规律相同  20201130Du
        }
    }

    void TestMouseButton0()//检测鼠标在允许小球弹跳的情况下按下的时间
    {
        if(Input.GetAxis("MouseButton0") > 10e-6)
        {
           m_PressDuringTime += Time.deltaTime;
           UISystem.instance.SetValue(m_PressDuringTime/maxPressTime); //此处返回给UI
           return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (m_PressDuringTime > 0.01 && m_PressDuringTime < 0.1)//如果按下鼠标的时间过短，则认为按下了0.1s
            m_PressDuringTime = 0.1f;
            if (m_PressDuringTime > maxPressTime)//如果按下鼠标的时间超过最大按压时限，则置为最大时限
            m_PressDuringTime = maxPressTime;
            Jumping(ForceValue(m_PressDuringTime, maxForce, minForce));
            UISystem.instance.ReleaseEnergy();
            m_PressDuringTime = 0;
        }

    }

    float ForceValue(float delT,float maxF,float minF)//根据鼠标按下的时长及弹跳力范围，计算弹跳力的大小
    {
        return ((maxF-minF)/0.9f) * (delT-1) + maxF;//呈线性变化
    }


    void RightMouseButtonDetection()//检测鼠标右键是否已经按下Du 2020 1207 2034
    {
        if (Input.GetMouseButtonDown(1)) playerBreaking = true;
        if (Input.GetMouseButtonUp(1)) playerBreaking = false;
    }

    void PlayerBreak()//小球刹车函数Du 2020 1207 2034
    {
        if (rb2d.velocity.x < 0f)//小球向左运动的刹车
        {
            print("左刹车");
            playerVelocety = rb2d.velocity;
            playerVelocety.x += breakAcceleration * Time.deltaTime;
            if (playerVelocety.x > 0f) //刹车过度使速度为正,则将速度置零
            {
                playerVelocety.x = 0f;
                rb2d.velocity = playerVelocety;
            }
            else rb2d.velocity = playerVelocety;//正常情况
        }
        if (rb2d.velocity.x > 0f)//小球向右运动的刹车
        {
            print("右刹车");
            playerVelocety = rb2d.velocity;
            playerVelocety.x -= breakAcceleration * Time.deltaTime;
            if (playerVelocety.x <0f) //刹车过度使速度为负,则将速度置零
            {
                playerVelocety.x = 0f;
                rb2d.velocity = playerVelocety;
            }
            else rb2d.velocity = playerVelocety;//正常情况
        }
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
