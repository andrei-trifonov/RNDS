using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.UIElements;
using UnityEngine;

public class OuterRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Sprite;

    private SpriteRenderer m_SpriteRenderer;
    private void Start()
    {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        m_SpriteRenderer.sprite = Sprite.sprite;
       
    }
}
