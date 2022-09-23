using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.UIElements;
using UnityEngine;

public class OuterRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Sprite;
    
    private void FixedUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.sprite;
    }
}
