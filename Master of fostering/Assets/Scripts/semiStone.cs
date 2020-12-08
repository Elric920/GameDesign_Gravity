using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************
 * 半稳定石运动规律
 * 1 向上匀加速到semiV
 * 2 向上匀速运动指定距离
 * 3 向上匀减速到0
 * 4 向下匀加速到semiV
 * 5 向下匀速运动指定距离
 * 6 向下匀减速到0
 * 7 循环
 * 
 * 
 * *******************************/



public class semiStone : MonoBehaviour
{
    private Transform semiStoneTrans; //半稳定石的位置 
    private Rigidbody2D rb2D;//半稳定石的rb2D组件


    //公开的变量
    public bool moveDirectionIsUpright = true;//半稳定石类型
    public float semiUpVelocity = 0f;//半稳定石向上的运动速度
    public float semiDownVelocity = 0f;//半稳定石向下的运动速度
    public float semiLeftVelocity = 0f;//半稳定石向左的运动速度
    public float semiRightVelocity = 0f;//半稳定石向右的运动速度

    //public float acceleratedSpeed = 0f;//半稳定石运动的加速度
    public float movementDistance = 0f;//半稳定石的运动范围

    //半稳定石位置判断
    private Vector2 semiP0;//记录半稳定石初始的位置
    private Vector2 semip;//记录半稳定石当前的位置
    Vector3 position = new Vector3(0, 0, 0);
    private int semiStoneMoveState = 0;//记录半稳定石的运动状态：1为向上匀加速，2为向上匀速，3为向上匀减速
                                       // 4为向下匀加速，5为向下匀速，6为向下匀减速

    //控制半稳定石速度
    private Vector2 presentVelocity = new Vector2(0f, 0f);//记录半稳定石当前的速度



    // Start is called before the first frame update
    void Start()
    {
        semiStoneTrans = this.GetComponent<Transform>();
        rb2D = this.GetComponent<Rigidbody2D>();

        semiP0 = semiStoneTrans.position;//游戏开始，记录半稳定石位置

    }

    // Update is called once per frame
    void Update()
    {

        if (moveDirectionIsUpright == true)
        {
            SemiStoneMoveState_Upright();//半稳定石竖直运动状态判断
            SemiStoneMove_Upright();//半稳定石竖直运动
        }

        if (moveDirectionIsUpright == false)
        {
            SemiStoneMoveState_Horizontal();//半稳定石竖直运动状态判断
            SemiStoneMove_Horizontal();//半稳定石竖直运动

        }

    }

    void SemiStoneMoveState_Upright()//半稳定石竖直运动状态判断
    {
        semip = semiStoneTrans.position;
        if (semiP0.y == semip.y) semiStoneMoveState = 2;
        if ((semip.y - semiP0.y) >= 0 && (semip.y - semiP0.y) <= movementDistance)
            if (rb2D.velocity.y > 0) semiStoneMoveState = 2;
        if ((semip.y - semiP0.y) > movementDistance)
        {
            position = this.transform.position;
            position.y = semiP0.y + movementDistance;
            this.transform.position = position;
            semiStoneMoveState = 5;
        }
        if ((semip.y - semiP0.y) == movementDistance) semiStoneMoveState = 5;
        if ((semip.y - semiP0.y) >= 0 && (semip.y - semiP0.y) <= movementDistance)
            if (rb2D.velocity.y < 0) semiStoneMoveState = 5;
        if (semip.y < semiP0.y)
        {
            position = this.transform.position;
            position.y = semiP0.y;
            this.transform.position = position;
            semiStoneMoveState = 2;
        }
    }

    private void SemiStoneMove_Upright() //半稳定石的运动
    {
        presentVelocity = rb2D.velocity;
        //if (semiStoneMoveState == 1)//半稳定石加速度向上的阶段
        //{
        //    presentVelocity.y += acceleratedSpeed * Time.deltaTime;
        //    if (presentVelocity.y >= semiVelocity) presentVelocity.y = semiVelocity;//速度加过头了
        //    rb2D.velocity = presentVelocity;
        //    print("加速向上");
        //}
        if (semiStoneMoveState == 2)//半稳定石匀速向上阶段
        {
            presentVelocity.y = semiUpVelocity;
            rb2D.velocity = presentVelocity;
            //  print("匀速向上");
        }

        //if (semiStoneMoveState == 3)//半稳定石匀减速向上的阶段
        //{
        //    presentVelocity.y -= acceleratedSpeed * Time.deltaTime;
        //    if (presentVelocity.y <= 0) presentVelocity.y = 0;//速度减过头了
        //    rb2D.velocity = presentVelocity;
        //    print("减速向上");
        //}
        //if (semiStoneMoveState == 4)//半稳定石匀加速向下的阶段
        //{
        //    presentVelocity.y -= acceleratedSpeed * Time.deltaTime;
        //    if (presentVelocity.y <= -semiVelocity)//速度加过头了
        //        rb2D.velocity = presentVelocity;
        //    print("加速向下");
        //}
        if (semiStoneMoveState == 5)//半稳定石匀速向下的阶段
        {
            presentVelocity.y = -semiDownVelocity;
            rb2D.velocity = presentVelocity;
            // print("匀速向下");
        }
        //if (semiStoneMoveState == 6)//半稳定石匀减速向下的阶段
        //{
        //    presentVelocity.y += acceleratedSpeed * Time.deltaTime;
        //    if (presentVelocity.y >= 0) presentVelocity.y = 0;//速度减过头了
        //    rb2D.velocity = presentVelocity;
        //    print("减速向下");
        //}
    }
    void SemiStoneMoveState_Horizontal()//半稳定石水平运动状态判断
    {
        semip = semiStoneTrans.position;
        if (semiP0.x == semip.x) semiStoneMoveState = 2;
        if ((semip.x - semiP0.x) >= 0 && (semip.x - semiP0.x) <= movementDistance)
            if (rb2D.velocity.x > 0) semiStoneMoveState = 2;
        if ((semip.x - semiP0.x) > movementDistance)
        {
            position = this.transform.position;
            position.x = semiP0.x + movementDistance;
            this.transform.position = position;
            semiStoneMoveState = 5;
        }
        if ((semip.x - semiP0.x) == movementDistance) semiStoneMoveState = 5;
        if ((semip.x - semiP0.x) >= 0 && (semip.x - semiP0.x) <= movementDistance)
            if (rb2D.velocity.x < 0) semiStoneMoveState = 5;
        if (semip.x < semiP0.x)
        {
            position = this.transform.position;
            position.x = semiP0.x;
            this.transform.position = position;
            semiStoneMoveState = 2;
        }
    }

    private void SemiStoneMove_Horizontal() //半稳定石的运动
    {
        presentVelocity = rb2D.velocity;
        if (semiStoneMoveState == 2)//半稳定石匀速向右阶段
        {
            presentVelocity.x = semiRightVelocity;
            rb2D.velocity = presentVelocity;
            //  print("匀速向右");
        }
        if (semiStoneMoveState == 5)//半稳定石匀速向左的阶段
        {
            presentVelocity.x = -semiLeftVelocity;
            rb2D.velocity = presentVelocity;
            // print("匀速向左");
        }

    }
}



    //private void OnGUI()
    //{
    //    if (GUILayout.Button("开始"))
    //    {

    //    }
    //    if (GUILayout.Button("停止"))
    //    {

    //    }
    //}

