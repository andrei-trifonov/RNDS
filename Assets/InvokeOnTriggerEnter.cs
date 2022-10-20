using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private MonoBehaviour invokeScript;
    [SerializeField] private string enterMethod;
    [SerializeField] private string exitMethod;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            invokeScript.Invoke(enterMethod, 0);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            invokeScript.Invoke(exitMethod, 0);
        }
    }
}
