using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class BreakManager : MonoBehaviour
{

    [FormerlySerializedAs("timerBounds")] [SerializeField] private float[] breakTimerBounds = new float[2];
    [SerializeField] private float[] leakTimerBounds = new float[2];
    [SerializeField] private moduleBreaker[] Breakers;
    [SerializeField] private GameObject[] Leaks;
    [SerializeField] private commonBar Bar;
    [SerializeField] private List<GameObject> breachList;
    [SerializeField] private StorageManager o_storageManager;
    private float bFinishTime;
    private float bCurrentTime = 0;
    private float lFinishTime;
    private float lCurrentTime = 0;
    private bool Alarm;
    private ShipMove Engine;
    private bool Accident;
    private bool Leak;
    private GameObject selectedBreach;
    [SerializeField] private bool DEBUG;
    [SerializeField] private int BreakElementNumber;
   
    // Start is called before the first frame update

    private void Start()
    {
        foreach (var breach in breachList)
        {
            breach.SetActive(false);
        }
        Engine = GetComponent<ShipMove>();
        lFinishTime = Random.Range(leakTimerBounds[0], leakTimerBounds[1]);
        bFinishTime = Random.Range(breakTimerBounds[0], breakTimerBounds[1]);
    }

    public void TurnAlarm(bool state)
    {
        Bar.FillReset();
        Alarm = state;
       
    }
    
    void RenewTimer()
    {
        int i = Random.Range(0, Breakers.Length);
        if (DEBUG)
            i = BreakElementNumber;
        
        Breakers[i].Break();
        if (Breakers[i].GetComponent<modulePipe>())
        {
            Breakers[i].gameObject.GetComponent<modulePipe>().BreakPipe();
        }
        bCurrentTime = 0;
        TurnAlarm(true);
        bFinishTime = Random.Range(breakTimerBounds[0], breakTimerBounds[1]);
    }
    void RenewTimerLeak()
    {
        int i = Random.Range(0, Leaks.Length);
        if (DEBUG)
            i = BreakElementNumber;
        Leaks[i].SetActive(true);
        lCurrentTime = 0;
        Leak = true;
        lFinishTime = Random.Range(leakTimerBounds[0], leakTimerBounds[1]);
    }

    public void LeakStopped()
    {
        Leak = false;
    }
    void OnAccident()
    {
        
        Accident = true;
        Engine.SetMachineBlocked(true);
        int rnd = Random.Range(0, breachList.Count);
        selectedBreach = breachList[rnd];
        selectedBreach.SetActive(true);

    }

    public void OnAccidentFix()
    {
        if (o_storageManager.GetScrap(3))
        {
            Accident = false;
            Engine.SetMachineBlocked(false);
            selectedBreach.SetActive(false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (lCurrentTime < lFinishTime && !Leak && !Alarm && !Accident && Engine.GetEngineState())
            lCurrentTime += Time.deltaTime;
        if (lCurrentTime >= lFinishTime && !Leak && !Alarm && !Accident && Engine.GetEngineState())
            RenewTimerLeak();
        if (bCurrentTime < bFinishTime && !Alarm && !Accident && Engine.GetEngineState())
            bCurrentTime += Time.deltaTime;
        if (bCurrentTime >= bFinishTime && !Alarm && !Accident && Engine.GetEngineState())
            RenewTimer();
        if (Alarm)
        {
            Bar.FillStart();
        }

        if (Bar.isFinished() && !Accident)
        {
            OnAccident();
        }

    }
}
