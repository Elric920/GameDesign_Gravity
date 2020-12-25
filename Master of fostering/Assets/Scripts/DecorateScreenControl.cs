using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecorateScreenControl : MonoBehaviour
{
    TMP_Text m_ScreenText;
    float m_CurrentTime;
    float m_CurrentTimeForBlink;
    [Tooltip("描述了每次屏幕刷新的间隔时间。")]
    public float updateTextTime = 1.0f;
    [Tooltip("描述了显示在屏幕中最多的文字行数是多少？如果需要铺满屏幕，那么请把这个数值放大，但过大的数值可能导致内存泄露。")]
    public uint textLines = 50;
    [Tooltip("描述了文字中下划线的闪烁间隔时间")]
    public float inputBlinkTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        m_ScreenText = GetComponent<TMP_Text>();
        if(m_ScreenText == null) Debug.LogError("No TextMeshPro Component is added to "+ name); 
        m_CurrentTime = updateTextTime;
        m_CurrentTimeForBlink = inputBlinkTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime -= Time.deltaTime;
        if(m_CurrentTime<=0)
        {
            m_CurrentTime = updateTextTime;
            
        }
        
    }
}
