using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ActionZone : commonOutliner
{
   
    [SerializeField] private List <HealBar> m_Bar;
    [SerializeField] private bool Rope;
    [SerializeField] private bool Button;
    [SerializeField] private bool Rotor;
    [SerializeField] private bool Punch;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip Clip;
    [SerializeField] private ShowMenuExt SME;
    [SerializeField] GameObject Effect;
    [SerializeField] GameObject Target;
    [SerializeField] private  moduleBase machineModule;
    [SerializeField] private string actionName;
    [SerializeField] private bool ignoreCarry;
    private PlatformerCharacter2D CharacterController;
    private CarryManager CarryController;
    private bool Finished;
    private bool blockEffect;
   
    private void Start()
    {
       SearchForVisuals();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnableOutline();
            foreach (HealBar bar in m_Bar)
            {
                bar.SetTriggered(true);
                bar.Restart();
            }
            
            CharacterController = other.GetComponent<PlatformerCharacter2D>();
            CarryController = other.GetComponent<CarryManager>();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {   
       
        if (other.CompareTag("Player"))
        {
            DisableOutline();
            foreach (HealBar bar in m_Bar)
            {
                bar.SetTriggered(false);
                bar.Restart();
            }
        }
    }

    public void StartInteraction()
    {
       
        
        if (ignoreCarry || !CarryController.isPicked())
        {
            m_AudioSource.PlayOneShot(Clip);
            if (Rotor)
            {
                CharacterController.StartWorking(1, Target);
                Visual.GetComponent<Animator>().enabled = true;
            }
            if (Rope)
            {
                CharacterController.StartWorking(2, Target);
                Visual.GetComponent<Animator>().SetBool("Activate", true);
            }
            if (Button)
            {
               
                CharacterController.StartWorking(3, Target);
            }
            if (Punch)
            {
               
                CharacterController.StartWorking(4, Target);
            }
        }
        


    }

    public void StartBar()
    {
        foreach (HealBar bar in m_Bar)
        {
            bar.SetClicked(true);
        }
    }

    public void EndInteraction()
    {
        m_AudioSource.Stop();
        if (Rotor)
        {
            Visual.GetComponent<Animator>().enabled = false;
        }
        if (Rope)
        {
            Visual.GetComponent<Animator>().SetBool("Activate", false);
        }
        foreach (HealBar bar in m_Bar)
        {
            bar.SetClicked(false);
        }
        CharacterController.StopWorking();
    }

   
    IEnumerator EffectCoroutine()
    {

        blockEffect = true;
        Effect.SetActive(true);
        yield return new WaitForSeconds(1);
        Effect.SetActive(false);
        blockEffect = false;
    }

    public void InteractionFinished()
    {
        //TODO
        m_AudioSource.Stop();
        if (Rotor)
        {
            Visual.GetComponent<Animator>().enabled = false;
        }
        if (Rope)
        {
            Visual.GetComponent<Animator>().SetBool("Activate", false);
        }
        if (machineModule != null)  
            machineModule.Invoke(actionName, 0);
        if (!blockEffect)
            StartCoroutine(EffectCoroutine());
        Finished = true;
        CharacterController.StopWorking();
        foreach (HealBar bar in m_Bar)
        {
            bar.Restart();
        }
    }
    // Start is called before the first frame update



}
