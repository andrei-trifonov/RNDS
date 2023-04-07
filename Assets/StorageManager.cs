using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct StorageStruct
{
    public GameObject Item;
    public commonMagneticPlace Hook;
    public bool isScrap;
    public StorageStruct(GameObject Item, commonMagneticPlace Hook, bool isScrap)
    {
        this.Item = Item;
        this.Hook = Hook;
        this.isScrap = isScrap;
    }
}
public class StorageManager : MonoBehaviour
{
    [SerializeField] private List<StorageStruct> Storage;
    private int scrapCount;

    
    public void AddItem(GameObject Item, commonMagneticPlace Hook)
    {
        StorageStruct SS = new StorageStruct(Item, Hook , false);
        if (Item.GetComponentInChildren<itemScrap>().GetScrapPotential() == 0)
        {
            scrapCount++;
            SS.isScrap = true;
        }

        Storage.Add(SS);


    }
    public void RemoveItem(commonMagneticPlace Hook)
    {
        for (int i = 0; i < Storage.Count; i++)
        {
            if (Storage[i].Hook == Hook)
            {
                if (Storage[i].Item.GetComponentInChildren<itemScrap>().GetScrapPotential() == 0)
                {
                    scrapCount--;
                }

                Storage.RemoveAt(i);
                return;
            }
        }
    }

    public bool GetScrap(int needScrap)
    {
        if (scrapCount >= needScrap)
        {
            int tmpScrap=0;
            while (tmpScrap!=needScrap)
            {
                for (int i = 0; i < Storage.Count; i++)
                {
                    if (Storage[i].isScrap == true)
                    {
                        scrapCount--;
                        Storage[i].Hook.StealItem();
                        Storage.RemoveAt(i);
                        tmpScrap++;
                    }
                }
                
            }

            return true;
        }
        else
        {
            return false;
        }
    }
    
    
}
