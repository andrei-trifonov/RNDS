using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class itemLightCol : MonoBehaviour
{
    private PlatformerCharacter2D PC2D;
    private HandsHolds HH;
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
   



}
