using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moduleCannon : commonThrower
{
    //[SerializeField] float Speed;
  
    [SerializeField] private Animator m_Anim;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip burnSound;
    [SerializeField] private Transform Cannon;
    [SerializeField] private float fireCorrection;
    [SerializeField] public List<moduleReloader> Reloaders;
    private Vector3 directionOfTravel;
    private bool Blocked;

    // Start is called before the first frame update
    

    public override void BeforeSpawn()
    {
        
        Blocked = true;
        m_AudioSource.PlayOneShot(burnSound);
        m_Anim.SetBool("Reload", true);
    }

    public override void inSpawn(GameObject garbage)
    {
        m_Anim.SetBool("Reload", false);
        m_Anim.SetBool("Shot", true);
    }

    public override void AfterSpawn()
    {
        Blocked = false;
        m_Anim.SetBool("Shot", false);
    }

    private void Reload()
    {
        for(int i=1; i< Reloaders.Count; i++)
        {
            
            Reloaders[i-1].Exchange(Reloaders[i]);
        }
    }
    public void FireCannon()
    {
        throwAngle = (Cannon.transform.rotation.eulerAngles.z)%360 - fireCorrection;
        if (Reloaders[0].isLoaded() && !Blocked)
        {
          
            Reload();
            
            StartCoroutine(ThrowerCoroutine(garbagePrefab,1.6f,2.5f));
            
        }
        else
        {
            Reload();
        }

    }
    


}
