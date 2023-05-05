using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoneChange : MonoBehaviour
{
    [SerializeField] private string i;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerPrefs.SetString("Zone", i);
        }

    }
}
