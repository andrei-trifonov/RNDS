using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleRepairer : commonThrower
{
    
    [SerializeField] private moduleHookRepairer Hook;
    [SerializeField] private GameObject fixEffect;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private AudioClip fixSound;
    [SerializeField] private Animator m_Anim;
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
        m_AudioSource.PlayOneShot(fixSound);
        fixEffect.SetActive(false);
        itemRepair iR;
        if (iR = garbage.transform.GetChild(0).gameObject.GetComponent<itemRepair>())
            iR.SetRepaired(true);
    }
    
    public override void AfterSpawn()
    {
        m_Anim.SetBool("Pipe", false);
        Hook.gameObject.SetActive(true);
    }

    public override void DoAction(int itemID)
    {

        m_Anim.SetBool("Pipe", true);
        fixEffect.SetActive(true);
        m_AudioSource.PlayOneShot(breakSound);
        StartCoroutine(ThrowerCoroutine(Hook.GetGDB().GetItemFromList(itemID), 0.5f, 2.5f));

    }

    public override void DoAction(int scrapCount, float par1)
    {
        m_Anim.SetBool("Pipe", true);
        fixEffect.SetActive(true);
        m_AudioSource.PlayOneShot(breakSound);
        
        for (int i = 0; i < scrapCount; i++)
        {
            StartCoroutine(ThrowerCoroutine(Hook.GetGDB().GetItemFromList(11), 0, 0));
        }

        
    }
}
