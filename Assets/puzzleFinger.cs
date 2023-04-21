using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleFinger : MonoBehaviour
{
    public PlatformerCharacter2D PC2D;
    public Transform CameraPoint;
    public bool Laser1;
    public bool Laser2;
    public GameObject[] finishPuzzle;
    public GameObject[] itemsPuzzle;
    public PanZoom Camera;
    private Vector3 campos;

    private void FixedUpdate() 
    {

        if (Laser1 && Laser2)
        {

            EndPuzzle();
        }
        Laser1 = false;
        Laser2 = false;
    }
    private void EndPuzzle()
    {
        StartCoroutine(EndPuzzleCoroutine());
      
    }
    IEnumerator EndPuzzleCoroutine()
    {

        campos = Camera.gameObject.transform.position;
        Camera.MoveToPosition(CameraPoint.position);
        Camera.ChangeZoom(19);
        PC2D.Block();
        yield return new WaitForSeconds(2);
        foreach (GameObject obj in finishPuzzle)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in itemsPuzzle)
        {
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(3);
        Camera.MoveToPosition(campos);
        Camera.ChangeZoom(2.6f);
        yield return new WaitForSeconds(4);
        PC2D.UnBlock();
       
        Destroy(gameObject);
    }
}
