using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLightShadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Shadow;
    [SerializeField] private SpriteRenderer verticalShadow;
    private GameObject Master;
    private float masterDistance;
    private Animator m_Animator;
    private bool isBusy;
    private void Start()
    {
        m_Animator = Shadow.GetComponent<Animator>();
        masterDistance = 999.0f;
        if (m_Animator)
            m_Animator.SetBool("Hide", true);
        if (verticalShadow)
            verticalShadow.enabled = false;
    }

    public void SetBusy(bool state)
    {
        if (isBusy != state)
        {
            if (m_Animator)
                m_Animator.SetBool("Hide", !state);
            //Shadow.enabled = state;
            if (verticalShadow)
                verticalShadow.enabled = state;
            isBusy = state;
        }

    }
    public bool isPriority(float distance, GameObject master)
    {
        if (distance < masterDistance || Master == master || !isBusy)
        {
            Master = master;
            masterDistance = distance;
            return true;
        }
       
        return false;

    }
}
