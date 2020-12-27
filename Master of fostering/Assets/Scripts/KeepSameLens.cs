using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KeepSameLens : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    Camera m_ThisCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponentInParent<CinemachineVirtualCamera>();
        m_ThisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        m_ThisCamera.orthographicSize = virtualCamera.m_Lens.OrthographicSize;
    }
}
