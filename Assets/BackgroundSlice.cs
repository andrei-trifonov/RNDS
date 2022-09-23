using System;
using System.Collections;
using System.Collections.Generic;
using AdultLink;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BackgroundSlice : MonoBehaviour
{
    [SerializeField] private float Speed;
    private float offset;
    [SerializeField] private Material Mat;


  

    
    private void FixedUpdate()
    {
        offset += (Time.deltaTime * Speed) ;
        if (Mathf.Abs(offset) >= 1)
            offset = 0;
       Mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
