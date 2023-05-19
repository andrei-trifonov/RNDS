using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class LoadManager : MonoBehaviour
{
    [SerializeField] private GameObject Machine;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform[] MachinePlaces;
    [SerializeField] private Transform[] PlayerPlaces;
    [SerializeField] private upgradeManager uM;
    // Start is called before the first frame update
    public void DebugCheat()
    {
        PlayerPrefs.SetString("Save", "01");
    }
    void Start()
    {
        Load();
    }

    public void CheatLoad(string name)
    {
        PlayerPrefs.SetString("Save", name);
        Load();
    }
    public void Load()
    {
        int  save = Int32.Parse(PlayerPrefs.GetString("Save"));
       
        Vector3 ppos = PlayerPlaces[save % 10].position;
        Vector3 mpos = MachinePlaces[save % 10].position;
        Player.transform.position = new Vector3(ppos.x, ppos.y, Player.transform.position.z);
        Machine.transform.position = new Vector3(mpos.x, mpos.y, Machine.transform.position.z);
        switch(Int32.Parse(PlayerPrefs.GetString("Save"))/10)
        {

            case 0:
            {
                if (save % 10 > 0 && save % 10 <= 2)
                {
                    GameObject Tutorial = GameObject.FindWithTag("ControlTutorial");
                    Tutorial.SetActive(false);
                    ASFade asfade = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<ASFade>();
                    asfade.Fade(1);
                    if (PlayerPrefs.GetInt("Upgrade0") == 1)
                    {
                        uM.Upgrade(0);
                    }
                    PlayerPrefs.SetInt("CrueCount", 1);
                    Player.GetComponent<Platformer2DUserControl>().useBeacon = true;
                    if (PlayerPrefs.GetInt("Upgrade4") == 1)
                    {
                        uM.Upgrade(4);
                    }
                }

                if (save % 10 > 2 && save <= 3)
                {
                    ASFade asfade = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<ASFade>();
                    asfade.Fade(1);
                    PlayerPrefs.SetInt("CrueCount", 1);
                    uM.Upgrade(1);
                    uM.Upgrade(5);
                    if (PlayerPrefs.GetInt("Upgrade0") == 1)
                    {
                        uM.Upgrade(0);
                    }
                    Player.GetComponent<Platformer2DUserControl>().useBeacon = true;
                    if (PlayerPrefs.GetInt("Upgrade4") == 1)
                    {
                        uM.Upgrade(4);
                    }
                }
               
                if (save % 10 > 3)
                {
                    ASFade asfade = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<ASFade>();
                    asfade.Fade(1);
                    PlayerPrefs.SetInt("CrueCount", 2);
                    uM.Upgrade(1);
                    uM.Upgrade(5);
                    if (PlayerPrefs.GetInt("Upgrade0") == 1)
                    {
                        uM.Upgrade(0);
                    }
                  
                    uM.Upgrade(6);
                    Player.GetComponent<Platformer2DUserControl>().useBeacon = true;
                    if (PlayerPrefs.GetInt("Upgrade4") == 1)
                    {
                        uM.Upgrade(4);
                    }
                }

                break;
            }
        }
    }
}
