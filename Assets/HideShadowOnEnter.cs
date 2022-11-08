using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HideShadowOnEnter : MonoBehaviour
{
    
    [SerializeField] public  List<Animator> Shadow;
    [SerializeField] private GameObject VShadowMask;
    [HideInInspector] public  List<bool> isHiden;

    private void Start()
    {
        foreach (var item in Shadow)
        {
            isHiden.Add(false);
        }
      
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            for (int i = 0; i< Shadow.Count; i++)
            {
                bool status;
                if (status = Shadow[i].GetBool("Hide"))
                    isHiden[i] = status;
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
