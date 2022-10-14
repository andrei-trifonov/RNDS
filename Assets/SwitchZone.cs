using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchZone : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;
    private GameObject Player;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.gameObject;
        }
    }
    public void GoToAnotherZone()
    {
        Player.transform.SetParent(null);
        Player.transform.position = teleportPoint.position;
    }
}
