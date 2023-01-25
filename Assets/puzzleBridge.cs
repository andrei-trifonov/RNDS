using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleBridge : MonoBehaviour
{
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void Open()
    {
        m_Animator.SetBool("Open", true);
    }
}
