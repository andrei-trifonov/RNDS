using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoSave : MonoBehaviour
{
    [SerializeField] private string i;
    [SerializeField] private GameDataBase GDB;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (PlayerPrefs.GetString("Save") == i)
        {
            Destroy(gameObject);
        }
            if (col.CompareTag("Player"))
        {
            PlayerPrefs.SetString("Zone", i);
            GDB.Save();
            Destroy(this);
        }

    }
}
