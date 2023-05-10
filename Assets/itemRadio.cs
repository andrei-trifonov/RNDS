using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class itemRadio : MonoBehaviour
{
    [SerializeField] private Vector2 timeBounds;
    AudioSource m_AudioSource;
     DialogueSystem ds;
     private bool turnOff;
    [SerializeField] AudioClip Noise;
    [SerializeField] private GameObject Dialogue;
    private float timeLeft;
    private float timeLeftTurnOff=10;
    private GameObject spawned;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
       spawned  = Instantiate(Dialogue);
        spawned.GetComponent<FixedPos>().SetPoint(gameObject);
        ds = spawned.GetComponentInChildren<DialogueSystem>();
    }

    private void OnDisable()
    {
        spawned.SetActive(false);
    }

    private void FixedUpdate () {
        
        if(timeLeft <= 0)
        {
            turnOff = true;
            m_AudioSource.PlayOneShot(Noise);
            ds.ContinueDialogue();
            timeLeft = UnityEngine.Random.Range(timeBounds.x, timeBounds.y);
        }

        if (turnOff && timeLeftTurnOff <=0)
        {
            timeLeftTurnOff = 20;
            turnOff = false;
            ds.ContinueDialogue();
        }
        if (turnOff)
        {
            timeLeftTurnOff -= Time.deltaTime;
        }
        timeLeft -= Time.deltaTime;
    }

  
}
