using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlickeringLight : MonoBehaviour
{
    public Light light;
    public GameObject screen;
    public int numberOfBlink = 2;
    public float blinkDuration = 0.5f;
    public float delayBetweenBlinks = 1.0f;
    public float delayBetweenCycles = 5.0f;

    private void Start()
    {
        StartCoroutine(Blink());
    }



    IEnumerator Blink()
    {
        while (true)
        {
            for (int i =0; i < numberOfBlink; i++)
            {
                light.enabled = true;
                screen.SetActive(true);

                yield return new WaitForSeconds(blinkDuration);

                light.enabled = false;
                screen.SetActive(false);
                yield return new WaitForSeconds(blinkDuration);    
            }

            yield return new WaitForSeconds(delayBetweenCycles);
        }
    }
}
