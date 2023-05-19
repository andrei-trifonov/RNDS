using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WorldArchive : MonoBehaviour
{

    [SerializeField] private Text CollectionText;
    [SerializeField] private GameObject labelContent;
    [SerializeField] private GameObject labelTemplate;
    [SerializeField] private GameObject scanEffect;
    [SerializeField] private AudioClip scanSound;
    [SerializeField] private RectTransform ItemList;

    public List<ArchiveCell> archiveCells;
    public List<GameObject> ScanItems;
    private bool Blocked;
    private GameDataBase GDB;
    private GameObject ScanItem;
    private AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        GDB = GameObject.Find("GDB").GetComponent<GameDataBase>();
        CollectionText.text =  GDB.archiveSize().ToString() + "/" +  GDB.collectionSize().ToString() ;
     //   Renew();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (ScanItems.Contains(other.gameObject))
        {
            ScanItems.Remove(other.gameObject);
            if (ScanItems.Count>0)
                ScanItem = ScanItems.Last();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Item"))
        {
            if (other.transform.GetChild(0).GetComponent<itemLore>() && !ScanItems.Contains(other.gameObject))
            {
                ScanItems.Add(other.gameObject);
                ScanItem = ScanItems.Last();
            }
        }
     
    }

    IEnumerator ScanCoroutine()
    {
        Blocked = true;
        scanEffect.SetActive(true);
        m_AudioSource.PlayOneShot(scanSound);
        yield return new WaitForSeconds(1);
        scanEffect.SetActive(false);
        Blocked = false;
    }
    public void Scan()
    {
        if (ScanItem && ScanItems.Count > 0 && !Blocked)
        {
            AddToArchive(ScanItem.transform.GetChild(0).GetComponent<itemDB>().GetItemID());
            StartCoroutine(ScanCoroutine());
           
        }
        CollectionText.text = GDB.collectionSize().ToString() + "/" + GDB.archiveSize().ToString();
    }

    private void AddToArchive(int i)
    {
        GDB.ScanItem(i);
    }
    
    public void Renew()
    { 
        var children = new List<GameObject>();
        foreach (Transform child in labelContent.transform) if (child!=labelContent.transform && child!=labelTemplate.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        archiveCells =  GDB.GetArchiveCells();
        foreach (var item in archiveCells)
        {
            JournalScanLabel Label = Instantiate(labelTemplate, labelContent.transform).GetComponent<JournalScanLabel>();
            Label.gameObject.SetActive(true);
            Label.OnSpawn(item.Image, item.Label, item.Text, item.num);

        }
        children = new List<GameObject>();
        float height = 0;
        foreach (Transform child in labelContent.transform) if (child != labelContent.transform ) height += (child.GetComponent<RectTransform>().rect.height + 10);
        ItemList.sizeDelta = new Vector2(ItemList.sizeDelta.x, height);
        
        
    }
}
