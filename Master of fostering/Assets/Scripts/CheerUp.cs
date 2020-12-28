using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CheerUp : MonoBehaviour
{
    float m_CurrentTime;
    bool m_IsCheered;
    Animator playerAni;
    private int addingID = Animator.StringToHash("isAdding");
    Rigidbody2D rigidbody2d;
    public float force = 1000f;
    public float[] keyTime = {0.77f, 1.5f};  //第二跳的开始时间
    bool[] m_JumpTimes = {false, false};
    public GameObject DialogCanvas;
    public string[] dialogStrings;
    int m_StringLength;
    int m_CurrentStringIndex;
    const float dialogShowingDuringTime = 2.5f;
    float m_CurrentDialogShowingDuringTime;
    float m_FinalTime = 2.5f;

    TMP_Text dialogText;
    void Start()
    {
        m_CurrentTime = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<Animator>();
        m_IsCheered = false;
        m_StringLength = dialogStrings.Length;
        m_CurrentStringIndex = 0;
        m_CurrentDialogShowingDuringTime = 0;
    }

    void Update()
    {
        if (m_IsCheered)
        {
            m_CurrentTime += Time.deltaTime;
            if(m_JumpTimes[0] == false)
            {
                rigidbody2d.AddForce(transform.up * force);
                //playerAni.SetBool(addingID, true);
                m_JumpTimes[0] = true;
            }
            else if(m_JumpTimes[1] == false && m_CurrentTime > keyTime[0])
            {
                rigidbody2d.AddForce(transform.up * force);
                m_JumpTimes[1] = true;
            }
            else if(m_CurrentTime > keyTime[1])
            {
                DialogCanvas.SetActive(true);
                if(dialogText == null) dialogText = GetComponentInChildren<TMP_Text>();
                m_CurrentDialogShowingDuringTime -= Time.deltaTime;
                if(m_CurrentDialogShowingDuringTime <= 0 && m_CurrentStringIndex <= m_StringLength)
                {
                    if(m_CurrentStringIndex == m_StringLength) 
                    {
                        if(m_FinalTime > 0) m_FinalTime -= Time.deltaTime;
                        else SceneManager.LoadScene("MainMenuScene");
                        return;
                    }
                    dialogText.text = dialogStrings[m_CurrentStringIndex];
                    m_CurrentStringIndex += 1;
                    m_CurrentDialogShowingDuringTime = dialogShowingDuringTime;
                }
            }
        }
    }

    public void Cheer()
    {
        m_IsCheered = true;
    }

}
