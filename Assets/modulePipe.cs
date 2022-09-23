using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modulePipe : MonoBehaviour
{
    [SerializeField] private GameObject pipeBroken;
    [SerializeField] private GameObject pipeOnFix;
    [SerializeField] private GameObject pipeFixed;
    [SerializeField] private HealBar bridgeAZBar;
    [SerializeField] private List <GameObject> Canvases;
    [SerializeField] private ShipMove Engine;
    [SerializeField] private PanZoom Camera;
    [SerializeField] private BreakManager breakManager;
    private bool canvasesInitialized;

    public void BreakPipe()
    {
        if (!canvasesInitialized)
        {   
            Camera.Canvases.Add(Canvases[0]);
            Camera.Canvases.Add(Canvases[1]);
            canvasesInitialized = true;
        }
        Engine.SetPipeState(true);
        pipeBroken.SetActive(true);
        pipeFixed.SetActive(false);
    }
    public void OnFixPipe()
    {
        
        bridgeAZBar.SetStateStart();
        pipeBroken.SetActive(false);
        pipeOnFix.SetActive(true);
    }
    // Start is called before the first frame update
    public void FixPipe()
    {
        breakManager.TurnAlarm(false);
        Engine.SetPipeState(false);
        pipeOnFix.SetActive(false);
        pipeFixed.SetActive(true);
       
    }
}
