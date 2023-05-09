using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] private Text CrueText;
    [SerializeField] private Text HooksText;
    [SerializeField] private Text ScrapText;

    [SerializeField] private int hooksCount;
    [SerializeField] private List<StorageStruct> Storage;
    private int scrapCount;

    private int itemsCount;


    public void RenewStatus()
    {
        CrueText.text = PlayerPrefs.GetInt("CrueCount").ToString();
        ScrapText.text = scrapCount.ToString();
        HooksText.text = itemsCount.ToString() + "/" + hooksCount.ToString();
    }

    private void Start()
    {
        itemsCount = Storage.Count();
    }
    public void AddItem(GameObject Item, commonMagneticPlace Hook)
    {
        StorageStruct SS = new StorageStruct(Item, Hook , false);
        if (Item.GetComponentInChildren<itemScrap>().GetScrapPotential() == 0)
        {
            scrapCount++;
            SS.isScrap = true;
        }
        
        Storage.Add(SS);
        itemsCount = Storage.Count();

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
                itemsCount = Storage.Count();
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
