using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleBigHook :  commonMagneticPlace{
    // Start is called before the first frame update
    private bool isPrepared;
    private bool isGrabbed;
    public void Grab()
    { 
        //TODO animation

        if (isPrepared && !isGrabbed)
        {
            Picked = true;
            isGrabbed = true;
        }
        else if (isGrabbed)
        {
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
