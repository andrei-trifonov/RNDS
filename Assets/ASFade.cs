using System;
using System.Collections;
using System.Collections.Generic;
using AdultLink;
using UnityEngine;
using UnityEngine.Audio;

public class ASFade : MonoBehaviour
{
    [SerializeField]private AudioMixer Mixer;
    private AudioSource m_AS;
    private float Speed = 0.2f;
    private bool fadeOut;
    private bool fadeIn;
    private int nowPlaying;
    [SerializeField] private AudioClip[] OST;
    private float maxVolumeMusic;
    
    private void Start()
    {
        maxVolumeMusic = PlayerPrefs.GetFloat("Music");
        SetVolume(PlayerPrefs.GetFloat("Sound")*PlayerPrefs.GetFloat("Master"));
        m_AS = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        AudioMixerGroup[] audioMixerGroups = Mixer.FindMatchingGroups("Master");
        if (audioMixerGroups.Length > 0)
        {
            audioMixerGroups[0].audioMixer.SetFloat("Volume",     Mathf.Lerp(-80, 0, Mathf.InverseLerp(0f, 1f, volume)) );
        }
    }

    public void Fade()
    {
        m_AS.volume = PlayerPrefs.GetFloat("Music")*PlayerPrefs.GetFloat("Master");
        fadeOut = true;
    }

    public void Fade(int i)
    {
        Fade(OST[nowPlaying]);
        
    }
    public void PlayNext()
    {
        Fade(OST[nowPlaying]);
        if (nowPlaying == OST.Length)
            nowPlaying = 0;
        nowPlaying++;
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
        m_AS.Play();
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
            if ( m_AS.volume <    maxVolumeMusic)
                m_AS.volume += Time.deltaTime * Speed;
            else
            {
                fadeIn = false;
            }
        }
    }
}
