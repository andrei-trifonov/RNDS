using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanZoom : MonoBehaviour {
    private bool Auto;
    [SerializeField] private float zoomSpeed = 5f; // �������� ��������� ����
    Vector3 touchStart;
    [SerializeField] private Camera lightCamera;
    [SerializeField] private float zoomOutMin = 1;
    [SerializeField] private float zoomOutMax = 8;
    public float minLight;
    public float maxLight;
    public GameObject LightTexture;
    [SerializeField] private  float canvasSizeMax;
    //[SerializeField] private  float alphaModifier;
    [SerializeField] private  float alphaBegin;
    [SerializeField] private  float alphaEnd;
    public List<GameObject> Canvases;
    public List<GameObject> Sprites;
 
    private float delta;

    private Vector3 targetPosition; // ������� ������� ������
    private bool isMoving = false; // ���� �������� ������

    public void ChangeZoom(float targetZoom)
    {
        Auto = true;
        targetZoom = Mathf.Clamp(targetZoom, zoomOutMin, zoomOutMax); // ����������� �������� ���� � ��������� [minZoom, maxZoom]
        StartCoroutine(SmoothZoom(targetZoom)); // ������ �������� ��� �������� ��������� ����
    }

    void UpdateZoomObj()
    {
        float value;
        value = Mathf.Clamp(Camera.main.orthographicSize / zoomOutMax * canvasSizeMax, 1, canvasSizeMax);
        for (int i = 0; i < Canvases.Count; i++)
            try
            {
                Canvases[i].gameObject.transform.localScale = new Vector3(value, value, 1);
            }
            catch
            {

            }
        value = (alphaEnd) * ((Camera.main.orthographicSize - alphaBegin) / zoomOutMax);

        if (Camera.main.orthographicSize <= alphaBegin)
            value = 0;
        Color color;

        for (int i = 0; i < Sprites.Count; i++)
        {
            color = Sprites[i].GetComponent<SpriteRenderer>().color;
            color.a = value;
            Sprites[i].GetComponent<SpriteRenderer>().color = color;

        }
        value = Mathf.Clamp(Camera.main.orthographicSize / zoomOutMax * maxLight, 1, maxLight);
        LightTexture.transform.localScale = new Vector3(value, value, value);
    }
    private IEnumerator SmoothZoom(float targetZoom)
    {
        while (Mathf.Abs(Camera.main.orthographicSize - targetZoom) > 0.01f) // ���� �������� ���� �� ����������
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed); // ������� ��������� ����
            UpdateZoomObj();
            
            
            yield return null; // ������� ���������� ������
            
            
        }
        Auto = false;
    }
    // Update is called once per frame
    private void Start()
    {
        Canvases.AddRange( GameObject.FindGameObjectsWithTag("ScaleCanvas").ToList());
        Sprites = GameObject.FindGameObjectsWithTag("AlphaCanvas").ToList();
    }

    void FixedUpdate ()
    {
        if (!Auto)
        {
            UpdateZoomObj();
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;




                zoom(difference * 0.01f);
            }
            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        if (isMoving)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            // ��������� ���������� �� ������� �������
            delta += 0.07f;
            // ���� ���������� ������ ������������� ��������, ������������� ��������
            // ��������� ����� ������� ������
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, delta);
            transform.position = newPosition;
            if (delta >= 1)
                isMoving = false;
        }
        

    }

    public void zoom(float increment){
        
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        lightCamera.orthographicSize = Camera.main.orthographicSize;
        
    }

    public void MoveToPosition(Vector3 newPosition)
    {
       
        delta = 0;
        targetPosition = newPosition;
        isMoving = true;
     
    }
}