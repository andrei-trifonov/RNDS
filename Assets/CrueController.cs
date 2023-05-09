using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CrueController : MonoBehaviour
{
    [SerializeField] float changeTime = 60;
    [SerializeField] Renderer[] Variants;
    [SerializeField] GameObject[] VariantsObj;
    private float changeTimer;
    private bool updating;
    private int lastPicked;
    private List<int> randomList = new List<int>();

    private void Start()
    {
        lastPicked = Random.Range(0, randomList.Count);
        VariantsObj[lastPicked].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeTimer < changeTime && !updating)
        {
            changeTimer += Time.deltaTime;
        }
        else
        {
            changeTimer = 0;
            updating = true;
            UpdateChar();
        }
        
    }
    void UpdateChar()
    {
        randomList.Clear();
        for(int i=0;i< Variants.Length;i++)
        {
            if (!Variants[i].isVisible)
            {
                randomList.Add(i);
            }
        }
        if (randomList.Count > 0 && !Variants[lastPicked].isVisible)
        {
            VariantsObj[lastPicked].SetActive(false);
            lastPicked = randomList[Random.Range(0, randomList.Count)];
            VariantsObj[lastPicked].SetActive(true);
            updating = false;
        }
    }
}
