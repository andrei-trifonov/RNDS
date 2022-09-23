using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleWindow : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_Clip;
    [SerializeField] private Animator m_Animator;
    private bool isClosed;
    public void OpenClose()
    {
        isClosed = !isClosed;
        m_AudioSource.PlayOneShot(m_Clip);
        m_Animator.SetBool("Open", isClosed);
         
    }
    // Update is called once per frame

}
