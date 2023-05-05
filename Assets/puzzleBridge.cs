using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleBridge : MonoBehaviour
{
    public PlatformerCharacter2D PC2D;
    public PanZoom Cam;
    private Animator m_Animator;
    public AudioClip Sound;
    private AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void Open()
    {
        m_AudioSource.PlayOneShot(Sound);
        StartCoroutine(EndPuzzleCoroutine());
       
    }
    IEnumerator EndPuzzleCoroutine()
    {
        m_Animator.SetBool("Open", true);
        PC2D.Block();
        Cam.ChangeZoom(19);
        yield return new WaitForSeconds(4);
        Cam.ChangeZoom(2.6f);
        yield return new WaitForSeconds(2);
        PC2D.UnBlock();
        Destroy(this);
    }
    }
