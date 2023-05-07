using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class moduleHookOutside : commonMagneticPlace
{
    [SerializeField] private int SpawnItemID;

    [SerializeField] private GameObject m_Canvas;

    private SpriteRenderer[] visuals;
    // Start is called before the first frame update
    private void Start()
    {
        
        HH = GameObject.FindObjectOfType<HandsHolds>();
        SearchForVisuals();
        GDB = GameObject.Find("GDB").GetComponent<GameDataBase>();
        StM = GDB.gameObject.GetComponent<StorageManager>();
      
        Hooked = true;
       
        GameObject inst;
        inst = Instantiate(GDB.GetItemFromList(SpawnItemID), transform.position, transform.rotation, transform);
        visuals = inst.GetComponentsInChildren<SpriteRenderer>();
        hookedItem = inst.transform.GetChild(0).gameObject;
        
      
        inst.GetComponent<Rigidbody2D>().simulated = false;
        inst.GetComponent<Collider2D>().enabled = false;
        foreach (var img in visuals)
        {
            img.sortingOrder = -46;
        }
        
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

            if (Hooked &&  gameObject.transform.childCount > 0 && HH.ItemNum() == -1)
            {
                
                o_MagneticItem = hookedItem.GetComponentInChildren<MagneticItem>();
                Picked = false;
                o_MagneticItem.SetCarryManager(o_CManager);
                o_MagneticItem.StartPick();
                Hooked = false;
                m_Canvas.SetActive(false);
                foreach (var img in visuals)
                {
                    img.sortingOrder = 0;
                }
            }

        
    }

    void FixedUpdate()
    {
        base.OnFixedUpdate();
    }
    // Update is called once per frame

}
