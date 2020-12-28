using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillCanvas : MonoBehaviour
{
    public int requiredKeysNumber;
    public TMP_Text finishSkillText;
    public TMP_Text needMoreText;
    public Player player;
    public GameObject panel;
    public GameObject suipian;
    bool m_SkillLearn;

    // Start is called before the first frame update
    void Start()
    {
        m_SkillLearn = false;
    }

    void OnEnable()   //由于Unity渲染的原因，请把所有按钮的渲染放在最后
    {
        player.SetSkillCanvas(this);
        if(requiredKeysNumber > UISystem.instance.GetKeyNumber())
        {
            panel.SetActive(true);
            suipian.SetActive(true);
            needMoreText.text = "Need   "+(requiredKeysNumber-UISystem.instance.GetKeyNumber());
        }
        else
        {
            Accept();
            panel.SetActive(true);
            finishSkillText.text = "Now you can jump once more even in the air!";
        }
    }

    public void Accept()
    {
        player.SetJumpTwice(true);
        UISystem.instance.decreaseKey(requiredKeysNumber);
        m_SkillLearn = true;
    }

    public void Decline()
    {
        gameObject.SetActive(false);
        player.SetSkillCanvas(null);
    }
}
