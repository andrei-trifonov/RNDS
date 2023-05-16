using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class commonMagneticPlace : commonOutliner
{
    [SerializeField] protected string hookName;
    [SerializeField] protected float Speed = 8.5f;
    protected bool Hooked;
    
    protected CarryManager o_CManager;
    protected int itemID;
    protected GameDataBase GDB;
    protected StorageManager StM;
    protected MagneticItem o_MagneticItem;
    public GameObject hookedItem;
    protected Vector3 targetPosition;
    protected bool Picked = false;
    protected Vector3 currentPosition;
    protected Vector3 directionOfTravel;
    protected HandsHolds HH;

    protected PanZoom Camera;
    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
        
    }

    protected virtual void OnStart()
    {
      
        o_CManager = FindObjectOfType<CarryManager>();
        HH = GameObject.FindObjectOfType<HandsHolds>();
        SearchForVisuals();
        GDB = GameObject.Find("GDB").GetComponent<GameDataBase>();
        StM = GDB.gameObject.GetComponent<StorageManager>();
        if (PlayerPrefs.GetInt(hookName)> 10)
        {   
            Camera = FindObjectOfType<PanZoom>();
            Hooked = true;
           
            GameObject inst;
            itemID = PlayerPrefs.GetInt(hookName);
            inst = Instantiate(GDB.GetItemFromList(itemID), transform.position, transform.rotation, transform);
            hookedItem = inst.transform.GetChild(0).gameObject;
         
            StM.AddItem(hookedItem, this);
            inst.GetComponent<Rigidbody2D>().simulated = false;
            inst.GetComponent<Collider2D>().enabled = false;
            Camera.Canvases.Add(inst.GetComponentInChildren<Canvas>().gameObject);
        }
    }
    public GameDataBase GetGDB()
    {
        return GDB;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(hookName, itemID);
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnableOutline();
            
            
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisableOutline();
        }
    }

    protected virtual void AddToItemDB(GameObject hookedItem)
    {
        itemID = hookedItem.GetComponentInChildren<itemDB>().GetItemID(); 
    }
    

  

    public void ResetHook()
    {
        Hooked = false;
        hookedItem.SetActive(false);
    }

    public void StealItem()
    {
        Picked = false;
        Hooked = false;
        hookedItem.SetActive(false);
        itemID = -1;
    } 
    public virtual void OnClick()
    {
        if (!Hooked  && HH.ItemNum() >= 0)
        {
            hookedItem = HH.Item();
            o_MagneticItem = hookedItem.GetComponentInChildren<MagneticItem>();
            if (o_MagneticItem.GetStashable())
            {
                
                hookedItem.transform.rotation = transform.rotation;
                AddToItemDB(hookedItem);
                StM.AddItem(hookedItem, this);
                o_CManager.ThrowItem();
                o_MagneticItem.Connect();
                Picked = true;
                Hooked = true;
            }
        }
        else
        {
            if (Hooked &&  gameObject.transform.childCount > 0 && HH.ItemNum() == -1)
            {
                
                o_MagneticItem = hookedItem.GetComponentInChildren<MagneticItem>();
                Picked = false;
                o_MagneticItem.SetCarryManager(o_CManager);
                o_MagneticItem.StartPick();
                itemID = -1;
                StM.RemoveItem(this);
                Hooked = false;
            }

        }
    }

    public void OnFixedUpdate()
    {
        // Для подвеса скорость нужна ниже (для визуала)
        if (Picked) {

            targetPosition = gameObject.transform.position;
            currentPosition = hookedItem.transform.position;


            if(Vector3.Distance(currentPosition, targetPosition) > .1f) {
                directionOfTravel = targetPosition - currentPosition;
                //now normalize the direction, since we only want the direction information
                directionOfTravel.Normalize();
                //scale the movement on each axis by the directionOfTravel vector components
            }
            else {

                hookedItem.transform.SetParent(gameObject.transform);
                hookedItem.GetComponentInChildren<MagneticItem>().Connect();
                Picked = false;
            }

            hookedItem.transform.Translate(
                (directionOfTravel.x * Speed * Time.deltaTime),
                (directionOfTravel.y * Speed * Time.deltaTime),
                (directionOfTravel.z * Speed * Time.deltaTime),
                Space.World);

        }
    }
    void FixedUpdate()
    {
       OnFixedUpdate();
    }
    

}
