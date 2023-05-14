using System;
using System.Collections;
using System.Collections.Generic;
using AdultLink;
using UnityEngine;

public class Shadow2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_Renderer;
    [SerializeField] private SpriteRenderer eveningShadow;
    [SerializeField] private SpriteRenderer morningShadow;
    [SerializeField] private SpriteRenderer dayShadow;
    [SerializeField] private GameObject Shadow;
    [SerializeField] private Transform Pivot;
    [SerializeField] private float maxShadowCast;
    [SerializeField] private LayerMask lMask;
    private bool flipped;
    private bool inside;
    private bool bestQuality;
    private int backupState;
    private Transform eS;
    private Transform mS;
    private Transform dS;
    public void SetShadow(int i)
    {

        eveningShadow.enabled = false;
        dayShadow.enabled = false;
        morningShadow.enabled = false;
        if (i != -1)
            backupState = i;
        else
        {
            inside = !inside;
        }
        if (!inside )
            switch (i)
            {
                case -1:
                {
                    if (!inside)
                    {
                       SetShadow(backupState);
                    }
                }
                    break;
                case 0: dayShadow.enabled = true; break; 
                case 1: eveningShadow.enabled = true; break; 
                case 3: morningShadow.enabled = true; break;
            }
    }
    private void Start()
    {
        eS = eveningShadow.transform.parent.transform;
        dS = dayShadow.transform.parent.transform;
        mS = morningShadow.transform.parent.transform;
    Debug.Log(PlayerPrefs.GetInt("Quality") );
        eveningShadow.enabled = false;
        dayShadow.enabled = false;
        morningShadow.enabled = false;
        if (PlayerPrefs.GetInt("Quality") == 2)
            this.enabled = false;
    }

    void PivotFlip()
    {
       
        if (Pivot.localScale.x>0)
        {
            eS.transform.localPosition = new Vector3(-0.45f, eS.transform.localPosition.y, eS.transform.localPosition.z);
               dS.transform.localPosition = new Vector3(0.73f, 0.09f, dS.transform.localPosition.z);
          mS.transform.localPosition = new Vector3(0.62f, -0.01f, mS.transform.localPosition.z);
            if (backupState == 0 || backupState == 3)
                flipped = false;
            else
                flipped = true;
        }
        else
        {
            eS.transform.localPosition = new Vector3(-0.83f, eS.transform.localPosition.y, eS.transform.localPosition.z);
         dS.transform.localPosition = new Vector3(0.08f, 0.09f, dS.transform.localPosition.z);
                 mS.transform.localPosition = new Vector3(-0.15f,0.28f , mS.transform.localPosition.z);
            if (backupState == 0 || backupState == 3)
                flipped = true;
            else
                flipped = false;
        }
    }
    private void FixedUpdate()
    {
  
            try
            {
                PivotFlip();
                eveningShadow.sprite = m_Renderer.sprite;
                morningShadow.sprite = m_Renderer.sprite;
                dayShadow.sprite = m_Renderer.sprite;
                dayShadow.flipX = flipped;
                eveningShadow.flipX = flipped;
                morningShadow.flipX = flipped;
                Shadow.SetActive(false);
                Ray2D ray = new Ray2D(gameObject.transform.position, Vector2.down);
              

                RaycastHit2D hit = (Physics2D.Raycast(gameObject.transform.position, ray.direction, maxShadowCast, lMask));
                Debug.DrawRay(gameObject.transform.position, ray.direction);
                if (hit.collider != null)
                {

                    Shadow.transform.position = hit.point;
                    Shadow.SetActive(true) ;
                    Vector2 normal = hit.normal;

                    // поворачиваем объект на 90 градусов вокруг оси, указанной нормалью

                    // Преобразуем направление в угол поворота
                    float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;

                    // Устанавливаем поворот объекта в направлении целевой позиции
                    Shadow.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                }
            

            }
            catch { }
        
    }
}
