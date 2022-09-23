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

        if (!Hooked  && o_CManager.isPicked())
        {
            
            hookedItem = o_CManager.GetPickedItem().transform.GetChild(0).gameObject;
            Item = hookedItem.transform.parent.gameObject;
            Item.transform.rotation = transform.rotation;
            itemID = hookedItem.GetComponent<itemDB>().GetItemID();
            o_CManager.ThrowItem();
            Item.GetComponentInChildren<MagneticItem>().Connect();
            Picked = true;
            Hooked = true;

            if (hookedItem.GetComponent<itemBucket>())
            {
                waterInside = hookedItem.GetComponent<itemBucket>().GetWater();
                waterTank.GetComponent<commonThrower>().DoAction(itemID, waterInside);
            }
        }
        else
        {
            if (Hooked && gameObject.transform.childCount > 0 && !o_CManager.isPicked())
            {
                o_MagneticItem = hookedItem.GetComponent<MagneticItem>();
                Picked = false;
                o_MagneticItem.SetCarryManager(o_CManager);
                o_MagneticItem.StartPick();
                Hooked = false;
            }

        }
    }

    // Update is called once per frame

}
