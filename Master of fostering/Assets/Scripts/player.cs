using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class player : MonoBehaviour {

    public Transform players;
    Vector3 shootAngle;
    Camera cam;
    private Rigidbody2D rb2d;
    private Animator playerAni;
    private int addingID = Animator.StringToHash("isAdding");
    public UnityEngine.Experimental.Rendering.Universal.Light2D lightOnLand;
    private ParticleSystem hatParticle;

    //小球状态相关变量
    //记录小球碰撞的物体类型：[0]稳定石、[1]半稳定石、[2]不稳定石
    private int[] collisionType = {0,0,0};
    


    //小球弹跳相关变量
    public float maxForce=500f;
    public float minForce = 400f;
    [Tooltip("小球最长蓄力时长(单位为s)：")]
    public float maxPressTime = 1.0f;
    
    //MatthewChen's Code 11.28 13:17 v1.0
    public GameObject floatObject;
    public GameObject doorToNextLevel;
    //MatthewChen's Code 11.28 23:30 v1.0
    bool m_DoorIsDetected = false;
    float m_PressDuringTime;
    AudioSource audioSource;

    //小球弹跳力量修正相关
    public float forceCompensation=1f;
    public bool forceCompensationOpen = false;//是否修正弹射力


    void Start()
    {
        cam = Camera.main;
        players = this.GetComponent<Transform>(); 
        rb2d=this.GetComponent<Rigidbody2D>();
        m_PressDuringTime = 0.0f;
        playerAni = this.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        hatParticle = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        Turning();
        TestMouseButton0();  //检测鼠标左键输入


        //Matthew 11.28 13:19
        /* if(Input.GetKeyDown(KeyCode.Space))
        {
            LiftUp liftUp = floatObject.GetComponent<LiftUp>();
            liftUp.StartLift();
        } */  
        //12.8禁用Lift模块

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
        lightOnLand.intensity = 0.3f;
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
        lightOnLand.intensity = 0.0f;
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
           if(!audioSource.isPlaying) audioSource.Play();
           m_PressDuringTime += Time.deltaTime;
           UISystem.instance.SetValue(m_PressDuringTime/maxPressTime); //此处返回给UI
            playerAni.SetBool(addingID,true);
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
            playerAni.SetBool(addingID,false);
            audioSource.Stop();
            hatParticle.Play();
        }
    }

    float ForceValue(float delT,float maxF,float minF)//根据鼠标按下的时长及弹跳力范围，计算弹跳力的大小
    {
        float k = 0f;//小球弹射力的修正系数
        float F = ((maxF - minF) / 0.9f) * (delT - 1) + maxF;//修正前的力，线性变化结果
        float a = -players.transform.up.x;//小球弹射方向向量的水平分量

        //开口向下的抛物线修正倍率，forceCompensation(大于1)控制抛物线顶点位置，
        k = (float)(a * a + forceCompensation * (1 - a * a));
        if (forceCompensationOpen == true) F *= k;
        return F; 
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

    //MatthewChen 12.8 13:53
    public void Death()
    {
        //可能要先播个动画？？
        rb2d.velocity = new Vector2(0.0f, 0.0f);
        transform.position = SavingSystem.instance.GetLatestSavePoint();
    }

}
