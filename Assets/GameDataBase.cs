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

    public ArchiveCell(string Label, string Text, Sprite Image)
    {
        this.Label = Label;
        this.Text = Text;
        this.Image = Image;
    }
}
public class GameDataBase : MonoBehaviour
{
    [SerializeField] private GameObject[] hookItemList;
    [SerializeField] public List<ArchiveCell> archiveCells;
    // Start is called before the first frame update

    private void Start()
    {
        RenewArchiveCells();
    }

    private void RenewArchiveCells()
    {
        archiveCells.Clear();
        foreach (var item in hookItemList)
        {
            itemLore lore = item.transform.GetChild(0).GetComponent<itemLore>();
            try
            {
                if (PlayerPrefs.GetInt(item.name + " Scan") == 1)
                {
                    archiveCells.Add(new ArchiveCell(lore.GetLabel(), lore.GetText(), 
                        item.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite));
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

    public List<ArchiveCell> GetArchiveCells()
    {
        RenewArchiveCells();
        return archiveCells;
    }

}
