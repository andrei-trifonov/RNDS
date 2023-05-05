using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private GameObject Obj;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
           Obj.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Obj.SetActive(false);
        }
    }
}
