﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBlurTexture : MonoBehaviour
{
    public Camera blurCamera;
    public Material blurMaterial;

    // Start is called before the first frame update
    public void GetBlurMaterial()
    {
        if(blurCamera.targetTexture != null)
        {
            blurCamera.targetTexture.Release();
        }
        blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMaterial.SetTexture("_MainTex", blurCamera.targetTexture);
    }

}
