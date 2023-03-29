using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class itemLightCol : MonoBehaviour
{
    private PlatformerCharacter2D PC2D;
    private HandsHolds HH;
    public itemLight Light;
    private bool facingRight = true;
    public GameObject lightParent;
    private void OnEnable()
    {
        HH = GameObject.FindObjectOfType<HandsHolds>();
        PC2D = GameObject.FindObjectOfType<PlatformerCharacter2D>();
    }
    private void FixedUpdate()
    {
        if ( lightParent && HH.Item() == lightParent)
        {
            if (PC2D.GetFacing() != facingRight)
            {
                facingRight = !facingRight;
                gameObject.transform.localScale *= -1;
            }
        }
    }
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
