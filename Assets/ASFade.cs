using System;
using System.Collections;
using System.Collections.Generic;
using AdultLink;
using UnityEngine;

public class ASFade : MonoBehaviour
{
    private AudioSource m_AS;
    private float Speed = 0.2f;
    private bool fadeOut;
    private bool fadeIn;
    private void Start()
    {
        m_AS = GetComponent<AudioSource>();
    }

    public void Fade()
    {
        m_AS.volume = PlayerPrefs.GetFloat("Music");
        fadeOut = true;
    }

    public void Fade(AudioClip Clip)
    {
        Fade();
        StartCoroutine(FadeCoroutine(Clip));
    }

    IEnumerator FadeCoroutine(AudioClip Clip)
    {
        yield return new WaitForSeconds(5);
        m_AS.clip = Clip;
        fadeIn = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (fadeOut  )
        {
            if ( m_AS.volume > 0)
                m_AS.volume -= Time.deltaTime * Speed;
            else
            {
                fadeOut = false;
            }
        }
        if (fadeIn)
        {
            if ( m_AS.volume < PlayerPrefs.GetFloat("Music"))
                m_AS.volume += Time.deltaTime * Speed;
            else
            {
                fadeIn = false;
            }
        }
    }
}
