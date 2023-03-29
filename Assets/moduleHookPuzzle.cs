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
            if (!Hooked && HH.ItemNum() >= 0)
            {
                hookedItem = HH.Item();
                o_MagneticItem = hookedItem.GetComponentInChildren<MagneticItem>();
                if (o_MagneticItem.GetStashable())
                {

                    hookedItem.transform.rotation = transform.rotation;
                    o_CManager.ThrowItem();
                    o_MagneticItem.Connect();
                    Picked = true;
                    Hooked = true;
                    pE.AddWeight(hookedItem.GetComponentInChildren<itemWeight>().GetWeight());
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
                    pE.RemoveWeight(o_MagneticItem.gameObject.GetComponentInChildren<itemWeight>().GetWeight());
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
