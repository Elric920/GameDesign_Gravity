using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCanvas : MonoBehaviour
{
    public int requiredKeysNumber;
    public GameObject acceptButton;
    public GameObject declineButton;
    public GameObject escButton;
    public Player player;
    // Start is called before the first frame update

    void OnEnable()
    {
        if(requiredKeysNumber > UISystem.instance.GetKeyNumber())
        {
            acceptButton.SetActive(false);
            declineButton.SetActive(false);
            escButton.SetActive(true);
        }
        else
        {
            acceptButton.SetActive(true);
            declineButton.SetActive(true);
            escButton.SetActive(false);
        }
    }

    public void Accept()
    {
        player.SetJumpTwice(true);
        UISystem.instance.decreaseKey(requiredKeysNumber);
        GameManagement.instance.Resume(true);
        gameObject.SetActive(false);
    }

    public void Decline()
    {
        GameManagement.instance.Resume(true);
        gameObject.SetActive(false);
    }
}
