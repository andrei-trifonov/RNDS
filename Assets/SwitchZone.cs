using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchZone : MonoBehaviour
{
    [SerializeField] private bool turnAnchor;
    [SerializeField] private Transform teleportPoint;
    private GameObject Player;
    private GameObject Tint;
    private void Start()
    {
        Tint = GameObject.FindGameObjectWithTag("BGTint");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.gameObject;
        }
    }
    public void GoToAnotherZone()
    {
        Tint.SetActive(turnAnchor);
        Player.GetComponentInChildren<Platformer2DUserControl>().useBeacon = turnAnchor;
        Player.transform.SetParent(null);
        Player.transform.position =  new Vector3 (teleportPoint.position.x, teleportPoint.position.y, Player.transform.position.z );
    }
}
