using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace Tools{

    public class LinkChain  
    {
       int end;
       SavePointMap[] latestSavePoints;
       public LinkChain(Vector2 position)   //类初始化构造函数
       {
           end = 1;   //非空数组数量
           latestSavePoints = new SavePointMap[5];    //5个存档点
           latestSavePoints[0] = new SavePointMap("originalSite", position);  //如果玩家没有存任何档就死了，会默认回到初始位置
       }

       public bool AddLatestSavePoint(string savePointName, Vector2 vector)
       {   
           if(Equals(latestSavePoints[0].GetName(), savePointName)){
               Debug.Log("连续在同一地点触发存档");
               return false;  //如果玩家连续触发同一个存档点，之后的存档不会占用额外的存档位 
           }
           for(int t = end; t>0; t--)
           {
               latestSavePoints[t] = latestSavePoints[t-1];
           }
           latestSavePoints[0] = new SavePointMap(savePointName, vector);
           if(end < 5) end += 1;
           for(int t = 0; t<end; t++)
           {
               Debug.Log(t + "存档效果" +latestSavePoints[t] );
           }
           Debug.Log("已存档！ END：" + end);
           return true;
       }

       public Vector2 GetLatestSavePoint()   //默认获得最近的存档点的名称
       {
           return latestSavePoints[0].GetSavePointPosition();
       }
    }
}

