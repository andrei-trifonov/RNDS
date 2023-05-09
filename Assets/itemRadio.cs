using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemRadio : MonoBehaviour
{
    AudioSource m_AudioSource;
     DialogueSystem ds;
    [SerializeField] List<AudioClip> Music;
    [SerializeField] AudioClip Noise;
    [SerializeField] private GameObject Dialogue;
    private float timeLeft;
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
        if(timeLeft <= 0){

            ds.ContinueDialogue();
            timeLeft = 6f;
        }

        timeLeft -= Time.deltaTime;
    }

  
}
