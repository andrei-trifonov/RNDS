using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class moduleHookTank : commonMagneticPlace
{
  
    [SerializeField] private GameObject waterTank;
    private bool hookBreakerModule;
    private float waterInside;
    private moduleWaterTank o_moduleWaterTank;
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
        HH = GameObject.FindObjectOfType<HandsHolds>();
       o_moduleWaterTank = waterTank.GetComponent<moduleWaterTank>();
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

            if (hookedItem.GetComponentInChildren<itemBucket>())
            {
                waterInside = hookedItem.GetComponentInChildren<itemBucket>().GetWater();
                waterTank.GetComponent<commonThrower>().DoAction(itemID, waterInside);
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
