using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HideShadowOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject globalShadow;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            globalShadow.SetActive(false);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    
        if(other.CompareTag("Player"))
        {
            globalShadow.SetActive(true);


        }
    }
}
