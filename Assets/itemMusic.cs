using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMusic : MonoBehaviour
{
    [SerializeField] private int randomPercent;
    [SerializeField] private AudioClip[] Clips;
     private AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void Play()
    {
        m_AudioSource.pitch *= 1 + Random.Range(-randomPercent / 100, randomPercent / 100);
        m_AudioSource.PlayOneShot(Clips[Random.Range(0, Clips.Length)]);
    }
}
