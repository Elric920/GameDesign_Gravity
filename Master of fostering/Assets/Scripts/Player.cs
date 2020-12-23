using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class Player : MonoBehaviour {

    Vector3 shootAngle;
    Camera cam;
    private Rigidbody2D rb2d;
    private Animator playerAni;
    private int addingID = Animator.StringToHash("isAdding");
    private int fullID = Animator.StringToHash("isFull");
    public UnityEngine.Experimental.Rendering.Universal.Light2D lightOnLand;
    private ParticleSystem balltail;

    //小球状态相关变量
    //记录小球碰撞的物体类型：[0]稳定石、[1]半稳定石、[2]不稳定石、[3]闯关门
    private bool[] collisionType = {false,false,false,false};
    
    //小球弹跳相关变量
    public float maxForce=3000f;
    public float minForce = 400f;
    [Tooltip("小球最长蓄力时长(单位为s)：")]
    public float maxPressTime = 1.0f;
    private const float minPressTime = 0.1f;
    
    //MatthewChen's Code 12.21 13:17 v1.1
    //public GameObject doorToNextLevel;
    //MatthewChen's Code 11.28 23:30 v1.0
    //MatthewChen 12.23
    float m_PressDuringTime;
    bool m_BlockMouseButton0;  //定义了当前充能状态是否已经被玩家点击鼠标右键而终止
    bool m_FastChargingEnergy;  //定义了当前的状态是否是可以快速充能的
    
    //小球弹跳力量修正相关
    public float forceCompensation=1f;
    public bool forceCompensationOpen = false;//是否修正弹射力


    void Start()
    {
        cam = Camera.main; 
        rb2d=this.GetComponent<Rigidbody2D>();
        m_PressDuringTime = 0.0f;
        playerAni = this.GetComponent<Animator>();
        balltail = GetComponent<ParticleSystem>();
        m_BlockMouseButton0 = false;
        m_FastChargingEnergy = false;
    }

    void Update()
    {
        Turning();
        TestMouseButton();  //检测鼠标左键输入

        //Matthew 11.28 13:19
        /* if(Input.GetKeyDown(KeyCode.Space))
        {
            LiftUp liftUp = floatObject.GetComponent<LiftUp>();
            liftUp.StartLift();
        } */  
        //12.8禁用Lift模块

        //Matthew 11.28 23:30
        /* if(Input.GetKeyDown(KeyCode.Z))
        {
            if(collisionType[3]) 
            {
                DoorController doorController = doorToNextLevel.GetComponent<DoorController>();
                if(doorController != null)
                {
                    doorController.DisplayDialog();
                }
                else Debug.Log("Door Detected but there is no right script attached to it.");
            }
        } */
    }

    void Turning()//小球的转向函数
    {
        Vector3 playerposition = cam.WorldToScreenPoint(transform.position);
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = playerposition.z;
        transform.up = mouseposition - playerposition;
    }

    public void SetCollisionTag(uint tagNumber, bool value, bool enter = true)
    {
        if(enter) 
        {
            lightOnLand.intensity = 0.3f;    
        }
        else
        {
            lightOnLand.intensity = 0.0f;
        }
        collisionType[tagNumber] = value; 
    }

    void Jumping(float force)//小球的弹跳函数
    {
        if(transform.parent != null)
        {
            //小球在半浮力石上跳跃后，有一小段时间（默认是0.01s）不可再次被半浮力石变为子物体
            AttachPlayer attachPlayer = GetComponentInParent<AttachPlayer>();
            attachPlayer.SetLock();
            transform.parent = null;  //把父节点置为根节点
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
        if (collisionType[0])//小球碰到了稳定石
        {
            rb2d.AddForce(-transform.up * force);
        }
        if (collisionType[1])//小球碰到了半稳定石 20201130Du
        {
            rb2d.AddForce(-transform.up * force);//暂定与稳定石运动规律相同  20201130Du
        }
        AudioManager.instance.Play("AddingForceShoot");
    }

    void TestMouseButton()//检测鼠标在允许小球弹跳的情况下按下的时间以及右键取消
    {
        if(!m_BlockMouseButton0 && Input.GetAxis("MouseButton0") > 10e-6)
        {
            if(Input.GetMouseButtonDown(1))
            {
                AudioManager.instance.Stop("AddingForceUnfinished");
                AudioManager.instance.Stop("AddingForceFinished");
                m_PressDuringTime = 0;
                //是否需要加入停止动画？？
                playerAni.SetBool(addingID, false);
                playerAni.SetBool(fullID, false);
                m_BlockMouseButton0 = true;    //当按了鼠标右键后会封锁鼠标左键的输入
                return;
            }
           if(m_FastChargingEnergy)  //如果允许快速充能则立马视为按压时间已到最高
           {
               m_PressDuringTime = maxPressTime;
           }
           
           m_PressDuringTime += Time.deltaTime;
           playerAni.SetBool(addingID, true);
           UISystem.instance.SetValue(m_PressDuringTime/maxPressTime); //此处返回给UI

           if (m_PressDuringTime > maxPressTime)//如果按下鼠标的时间超过最大按压时限
            {
                playerAni.SetBool(addingID, false);
                playerAni.SetBool(fullID, true);
                if(!AudioManager.instance.isPlaying("AddingForceFinished"))
                AudioManager.instance.Play("AddingForceFinished");

            }
            else
            {
                if(!AudioManager.instance.isPlaying("AddingForceUnfinished"))
                AudioManager.instance.Play("AddingForceUnfinished");
            }
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(m_BlockMouseButton0) 
            {
                m_BlockMouseButton0 = false;
                return;
            }
            if (m_PressDuringTime > 0.01 && m_PressDuringTime < 0.1)//如果按下鼠标的时间过短，则认为按下了0.1s
            m_PressDuringTime = 0.1f;
            if (m_PressDuringTime > maxPressTime)//如果按下鼠标的时间超过最大按压时限，则置为最大时限
            {
                m_PressDuringTime = maxPressTime;
            }
            Jumping(ForceValue(m_PressDuringTime, maxForce, minForce));
            UISystem.instance.ReleaseEnergy();
            m_PressDuringTime = 0;
            playerAni.SetBool(addingID, false);
            playerAni.SetBool(fullID, false);
            AudioManager.instance.Stop("AddingForceUnfinished");
            AudioManager.instance.Stop("AddingForceFinished");
            balltail.Play();
        }
    }


    float ForceValue(float pressDuringTime,float maxF,float minF)//根据鼠标按下的时长及弹跳力范围，计算弹跳力的大小
    {
        float k = 0f;//小球弹射力的修正系数
        float F = ((maxF - minF) / (maxPressTime - minPressTime)) * (pressDuringTime - maxPressTime) + maxF;//修正前的力，线性变化结果
        float a = -transform.up.x;//小球弹射方向向量的水平分量

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
            shootAngle = transform.up;
        }
    }

    //MatthewChen 12.8 13:53
    public void Death()
    {
        //可能要先播个动画？？
        rb2d.velocity = new Vector2(0.0f, 0.0f);
        transform.position = SavingSystem.instance.GetLatestSavePoint();
    }

    public void EnableFastChargingEnergy()
    {
        m_FastChargingEnergy = true;
    }

    public void DisableFastChargingEnergy()
    {
        m_FastChargingEnergy = false;
    }

}
