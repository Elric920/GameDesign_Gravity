using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class SavingSystem: MonoBehaviour
{
    public static SavingSystem instance {get; private set; }
    public Transform birthPlace;
    LinkChain m_SaveChain;
    float RestartBias = 0.3f;    //有一个小小的从上到小掉落的偏移，防止小球在重新初始化时卡在地图里
    //public GameObject SavePointsTree;
    //List<SavePointMap> savePointMaps;


    void Awake()
    {
       instance = this;  
    }

    void Start()  
    {
        m_SaveChain = new LinkChain(birthPlace.position);

        /*
        savePointMaps = new List<SavePointMap>();
        GameObject[] checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        foreach(GameObject checkPoint in checkPoints)
        {
            string pointName = checkPoint.name;
            Vector2 vector = checkPoint.GetComponent<Transform>().position;
            savePointMaps.Add(new SavePointMap(pointName, vector));
        }

        --------暂时废弃Matthew 12.8 15:18
        --------游戏开始前，遍历场景中的所有检查点，把这些检查点的位置信息和对应的信息存入内存
        */
        
        
    }

    public Vector2 GetLatestSavePoint()
    {
        Vector2 restartPosition = m_SaveChain.GetLatestSavePoint();
        restartPosition.y += RestartBias;
        Debug.Log("读档：" + restartPosition);
        return restartPosition;
    }

    public void Save(string savePointName, Vector2 vector)
    {
        Debug.Log("Save!!!");
        bool savePossibility = m_SaveChain.AddLatestSavePoint(savePointName, vector);
        Debug.Log("SavePossibility:"+savePossibility);
    }
}





