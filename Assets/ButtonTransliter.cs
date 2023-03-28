using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTransliter : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private string scriptName;
    [SerializeField] private string Function;
    private MonoBehaviour[] Scripts;
    private MonoBehaviour targetScript;
    private void Start()
    {
        Scripts = Target.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in Scripts)
        {
            
            if (script.GetType().ToString() == scriptName)
            {
                targetScript = script;
            }
        }
    }
    public void PressButton() 
    { 
         targetScript.Invoke(Function, 0);
    }
}

