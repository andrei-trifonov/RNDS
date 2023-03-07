using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HideShadowOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject globalShadow;
    [SerializeField] private Material Shadow;
    private Color startColor;
    private void Start()
    {
        startColor = Shadow.color;
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
            Shadow.color =  new UnityEngine.Color(startColor.r, startColor.g, startColor.b, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    
        if(other.CompareTag("Player"))
        {
            globalShadow.SetActive(true);
            Shadow.color = startColor;

        }
    }
}
