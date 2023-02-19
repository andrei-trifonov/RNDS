using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class moduleHookPuzzle : commonMagneticPlace
{
    [SerializeField] private puzzleElevator pE;

    private Animator Elevator;
    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
        Elevator = pE.GetElevator();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);   
    }
    private void OnTriggerExit2D(Collider2D other)
    { 
        base.OnTriggerExit2D(other);   
    }
    

    public  void OnClick()
    {
        if (!Elevator.GetComponent<Elevator>().GetStateB())
        {
            if (!Hooked && o_CManager.isPicked())
            {
                hookedItem = o_CManager.GetPickedItem().transform.GetChild(0).gameObject;
                o_MagneticItem = hookedItem.GetComponent<MagneticItem>();
                if (o_MagneticItem.GetStashable())
                {
                    Item = hookedItem.transform.parent.gameObject;
                    Item.transform.rotation = transform.rotation;
                    o_CManager.ThrowItem();
                    o_MagneticItem.Connect();
                    Picked = true;
                    Hooked = true;
                    pE.AddWeight(o_MagneticItem.gameObject.GetComponent<itemWeight>().GetWeight());
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
                    pE.RemoveWeight(o_MagneticItem.gameObject.GetComponent<itemWeight>().GetWeight());
                }

            }
        }
    }

    void FixedUpdate()
    {
        base.OnFixedUpdate();
    }
    // Update is called once per frame

}
