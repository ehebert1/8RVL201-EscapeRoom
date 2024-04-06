using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public class PC_Controller : MonoBehaviour
{
    public AudioSource openSound;

    const string computer_screen_name = "interactible_screen";
    const string computer_screen_code_name = "interactible_screen_code";
    const string the_screen = "COMPUTERscreen";

    public void OpenComputer()
    {
        var obj = GameObject.FindObjectsOfType(typeof(MonoBehaviour));
        openSound.Play();

        foreach (var item in obj)
        {
            if (item.name.Contains(the_screen))
            {
                Transform itemGameObj = item.GameObject().transform;

                for(int i =0; i< itemGameObj.childCount; i++)
                {
                    Transform child = itemGameObj.GetChild(i);

                    if (child.name.Equals(computer_screen_name))
                    {
                        child.transform.gameObject.SetActive(true);
                    }

                    if (child.name.Equals(computer_screen_code_name))
                    {
                        child.transform.gameObject.SetActive(true);
                        var flick_comp = item.GetComponent<FlickeringLight>();
                        if (flick_comp != null)
                        {
                            flick_comp.enabled = true;
                            flick_comp.StartBlink();
                        }                            
                    }
                }
            }
        }
    }
}
