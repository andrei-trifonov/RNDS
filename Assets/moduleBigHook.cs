using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleBigHook :  commonMagneticPlace{
    // Start is called before the first frame update
    [SerializeField] private Animator m_Animator;
    private bool isPrepared;
    private bool isGrabbed;
    private bool isClosed;
    public void Grab()
    {
        if (!isClosed)
        {
            m_Animator.SetBool("Catch", true);
            isClosed = true;
        }
        else
        {
            m_Animator.SetBool("Catch", false);
            isClosed = false;
        }
        
        if (isPrepared && !isGrabbed)
        {
            m_Animator.SetBool("Catch", true);
            Picked = true;
            isGrabbed = true;
        }
        else if (isGrabbed)
        {
            m_Animator.SetBool("Catch", false);
            isGrabbed = false;
            o_MagneticItem = Item.transform.GetChild(0).gameObject.GetComponent<MagneticItem>();
            Picked = false;
            o_MagneticItem.StopPick();
            Hooked = false;
        }
            
       
       
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BigHookItem"))
        {
            Item = other.gameObject;
            isPrepared = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BigHookItem"))
        {
            isPrepared = false;
        }
    }
}
