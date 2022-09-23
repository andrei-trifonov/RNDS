using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShadowOnEnter : MonoBehaviour
{
    
    [SerializeField] private  List<Animator> Shadow;
    [SerializeField] private GameObject VShadowMask;
    [SerializeField] private  List<bool> isHiden;

    private void Start()
    {
       
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            for (int i = 0; i< Shadow.Count; i++)
            {

                isHiden[i] = Shadow[i].GetBool("Hide");
                Shadow[i].SetBool("Hide", true);    
            }
            


            VShadowMask.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i< Shadow.Count; i++)
            {
                if (isHiden[i] == false)
                    Shadow[i].SetBool("Hide", false);    
            }
           
                VShadowMask.SetActive(true);
       
        }
    }
}
