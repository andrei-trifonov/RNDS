using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleWaterTank : commonThrower
{
    [SerializeField] private float waterAmount;
    [SerializeField] private moduleHookTank Hook;
    [SerializeField] private ShipMove Engine;
    [SerializeField] private GameObject useEffect;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip useSound;
  
    private void Start()
    {
       
        FindCamera();
    }

    public override void BeforeSpawn()
    {
        Hook.ResetHook();
        Hook.gameObject.SetActive(false);
    }


    public override void inSpawn(GameObject garbage)
    {
        useEffect.SetActive(false);
    }
    
    public override void AfterSpawn()
    {
        
        Hook.gameObject.SetActive(true);
    }

    public override void DoAction(int itemID, float par1)
    {
        useEffect.SetActive(true);
        m_AudioSource.PlayOneShot(useSound);
        Engine.SetWater(par1);
        StartCoroutine(ThrowerCoroutine(Hook.GetGDB().GetItemFromList(itemID), 0.5f, 2.5f));
    }
  

}