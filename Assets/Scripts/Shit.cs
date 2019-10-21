/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : MonoBehaviour
{
    public Animator animador;
    public bool floor,hit;
    public float time,time_floor;
    // Start is called before the first frame update
    void Start()
    {
        time_floor = time = Time.deltaTime;
        hit = floor = false;
        animador = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        time += Time.deltaTime;
        if (!floor) {
            //transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0.0f);
        } else {
            //transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
            if (time - time_floor > 5.0f) {
                Destroy(this.gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            floor = true;
            time_floor = Time.deltaTime;
            
        }
        if (collision.gameObject.tag == "Player" && !hit) {
            hit = true;
            collision.gameObject.GetComponent<AudioSource>().Play();
            GameController.gameInstance.UpdatePoints(-1);
            Instantiate(GameController.gameInstance.explo, new Vector3(GameController.gameInstance.almirante.transform.position.x, GameController.gameInstance.almirante.transform.position.y+1.0f, GameController.gameInstance.almirante.transform.position.z), Quaternion.identity);


        }
        animador.SetTrigger("shit-final");
        // Ignore collision between shit and almirante
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameController.gameInstance.almirante.gameObject.GetComponent<Collider2D>());

    }
}
