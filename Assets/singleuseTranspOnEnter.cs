using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleuseTranspOnEnter : MonoBehaviour
{
    private Color color;

    private void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Color newColor = color;
            newColor.a = 0.25f;
            GetComponent<SpriteRenderer>().color = newColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
