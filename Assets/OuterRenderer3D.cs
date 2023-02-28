using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.UIElements;
using UnityEngine;

public class OuterRenderer3D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Actor;
    private ShadowGPT shadow3D;
    public PlatformerCharacter2D PC2D;
    private void Start()
    {
        shadow3D = gameObject.GetComponent<ShadowGPT>();
    }
    private void FixedUpdate()
    {
       
         shadow3D.SetSprite(Actor.sprite,!PC2D.GetFacing());
    }
}
