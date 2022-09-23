using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonMagneticPlace : commonOutliner
{
    [SerializeField] protected string hookName;
    [SerializeField] protected float Speed = 8.5f;
    protected bool Hooked;
    protected GameObject Player;
    protected CarryManager o_CManager;
    protected int itemID;
    protected GameDataBase GDB;
    protected StorageManager StM;
    protected MagneticItem o_MagneticItem;
    public GameObject hookedItem;
    protected GameObject Item;
    protected Vector3 targetPosition;
    protected bool Picked = false;
    protected Vector3 currentPosition;
    protected Vector3 directionOfTravel;       

    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        SearchForVisuals();
        GDB = GameObject.Find("GDB").GetComponent<GameDataBase>();
        StM = GDB.gameObject.GetComponent<StorageManager>();
        if (PlayerPrefs.GetInt(hookName)> 10)
        {
            Hooked = true;
           
            GameObject inst;
            inst = Instantiate(GDB.GetItemFromList(PlayerPrefs.GetInt(hookName)), transform.position, transform.rotation, transform);
            hookedItem = inst.transform.GetChild(0).gameObject;
            StM.AddItem(hookedItem, this);
            inst.GetComponent<Rigidbody2D>().simulated = false;
            inst.GetComponent<Collider2D>().enabled = false;
        }
    }
    public GameDataBase GetGDB()
    {
        return GDB;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnableOutline();
            Player = other.gameObject;
            o_CManager = Player.GetComponent<CarryManager>();
            
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
        itemID = hookedItem.GetComponent<itemDB>().GetItemID();
        PlayerPrefs.SetInt(hookName, itemID);
    }
    

    protected void RemoveFromItemDB()
    {
        PlayerPrefs.SetInt(hookName, 0);
    }

    public void ResetHook()
    {
        Hooked = false;
        hookedItem.transform.parent.gameObject.SetActive(false);
    }

    public void StealItem()
    {
        Picked = false;
        Hooked = false;
        hookedItem.transform.parent.gameObject.SetActive(false);
        RemoveFromItemDB();
    } 
    public virtual void OnClick()
    {
        if (!Hooked  && o_CManager.isPicked())
        {
            hookedItem = o_CManager.GetPickedItem().transform.GetChild(0).gameObject;
            o_MagneticItem = hookedItem.GetComponent<MagneticItem>();
            if (o_MagneticItem.GetStashable())
            {
                Item = hookedItem.transform.parent.gameObject;
                Item.transform.rotation = transform.rotation;
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
            if (Hooked &&  gameObject.transform.childCount > 0 && !o_CManager.isPicked())
            {
                
                o_MagneticItem = hookedItem.GetComponent<MagneticItem>();
                Picked = false;
                o_MagneticItem.SetCarryManager(o_CManager);
                o_MagneticItem.StartPick();
                RemoveFromItemDB();
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
            currentPosition = Item.transform.position;


            if(Vector3.Distance(currentPosition, targetPosition) > .1f) {
                directionOfTravel = targetPosition - currentPosition;
                //now normalize the direction, since we only want the direction information
                directionOfTravel.Normalize();
                //scale the movement on each axis by the directionOfTravel vector components
            }
            else {
     
                Item.transform.SetParent(gameObject.transform);
                Item.GetComponentInChildren<MagneticItem>().Connect();
                Picked = false;
            }
            
            Item.transform.Translate(
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
