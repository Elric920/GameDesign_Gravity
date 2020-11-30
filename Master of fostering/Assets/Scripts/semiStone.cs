using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semiStone : MonoBehaviour
{
    private Transform semiStoneTrans; //半稳定石的位置 
    private Rigidbody2D rb2D;//半稳定石的rb2D组件

    public float initialVelocity = 0;//半稳定石运动的初速度
    public float acceleratedSpeed = 0;//半稳定石运动的加速度

    private Vector2 semiStoneInitialPosition;//记录半稳定石初始的位置
    private Vector2 semiStonepresentPosition;//记录半稳定石当前的位置
    private int positionState=0;//记录半稳定石的位置状态：1为下，2为上

    private Vector2 presentVelocity = new Vector2(0f, 0f);//记录半稳定石当前的速度
    // Start is called before the first frame update
    void Start()
    {
        semiStoneTrans = this.GetComponent<Transform>();
        rb2D = this.GetComponent<Rigidbody2D>();

        semiStoneInitialPosition = semiStoneTrans.position;//游戏开始，记录半稳定石位置

        presentVelocity.y = -initialVelocity;//游戏开始，给半稳定石一个向下的初速度
        rb2D.velocity = presentVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        SemiStonePosion();//判断当前半稳定石位置
        SemiStoneMove();//半稳定石运动
    }

    void SemiStonePosion()//半稳定石位置判断
    {
        semiStonepresentPosition = semiStoneTrans.position;
        if (semiStoneInitialPosition.y > semiStonepresentPosition.y)//半稳定石当前位置低于半稳定石初始位置
            positionState = 1;
        if (semiStoneInitialPosition.y== semiStonepresentPosition.y)
            positionState = 0;
        if (semiStoneInitialPosition.y < semiStonepresentPosition.y)
            positionState = 2;
    }

    private void SemiStoneMove() //半稳定石的运动
    {
        if (positionState == 1)//半稳定石加速度向上的阶段
        {
            presentVelocity.y += acceleratedSpeed * Time.deltaTime;
            rb2D.velocity = presentVelocity;

        }
        if (positionState ==2)//半稳定石加速度向下的阶段
        {
            presentVelocity.y -= acceleratedSpeed * Time.deltaTime;
            rb2D.velocity = presentVelocity;
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



    //左右

}
