using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Upgrade
{
    public List<GameObject> Objects;
}
public class upgradeManager : MonoBehaviour
{
   
    [SerializeField] private List<Upgrade> modulesUpgrade;
    private PanZoom panZoom; 

    private void Start()
    {
        panZoom = GameObject.FindObjectOfType<PanZoom>();
    }
    public void Upgrade(int i)
    {
        switch (i)
        {
            case 5:
                {
                    modulesUpgrade[5].Objects[0].GetComponent<BreakManager>().SetTimeBounds(30, 120);
                }
                break;
            case 4:
                {
                    modulesUpgrade[4].Objects[0].GetComponent<outerBirdsController>().SetTimeBounds (5, 120);
                }
                break;
            case 2:
                {
                    modulesUpgrade[2].Objects[0].SetActive(true);

                    Canvas[] Canvases = modulesUpgrade[2].Objects[0].GetComponentsInChildren<Canvas>();

                    foreach (Canvas canv in Canvases)
                    {
                        if (canv.renderMode == RenderMode.WorldSpace)
                            panZoom.Canvases.Add(canv.gameObject);
                    }
                    panZoom.Sprites.Add(modulesUpgrade[2].Objects[1]);
                    panZoom.Sprites.Add(modulesUpgrade[2].Objects[2]);
                }
                break;
            default:
            { 
                foreach(GameObject upg in modulesUpgrade[i].Objects)
                {
                    upg.SetActive(true);
                   
                    Canvas[] Canvases = upg.GetComponentsInChildren<Canvas>();
                    foreach (Canvas canv in Canvases)
                    {
                            if(canv.renderMode == RenderMode.WorldSpace)
                                panZoom.Canvases.Add(canv.gameObject);
                    }
                }
                  
            }
                break;
        
        }
    }
}
