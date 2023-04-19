using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class moduleBase : MonoBehaviour
{
   
    // Start is called before the first frame update
    public void AddFuel()
    {
        GetComponent<moduleFurnance>().AddFuel();
    }
    public void StartEngine()
    {
        GetComponent<moduleFurnance>().StartEngine();
    }
    public void StopEngine()
    {
        GetComponent<moduleFurnance>().StopEngine();
    }
    public void GivePost()
    {
        GetComponent<modulePostamat>().DoAction(0);
    }   
    public void OpenCloseWindow()
    {
        GetComponent<moduleWindow>().OpenClose();
    }   
    public void FixPipe()
    {
        GetComponent<modulePipe>().FixPipe();
    }   
    public void FixBreach()
    {
        GetComponent<BreakManager>().OnAccidentFix();
    }  
    public void CallElevator0()
    {
        GetComponent<Elevator>().GetHere(0);
    } 
    public void CallElevator0P()
    {
        GetComponent<Elevator>().GetHere(0);
        GetComponent<puzzleElevator>().MoveLiftDown();
    } 
    public void CallElevator1()
    {
        GetComponent<Elevator>().GetHere(1);
    }
    public void CallElevator2()
    {
        GetComponent<Elevator>().GetHere(2);
    }
    public void GoToAnotherZone()
    {
        GetComponent<SwitchZone>().GoToAnotherZone();
    }
    public void FireCannon()
    {
        GetComponent<moduleCannon>().FireCannon();
    }
    public void Grab()
    {
        GetComponent<moduleBigHook>().Grab();
    }
    
    public void Scan()
    {
        GetComponent<WorldArchive>().Scan();
    }
    public void OpenBridge()
    {
        GetComponent<puzzleBridge>().Open();
    }
    public void Save()
    {
        GetComponent<GameDataBase>().Save();
    }
}
