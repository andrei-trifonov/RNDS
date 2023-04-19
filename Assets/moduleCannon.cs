using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class moduleCannon : commonThrower
{
    //[SerializeField] float Speed;
  
    [SerializeField] private Animator m_Anim;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip burnSound;
    [SerializeField] private Transform Cannon;
    [SerializeField] private float reloadTime;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private ShipMove Engine;
    private GameObject Projectile;
    public List<GameObject> Projectiles;
    private Vector3 directionOfTravel;
    private bool Blocked;

    // Start is called before the first frame update
    
    

    public override void inSpawn(GameObject garbage)
    {
        Engine.FireFeedbackCoroutine();
        m_AudioSource.PlayOneShot(shotSound);
        m_Anim.SetBool("Shot", true);
    }

    public override void AfterSpawn()
    {
        Blocked = false;
        m_Anim.SetBool("Shot", false);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (Projectiles.Contains(other.gameObject))
        {
            Projectiles.Remove(other.gameObject);
            if (Projectiles.Count>0)
                Projectile = Projectiles.Last();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Item"))
        {
            if (other.transform.GetChild(0).GetComponent<itemFuel>() && !Projectiles.Contains(other.gameObject))
            {
                Projectiles.Add(other.gameObject);
                Projectile = Projectiles.Last();
            }
        }
     
    }

    
   

    private void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        Blocked = true;
        m_Anim.SetBool("Reload", true);
        m_AudioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        m_Anim.SetBool("Reload", false);
        if (Projectile && Projectiles.Count > 0)
        {
            Projectiles.Remove(Projectile);
            Projectile.SetActive(false);
            if (Projectiles.Count>0)
               Projectile = Projectiles.Last();
            StartCoroutine(ThrowerCoroutine(garbagePrefab, 1.6f, 2.5f));
        }
        else
        {
            Blocked = false;
        }
    }
    public void FireCannon()
    {
        throwAngle = (Cannon.transform.rotation.eulerAngles.z);
        if (!Blocked)
        {
            Reload();
        }
        
    }
    


}
