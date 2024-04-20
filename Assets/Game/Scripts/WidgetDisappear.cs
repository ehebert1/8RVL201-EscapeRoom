using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetDisappear : MonoBehaviour
{
    public float CooldownTime = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CooldownTime -= Time.deltaTime;

        if (CooldownTime <= 0)
        {
            this.gameObject.SetActive(false);
        }

    }
}
