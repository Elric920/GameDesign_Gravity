using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecorateScreenControl : MonoBehaviour
{
    TMP_Text m_ScreenText;
    float m_CurrentTime;
    //float m_CurrentTimeForBlink;
    [Tooltip("描述了每次屏幕刷新的间隔时间。")]
    public float updateTextTime = 0.3f;
    [Tooltip("描述了显示在屏幕中最多的文字行数是多少？如果需要铺满屏幕，那么请把这个数值放大，但过大的数值可能导致内存泄露。")]
    public uint textLines = 15;
    //[Tooltip("描述了文字中下划线的闪烁间隔时间")]
    //public float inputBlinkTime = 0.3f;
    //const string m_FinalLineString = "_\n";
    string[] m_Resources = {
        "Building...",
        "Traning...",
        "Reparing...",
        "On Hold...",
        "Cancel...",
        "Unable to Comply, building in progress.",
        "Insufficient Funds",
        "Low Power...",
        "Structure sold.",
        "Construction Complete",
        "New Construction Options",
        "Can not Deploy here",
        "Our base is under attack.",
        "Our ally is under attack.",
        "Our miner is under attack.",
        "Mission accomplished.",
        "Mission failed.",
        "Battle control terminated.",
        "Establishing battle field control, stand by.",
        "Reinforcements ready.",
        "Iron Curtain ready.",
        "Iron Curtain activated.",
        "Warning, Nuclear silo detected.",
        "Warning, Nuclear missile launched.",
        "Warning, Weather control device detected.",
        "Warning, Lightning storm created."
    } ;

    int m_ResourcesLength;
    string m_OutputText;

    // Start is called before the first frame update
    void Start()
    {
        m_ScreenText = GetComponent<TMP_Text>();
        if(m_ScreenText == null) Debug.LogError("No TextMeshPro Component is added to "+ name); 
        m_CurrentTime = updateTextTime;
        //m_CurrentTimeForBlink = inputBlinkTime;
        m_ResourcesLength = m_Resources.Length;
        m_OutputText = "";
        for(int t=0; t<textLines; t++)
        {
            m_OutputText += m_Resources[Random.Range(0, m_ResourcesLength)] + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime -= Time.deltaTime;
        if(m_CurrentTime<=0)
        {
            m_CurrentTime = updateTextTime;   
            m_OutputText = m_OutputText.Substring(m_OutputText.IndexOf('\n')+1) + m_Resources[Random.Range(0, m_ResourcesLength)] + "\n";
            m_ScreenText.text = m_OutputText;    
        }
    }
}
