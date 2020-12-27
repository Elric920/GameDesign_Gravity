using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillCanvas : MonoBehaviour
{
    public int requiredKeysNumber;
    public GameObject acceptButton;
    public GameObject declineButton;
    public GameObject escButton;
    public GameObject tryButton;
    public GameObject learnSkillText;
    public GameObject finishSkillText;
    public Player player;
    bool m_SkillLearn;

    // Start is called before the first frame update
    void Start()
    {
        m_SkillLearn = false;
    }

    void OnEnable()   //由于Unity渲染的原因，请把所有按钮的渲染放在最后
    {
        player.SetSkillCanvas(this);
        if(m_SkillLearn)
        {
            learnSkillText.SetActive(false);
            finishSkillText.SetActive(true);
            acceptButton.SetActive(false);
            declineButton.SetActive(false);
            escButton.SetActive(false);
            tryButton.SetActive(true);
            return;
        }
        if(requiredKeysNumber > UISystem.instance.GetKeyNumber())
        {
            learnSkillText.SetActive(true);
            acceptButton.SetActive(false);
            declineButton.SetActive(false);
            escButton.SetActive(true);
            tryButton.SetActive(false);
            finishSkillText.SetActive(false);
        }
        else
        {
            learnSkillText.SetActive(true);
            acceptButton.SetActive(true);
            declineButton.SetActive(true);
            escButton.SetActive(false);
            tryButton.SetActive(false);
            finishSkillText.SetActive(false);
        }
    }

    public void Accept()
    {
        player.SetJumpTwice(true);
        UISystem.instance.decreaseKey(requiredKeysNumber);
        GameManagement.instance.Resume(true);
        gameObject.SetActive(false);
        m_SkillLearn = true;
    }

    public void Decline()
    {
        GameManagement.instance.Resume(true);
        gameObject.SetActive(false);
        player.SetSkillCanvas(null);
    }
}
