/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y+0.2f, 0.0f);
        transform.rotation = new Quaternion(transform.rotation.x+Random.Range(1,1000), transform.rotation.y + Random.Range(-1, -1000), transform.rotation.z, transform.rotation.w );
    }

   
}
