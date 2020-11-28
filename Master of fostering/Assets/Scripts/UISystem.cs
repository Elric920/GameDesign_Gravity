using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    public static UISystem instance {get; private set;}
    public UnityEngine.UI.Text envelopesText;
    public UnityEngine.UI.Text keysText;
    int m_KeyNums;
    int m_EnvelopeNums;
    public int maxKeys = 0; 

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_KeyNums = 0;
        m_EnvelopeNums = 0;
        keysText.text = "0/" + maxKeys;   
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

}
