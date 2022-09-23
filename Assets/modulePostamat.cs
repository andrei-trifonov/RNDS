using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modulePostamat : commonThrower
{
    
   
    
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip giveSound;
  
    private void Start()
    {
       
        FindCamera();
    }




    public override void DoAction(int itemID)
    {
        

            m_AudioSource.PlayOneShot(giveSound);
            StartCoroutine(ThrowerCoroutine(garbagePrefab, 0.5f, 2.5f));
        

    }
  

}
