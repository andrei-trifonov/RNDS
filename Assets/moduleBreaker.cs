using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleBreaker : commonThrower
{
    [SerializeField] private BreakManager breakManager;
    [SerializeField] private GameObject brokenPartVisual;
    [SerializeField] private int needItemID;
    [SerializeField] private GameObject breakEffect;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private AudioClip fixSound;
    [SerializeField] private moduleHookBreaker Hook;

    void Start()
    {
        FindCamera();
    }

    public void Break()
    {
        brokenPartVisual.SetActive(false);
        breakEffect.SetActive(true);
        m_AudioSource.PlayOneShot(breakSound);
        Hook.gameObject.SetActive(true);
        StartCoroutine(ThrowerCoroutine(garbagePrefab, 0, 2.5f));
    }
    

    public override  void DoAction(int itemID)
    {
        if (itemID == needItemID)
        {
            if (gameObject.GetComponent<modulePipe>())
                gameObject.GetComponent<modulePipe>().OnFixPipe();
            else
            {
                breakManager.TurnAlarm(false);
            }
            brokenPartVisual.SetActive(true);
            breakEffect.SetActive(false);
            m_AudioSource.PlayOneShot(fixSound);
            Hook.ResetHook();
            Hook.gameObject.SetActive(false);
            
        }

      
        
        
    }


}
