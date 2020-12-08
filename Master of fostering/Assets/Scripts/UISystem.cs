using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    public static UISystem instance {get; private set;}
    public UnityEngine.UI.Text envelopesText;
    public UnityEngine.UI.Text keysText;
    public UnityEngine.UI.Image mask;
    int m_KeyNums;
    int m_EnvelopeNums;
    public int maxKeys = 0; 
    [Tooltip("Explains how the Energy decrease pre frame.")]
    public float attenuatePerFrame = 0.1f;

    float m_OriginalSize;
    float m_Value;
    bool m_ReleaseMark = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_KeyNums = 0;
        m_EnvelopeNums = 0;
        keysText.text = "0/" + maxKeys;   
        m_OriginalSize = mask.rectTransform.rect.width;
    }

    void Update()
    {
        if(m_ReleaseMark)
        {
            m_Value -= attenuatePerFrame;
            if(m_Value < 0)
            {
                m_Value = 0;
                m_ReleaseMark = false;
            }
            ChangeSize();
        }
    }
    public void AddKey()
    {
        m_KeyNums += 1;
        keysText.text = m_KeyNums + "/"+ maxKeys; 
    }

    public void AddEnvelope()
    {
        m_EnvelopeNums += 1;
        envelopesText.text = m_EnvelopeNums.ToString();
    }

    public bool GetIfCollectedAllKeys()
    {
        if (m_KeyNums == maxKeys) return true;
        else return false;
    }

    public void SetValue(float value)
    {
        m_ReleaseMark = false;
        m_Value = value;
        ChangeSize();
    }

    public void ReleaseEnergy()
    {
        m_ReleaseMark = true;
    }

    void ChangeSize()
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_Value * m_OriginalSize);
    }
}
