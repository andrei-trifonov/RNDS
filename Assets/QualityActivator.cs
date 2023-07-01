using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> qualityBestObjects;
    [SerializeField] private List<GameObject> qualityMediumObjects;
    [SerializeField] private List<GameObject> qualityLowObjects;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Quality " +PlayerPrefs.GetInt("Quality")  );
        List<GameObject> tempArray = new List<GameObject>();
        switch (PlayerPrefs.GetInt("Quality") )
        {
                case 0: tempArray = qualityLowObjects; break;
                case 1: tempArray = qualityMediumObjects; break;
                case 2: tempArray = qualityBestObjects; break;
        }
       
     
        foreach (GameObject obj  in tempArray)
        {
            obj.SetActive(true);
        } 
    }


}
