using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class outerBirdsController : MonoBehaviour
{
    
    [SerializeField] private GameObject landingPointsGroup;
    [SerializeField] private GameObject birdsGroup;
    [SerializeField] private GameObject Leader;
    [SerializeField] private Vector2 Borders;
    private List<outerBirds> Birds;
    private float Timer;
    private List<GameObject> tempPoints;
    private float goalTime;
    private bool Blocked;
    private bool playerStaying;
    private bool onPlace;
    
    
    public static List<GameObject> GetChildren(Transform parent)
    {
        List<GameObject> ret = new List<GameObject>();
        foreach (Transform child in parent) ret.Add(child.gameObject);
        return ret;
    }

    public void SetTimeBounds(float first, float second)
    {
        Borders.x = first;
        Borders.y = second;
    }
    private List<outerBirds> GetBirds(Transform parent)
    {
        List<outerBirds> ret = new List<outerBirds>();
        foreach (Transform child in parent) ret.Add(child.GetComponent<outerBirds>());
        return ret;
    }
    private void ActiveBirds( [CanBeNull] List<outerBirds> Birds, bool state)
    {
        foreach (outerBirds bird in Birds)
        {
            bird.gameObject.SetActive(state);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            playerStaying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Blocked)
        {
            playerStaying = true;
            StartCoroutine(TakeOffCoroutine());
        }
    }

    private void FixedUpdate()
    {
        if (!Blocked)
        {
            if (Timer < goalTime)
                Timer += Time.deltaTime;
            else
            {

                Timer = 0;
                StartCoroutine(LandCoroutine());
            }
        }
    }

    IEnumerator LandCoroutine()
    {
        if (!playerStaying)
        {
            Blocked = true;
            int r = Random.Range(1, 3);
            TakeOff();
            goalTime = Random.Range(Borders.x, Borders.y);
            ActiveBirds(Birds, true);
            Leader.GetComponent<Animator>().SetBool("L" + r, true);
            yield return new WaitForSeconds(3f);
            Leader.GetComponent<Animator>().SetBool("L" + r, false);
            Land();
        }
    }
    IEnumerator TakeOffCoroutine()
    {
        if (onPlace)
        {
            int r = Random.Range(1, 3);
            Leader.GetComponent<Animator>().SetBool("T" + r, true);
            TakeOff();
            yield return new WaitForSeconds(4f);
            ActiveBirds(Birds, false);
            Leader.GetComponent<Animator>().SetBool("T" + r, false);
            Blocked = false;
            onPlace = false;
        }
    }
    private void Start()
    {
        Birds = GetBirds(birdsGroup.transform);
        ActiveBirds(Birds, false);
        goalTime = Random.Range(Borders.x, Borders.y);
    }

    void TakeOff()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].ResetTarget();
        }
    }
        
    void Land()
    {
        tempPoints = GetChildren(landingPointsGroup.transform);
        for (int i = 0; i < Birds.Count; i++)
        {
            int j = Random.Range(0, tempPoints.Count);
            Birds[i].SetNewTarget(tempPoints[j]);
            tempPoints.RemoveAt(j);
        }

        onPlace = true;
    }
    
    
}
