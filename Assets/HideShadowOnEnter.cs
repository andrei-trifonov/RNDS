using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HideShadowOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject globalShadow;
    [SerializeField] private Shadow2D _shadow2D;
    private bool bestQuality;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Quality") == 2)
            bestQuality = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (bestQuality)
                globalShadow.SetActive(false);
            else
                _shadow2D.SetShadow(-1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    
        if(other.CompareTag("Player"))
        {
            if (bestQuality)
                globalShadow.SetActive(true);
            else
                _shadow2D.SetShadow(-1);

        }
    }
}
