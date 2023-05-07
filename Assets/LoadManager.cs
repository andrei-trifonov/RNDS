using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private GameObject Machine;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform[] MachinePlaces;
    [SerializeField] private Transform[] PlayerPlaces;
    [SerializeField] private upgradeManager uM;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("Save"));
        Vector3 ppos = PlayerPlaces[Int32.Parse(PlayerPrefs.GetString("Save")) % 10].position;
        Vector3 mpos = MachinePlaces[Int32.Parse(PlayerPrefs.GetString("Save")) % 10].position;
        Player.transform.position = new Vector3(ppos.x, ppos.y, Player.transform.position.z);
        Machine.transform.position = new Vector3(mpos.x, mpos.y, Machine.transform.position.z);
        switch(PlayerPrefs.GetString("Save"))
        {

            case "01":
            {
                uM.Upgrade(1);
                uM.Upgrade(5);
                uM.Upgrade(0);
                if (PlayerPrefs.GetInt("Upgrade4") == 1)
                {
                    uM.Upgrade(4);
                }

                break;
        }
        }
    }

}
