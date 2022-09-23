using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class itemLightCol : MonoBehaviour
{
    public itemLight Light;


    private void OnTriggerExit2D(Collider2D other)
    {
        if ( other.CompareTag("ShadowCaster") ) 
        {
            Light.Shadows.Remove(other.gameObject);
            other.GetComponent<itemLightShadow>().SetBusy(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ShadowCaster") )
        {   
            Light.Shadows.Add(col.gameObject);
            col.GetComponent<itemLightShadow>().SetBusy(true);
            
        }
    }



}
