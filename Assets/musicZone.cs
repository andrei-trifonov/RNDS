using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicZone : MonoBehaviour
{
    private ASFade _asFade;
    
    private void Start()
    {
        _asFade = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<ASFade>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _asFade.PlayNext();
            Destroy(gameObject);
        }
    }
}
