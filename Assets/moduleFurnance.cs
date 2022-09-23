using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moduleFurnance : commonThrower
{
    //[SerializeField] float Speed;
    [SerializeField] private ShipMove Engine;
    [SerializeField] private Animator m_Anim;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip burnSound;
    
    private Vector3 directionOfTravel;
    private bool Blocked;
    public List<GameObject> burnList;
    
    // Start is called before the first frame update

    void SendFuel(float eff)
    {
        Engine.AddFuel(eff);
    }

    public void StartEngine()
    {
        Engine.SetEngineStarted(true);
    }

    public void StopEngine()
    {
        Engine.SetEngineStarted(false);
    }
    

    public override void BeforeSpawn()
    {
        Blocked = true;
        m_AudioSource.PlayOneShot(burnSound);
        m_Anim.SetBool("Burn", true);
    }

    public override void inSpawn(GameObject garbage)
    {
        m_Anim.SetBool("Burn", false);
    }

    public override void AfterSpawn()
    {
        Blocked = false;
    }

    public void AddFuel()
    {
        if (burnList.Count > 0 && !Blocked)
        {
            for (int i = 0; i < burnList.Count; i++)
            {
                SendFuel(burnList[i].transform.GetChild(0).GetComponent<itemFuel>().GetEfficience());
                burnList[i].SetActive(false);
            }

            StartCoroutine(ThrowerCoroutine(garbagePrefab,1.6f,2.5f));
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        burnList.Remove(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Item"))
        {
            if (other.transform.GetChild(0).GetComponent<itemFuel>())
                burnList.Add(other.gameObject);
        }
     
    }


}
