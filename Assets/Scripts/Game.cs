/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject urubu;
    public GameObject ball;
    public GameObject almirante;
    
    public Text textpoint;
    public int points;
    public int n_vulture;
    public static Game instanciaJogo = null;


    private void Awake()
    {
        // Singleton for instantiate game manager only once
        if (instanciaJogo == null)
        {
            instanciaJogo = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // number of vultures instantiated
        n_vulture = 0;
        // number of game points
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if(Almirante.instanciaAlmirante.isFacingRight)
                Instantiate(ball, new Vector3(almirante.transform.position.x+1, almirante.transform.position.y+0.8f, almirante.transform.position.z), Quaternion.identity);
            else
                Instantiate(ball, new Vector3(almirante.transform.position.x - 1, almirante.transform.position.y + 0.8f, almirante.transform.position.z), Quaternion.identity);
        }

        // Give a small random chance to instantiate a new vulture
        if(Random.Range(1,1000) <=  10 && n_vulture < 8) {
            Instantiate(urubu, new Vector3(Random.Range(-7, 7), 4.2f, 0.0f), Quaternion.identity);
            addUrubu();
        }
        
    }

    public void UpdatePoints()
    {
        points++;
        textpoint.text = "Pontos: " + points.ToString();
        
    }

    public void OnCollisionEnter2D(Collision2D colisao)
    {
        if(colisao.gameObject.tag == "Bola")
        {
            Destroy(colisao.gameObject);
        }
    }

    public void addUrubu() {
        n_vulture++;
    }

    public void remUrubu() {
        n_vulture--;
    }

}
