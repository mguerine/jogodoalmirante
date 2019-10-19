/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urubu : MonoBehaviour
{
    float time,death_time;
    float posicaox;
    float alterar;

    public GameObject explo2;

    public bool flying;
    public bool isFacingRight;
    public bool dead;
    public Animator animador;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(1,10)< 5) {
            isFacingRight = false;
            alterar = -0.03f;
        } else {
            alterar = 0.03f;
            Flip();
        }
        time = 0f;
        dead = false;
        animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        time += Time.deltaTime;
        posicaox = transform.position.x;

        if (posicaox > 7 || posicaox < -7) { 
            alterar *= -1;
            Flip();
        }
        posicaox += alterar;    
        if (!dead)
        {
            transform.position = new Vector3(posicaox, 4.2f, 0.0f);
        } else {
            //transform.position = new Vector3(posicaox, transform.position.y, 0.0f);
            if (time - death_time > 5.0f) {
                Destroy(this.gameObject);
                Game.instanciaJogo.remUrubu();
            }
        }
        
    }
    
    // Flip vulture horizontally when necessary
    protected void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bola")
        {
            if (!dead) {
                dead = true;
                death_time = time;
                animador.SetTrigger("Die");
                Destroy(collision.gameObject);
                Game.instanciaJogo.UpdatePoints();
            } else {
                animador.SetTrigger("Hit");
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f);
            }
            Instantiate(explo2, this.transform.position, Quaternion.identity);


        }
        if (collision.gameObject.tag == "Urubu")
        {
            alterar *= -1;
            Flip();
            // Ignore collion between vultures
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }

    }
}
