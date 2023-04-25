using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemActivator : MonoBehaviour
{
    [SerializeField] private GameObject Thing;
    [SerializeField] private float Time = 1;
    // Start is called before the first frame update
    private bool isActive;

    public void Play()
    {
        if (!isActive)
            StartCoroutine(PlayCoroutine());
        
    }
    IEnumerator PlayCoroutine()
    {
        isActive = true;
        Thing.SetActive(true);
        yield return new WaitForSeconds(Time);
        Thing.SetActive(false);
        isActive = false;
    }
}
