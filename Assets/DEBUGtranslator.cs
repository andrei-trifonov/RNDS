using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUGtranslator : MonoBehaviour
{
    public Slider slider;
   
    private float newval;
    private GameDataBase GDB;
    private float newvalT;
    public PanZoom panzoom;
    public GameObject LabelContent;
    public GameObject LabelTemplate;

    public List<ArchiveCell> archiveCells;
    // Start is called before the first frame update
    void Start()
    {
        GDB = GameObject.Find("GDB").GetComponent<GameDataBase>();
        newval = slider.value;
    }

    // Update is called once per frame
    public void Translate()
    {
        panzoom.zoom(slider.value-newval);
        newval = slider.value;
    }

    public void ChangeTime(bool up)
    {
        if (up)
         Time.timeScale *=2;
        else
        {
            Time.timeScale /=2;
        }
    }

    public void Scan(int i)
    {
        GDB.ScanItem(i);
    }
    
    public void Renew()
    { 
        var children = new List<GameObject>();
        foreach (Transform child in LabelContent.transform) if (child!=LabelContent.transform && child!=LabelTemplate.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        archiveCells =  GDB.GetArchiveCells();
       foreach (var item in archiveCells)
       {
           JournalScanLabel Label = Instantiate(LabelTemplate, LabelContent.transform).GetComponent<JournalScanLabel>();
           Label.gameObject.SetActive(true);
           Label.OnSpawn(item.Image, item.Label, item.Text, item.num);
       }
    }
}
