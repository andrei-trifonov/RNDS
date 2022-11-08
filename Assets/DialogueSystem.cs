using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Color = System.Drawing.Color;

public enum charsDropdown { 
      Psye,
      Lilly,
      Hans
};

[System.Serializable]
public struct chatWindow {
    public charsDropdown Char;
    public Text textAuthor;
    public Text textLine;
    public Animator chatCloud;
}
[System.Serializable]
public struct chatLine
{
    public charsDropdown Char;
    [TextArea(15, 10)]
    public string textLine;

}

[System.Serializable]

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private chatWindow[] chatWindows;
    [SerializeField] private chatLine[] chatLines;

    private string Language;
    [SerializeField] private PlatformerCharacter2D Player;
    private int stringCounter;
    private bool Blocked;

    private Tuple<UnityEngine.Color, string> ReturnCharName(int Char)
    {
        

        switch (Language) {
            case "RUS":
                { 
                    switch (Char) {
                        case 0:
                            return Tuple.Create(UnityEngine.Color.yellow, "Сай"); break;
                        case 1: 
                            return Tuple.Create(UnityEngine.Color.red,  "Лилли"); break;
                        case 2: 
                            return Tuple.Create(UnityEngine.Color.gray, "Ханс"); break;
                    
                                     
                    } 
                  
                }
            break;
        }
      return Tuple.Create(UnityEngine.Color.black,  "Unknown");;

    }

    [SerializeField] private bool DEBUG;
    private void Start()
    {
        if (DEBUG) {
            PlayerPrefs.SetString("Language", "RUS");
        }
         for(int i =0; i< chatWindows.Length; i++)
         {
             chatWindows[i].chatCloud.gameObject.SetActive(false);
         }
        Language = PlayerPrefs.GetString("Language");
    }   


    public void ContinueDialogue()
    {
        Player.Block();
        if (!Blocked && stringCounter < chatLines.Length)
        {

        
            for(int i=0;i< chatWindows.Length; i++)
            {
                chatWindows[i].chatCloud.gameObject.SetActive(false);
                if (chatWindows[i].Char == chatLines[stringCounter].Char)
                {
                   
                    chatWindows[i].chatCloud.gameObject.SetActive(true);
                    Tuple<UnityEngine.Color, string>  tuple= ReturnCharName(i);
                    chatWindows[i].textAuthor.color = tuple.Item1;
                    chatWindows[i].textAuthor.text = tuple.Item2; 
                    chatWindows[i].chatCloud.SetBool("PopUp", true);
                    chatWindows[i].textLine.text = chatLines[stringCounter].textLine;
                    break;
                }
            }
        
            stringCounter++;
       

           
        }
        else
        {
            EndDialogue();
        }
          
       
    }
    private void EndDialogue()
    {
        Player.GetComponent<PlatformerCharacter2D>().UnBlock();
        stringCounter = 0;
        foreach (chatWindow chat in chatWindows)
        {
            chat.chatCloud.gameObject.SetActive(false);
           
            
        }
    }
}
