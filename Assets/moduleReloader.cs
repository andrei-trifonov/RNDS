using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class moduleReloader : MonoBehaviour
{
    [SerializeField] private int reloadCellNum;
    [SerializeField] private moduleCannon Cannon;
    [SerializeField] private Transform Anchor;
    private GameObject Projectile;
    public List<GameObject> Projectiles;

    private void Start()
    {
        Projectile = null;
    }

    public GameObject GetProjectile()
    {
        return Projectile;
    }

    public Transform GetAnchor()
    {
        return Anchor;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.GetChild(0).GetComponent<itemFuel>() && !Projectiles.Contains(col.gameObject))
        {
            Projectiles.Add(col.gameObject);
            Projectile = Projectiles.Last();
        }
        
    }

    public bool isLoaded()
    {
        if (Projectile != null && Projectiles.Count>0)
            return true;
        return false;
    }
    public void Exchange(moduleReloader reloader)
    {
        if (reloadCellNum == 0)
        {
            Projectile.SetActive(false);
        }

        if(reloader.GetProjectile()!=null)
            reloader.GetProjectile().transform.position = Anchor.transform.position;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.GetChild(0).GetComponent<itemFuel>()  && Projectiles.Contains(col.gameObject))
        {
            Projectiles.Remove(col.gameObject);
            Projectile = Projectiles.Last();
        }
        
    }
}
