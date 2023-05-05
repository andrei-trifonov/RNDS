using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Manager : MonoBehaviour
{
    [SerializeField] private  puzzleScales Scale1;
    [SerializeField] private  puzzleScales Scale2;
    [SerializeField] private Animator Gate;
    [SerializeField] private GameObject toDestroy1;
    [SerializeField] private GameObject toDestroy2;
    [SerializeField] private AudioClip chainSound;
    [SerializeField] private AudioClip finishSound;
    private AudioSource m_AudioSource;
    private bool Done;
    private int pos=3;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void CheckWeight()
    {
        int w1 = Scale1.GetWeight();
        int w2 = Scale2.GetWeight();
        if (!Done)
        {
            if (w1 > w2)
            {
                Gate.SetBool("Right", false);
                Gate.SetBool("Left", true);
                if (pos != 1)
                {
                    m_AudioSource.PlayOneShot(chainSound);
                }
                pos = 1;
            }

            if (w1 < w2)
            {
                Gate.SetBool("Left", false);
                Gate.SetBool("Right", true);
                if (pos != 2)
                {
                    m_AudioSource.PlayOneShot(chainSound);
                }
                pos = 2;
            }
            if (w1 == w2)
            {   
                Gate.SetBool("Left", false);
                Gate.SetBool("Right", false);
                if (pos != 3)
                {
                    m_AudioSource.PlayOneShot(chainSound);
                }
                pos = 3;
                if (w1 == 5)
                {
                    Done = true;
                    m_AudioSource.PlayOneShot(finishSound);
                    Gate.SetBool("Open", true);
                    StartCoroutine(DestroyCoroutine());
                }
            }
        }

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(6);
            toDestroy1.SetActive(false);
            Scale1.enabled = false;
            Scale2.enabled = false;
            toDestroy2.SetActive(false);
        }

        
    }
}
