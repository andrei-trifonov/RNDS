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
    public void CallElevator1()
    {
        GetComponent<Elevator>().GetHere(1);
    }
    public void CallElevator2()
    {
        GetComponent<Elevator>().GetHere(2);
    }  
}
