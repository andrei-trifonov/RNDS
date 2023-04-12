using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomStartVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Sprite[] Visuals;


    private void Start()
    {
        m_spriteRenderer.sprite = Visuals[Random.Range(0, Visuals.Length)];
    }


}