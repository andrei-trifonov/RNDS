using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[System.Serializable]
public struct ArchiveCell
{
    
    public string Label;
    public string Text;
    public Sprite Image;
    public int num;
    public ArchiveCell(string Label, string Text, Sprite Image, int num)
    {
        this.Label = Label;
        this.Text = Text;
        this.Image = Image;
        this.num = num;
    }
}
public class GameDataBase : MonoBehaviour
{
    private bool isSaving;
    [SerializeField] private GameObject saveScreen;
    [SerializeField] private ShipMove Transport;
    [SerializeField] public GameObject[] hookItemList;
    [SerializeField] public List<ArchiveCell> archiveCells;
    [SerializeField] public List<commonMagneticPlace> savePlaces;
    // Start is called before the first frame update

    private void Start()
    {
        //RenewArchiveCells();
    }

    public int getSavePlaces()
    {
        return savePlaces.Count;
    }
    public void Save()
    {
        foreach(commonMagneticPlace hook in savePlaces)
        {
            hook.Save();
        }
        PlayerPrefs.SetString("Save", PlayerPrefs.GetString("Zone"));
        PlayerPrefs.SetFloat("Fuel", Transport.GetFuel());
        PlayerPrefs.SetFloat("Water", Transport.GetWater());
        if (!isSaving)
        {
            StartCoroutine(SaveCoroutine());
        }
    }
    IEnumerator SaveCoroutine()
    {
        isSaving = true;
        saveScreen.SetActive(true);
        yield return new WaitForSeconds(6);
        saveScreen.SetActive(false);
        isSaving = false;
    }
    private void RenewArchiveCells()
    {
        archiveCells.Clear();
        
        for (int i =0; i<  hookItemList.Length;i++)
        {
            try
            {
                itemLore lore = hookItemList[i].GetComponentInChildren<itemLore>();
           
                if (PlayerPrefs.GetInt(hookItemList[i].name + " Scan") == 1)
                {
                        archiveCells.Add(new ArchiveCell(lore.GetLabel(), lore.GetText(),
                   lore.GetSprite(), hookItemList[i].GetComponentInChildren<itemDB>().GetItemID()));
                }
            }
            catch (Exception e)
            {
                Debug.Log("Archive /add error" + e);
            }
        }
    }
    public GameObject GetItemFromList(int index)
    {
        return hookItemList[index];
    }

    public void ScanItem(int index)
    {
        GameObject item = GetItemFromList(index); 
        Debug.Log(item.name + " Scan");
        PlayerPrefs.SetInt(item.name + " Scan", 1);
       
    }
    public int archiveSize()
    {
        try
        {
            RenewArchiveCells();
        }
        catch { }
        return archiveCells.Count;
    }
    public int collectionSize()
    {

        return hookItemList.Length;
    }

    public List<ArchiveCell> GetArchiveCells()
    {
        RenewArchiveCells();
        return archiveCells;
    }

}
