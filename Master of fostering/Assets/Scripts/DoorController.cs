using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject SkillCanvas;

    public void EnableCanvas()
    {
        SkillCanvas.SetActive(true);
        GameManagement.instance.Pause(true);
    }

    public void DisableCanvas()
    {
        SkillCanvas.SetActive(false);
        GameManagement.instance.Resume(true);
    }
}
