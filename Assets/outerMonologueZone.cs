using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outerMonologueZone : MonoBehaviour
{
    [SerializeField] private DialogueSystem dS;
    [SerializeField] chatVariant Chat;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dS.ClearDialogue();
            dS.AddVariant(Chat);
        }
    }
}
