using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] Clips;
     private AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void Play()
    {
        m_AudioSource.PlayOneShot(Clips[Random.Range(0, Clips.Length)]);
    }
}
