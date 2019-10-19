/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explo : MonoBehaviour
{
    float tempoI,tempoF;
    // Start is called before the first frame update
    void Start()
    {
        tempoF = tempoI = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        tempoF += Time.deltaTime;
        
        // Destroy explosion after a few milisecondss
        if (tempoF - tempoI > 0.2f) { 
            Destroy(this.gameObject);
        }
    }
}
