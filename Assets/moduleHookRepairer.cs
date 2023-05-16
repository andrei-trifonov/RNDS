using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class moduleHookRepairer : commonMagneticPlace
{
  
    [SerializeField] private GameObject repairManager;
    private bool itemRepaired;
    private moduleRepairer o_moduleRepairer;
    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     base.OnTriggerEnter2D(other);   
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);   
    }
    protected override void OnStart()
    {
        o_CManager = FindObjectOfType<CarryManager>();
        HH = GameObject.FindObjectOfType<HandsHolds>();
        o_moduleRepairer = repairManager.GetComponent<moduleRepairer>();
        SearchForVisuals();
        GDB = GameObject.Find("GDB").GetComponent<GameDataBase>();
    }

    void FixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnClick()
    {

        if (!Hooked  && HH.ItemNum() >= 0)
        {
            
            hookedItem = HH.Item();

            hookedItem.transform.rotation = transform.rotation;
            itemID = hookedItem.GetComponentInChildren<itemDB>().GetItemID();
            o_CManager.ThrowItem();
            hookedItem.GetComponentInChildren<MagneticItem>().Connect();
            Picked = true;
            Hooked = true;

            if (itemID <= 10)
            {
                itemRepair iR;
                if (iR = hookedItem.GetComponentInChildren<itemRepair>())
                {
                    itemRepaired = iR.GetRepaired();
                    if (!itemRepaired)
                        repairManager.GetComponent<commonThrower>().DoAction(itemID);
                    
                }
            }
            else
            {
                itemScrap iS;
                if (iS = hookedItem.GetComponentInChildren<itemScrap>())
                {
                    int scrapCount = iS.GetScrapPotential();
                    if (scrapCount > 0)
                    {
                        o_moduleRepairer.DoAction(scrapCount, 0);
                    }
                }
            }
        }
        else
        {
            if (Hooked && gameObject.transform.childCount > 0 && HH.ItemNum() == -1)
            {
                o_MagneticItem = hookedItem.GetComponentInChildren<MagneticItem>();
                Picked = false;
                o_MagneticItem.SetCarryManager(o_CManager);
                o_MagneticItem.StartPick();
                Hooked = false;
            }

        }
    }

    // Update is called once per frame

}
