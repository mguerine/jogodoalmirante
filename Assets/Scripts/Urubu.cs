/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urubu : MonoBehaviour
{
    float time,death_time;
    float direction;

    public GameObject explo2;
    public GameObject shit1;
    
    public bool flying;
    public bool dead;
    public Animator animador;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private bool hit;

   // [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 100.0f;

    //[SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 0.5f;

    //[SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 1f;

    //[SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float airDeceleration = 200.0f;

    //[SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();

        if (Random.Range(1,10)< 5) {
            direction = -1f;
        } else {
            Flip();
            direction = 1f;
        }
        time = 0f;
        dead = false;
        hit = false;
        animador = GetComponent<Animator>();
        // Ignore collision between shit and vulture
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), shit1.gameObject.GetComponent<Collider2D>());
        // Ignore collision between game and vulture
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameController.gameInstance.floor.gameObject.GetComponent<Collider2D>());
        // Ignore layer collision 
        Physics2D.IgnoreLayerCollision(8, 8);


    }

    void Update() {
        // Makes vulture shit
        if (Random.Range(1, 100) <= 1 && !dead && Time.timeScale > 0) {
            Instantiate(shit1, new Vector3(transform.position.x, transform.position.y - 0.5f, 0.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
              
        if (!dead) {
            //transform.position = new Vector3(posicaox, 4.2f, 0.0f);
            //velocity.x = Mathf.MoveTowards(velocity.x, speed * direction, airAcceleration * Time.deltaTime);
            body.velocity = new Vector2(speed*direction*Time.deltaTime,body.velocity.y);
           
        } else {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            //velocity.x = Mathf.MoveTowards(velocity.x, 0, airDeceleration * Time.deltaTime);
            speed = 0.0f;
            if (time - death_time > 3.0f) {
                Destroy(this.gameObject);
                GameController.gameInstance.remUrubu();
            }
        }
        //transform.Translate(velocity * Time.deltaTime);
        time += Time.deltaTime;

        
    }

    // Flip vulture horizontally when necessary
    protected void Flip(){
        sprite.flipX = !sprite.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bola"){
            if (!dead) {
                dead = true;
                death_time = time;
                animador.SetTrigger("Die");
                Destroy(collision.gameObject);
                GameController.gameInstance.UpdatePoints(1);
            } else {
                animador.SetTrigger("Hit");
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f);
            }
            Instantiate(explo2, this.transform.position, Quaternion.identity);
            // Ignore collision between walls and vulture
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameController.gameInstance.wallL.gameObject.GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameController.gameInstance.wallR.gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "Urubu" ){
            direction *= -1;
            Flip();
            // Ignore collion between vultures
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
            if (direction > 0) {
                this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 1000.0f);
            } else {
                this.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 1000.0f);
            }

        }

        if ( collision.gameObject.tag == "Wall") {
            //velocity.x = Mathf.MoveTowards(velocity.x, 0, airDeceleration * Time.deltaTime);

            direction *= -1;
            Flip();
            if (direction > 0) {
                this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 1000.0f);
            } else {
                this.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 1000.0f);
            }
        }

        if (collision.gameObject.tag == "Player" && !hit) {
            hit = true;
            collision.gameObject.GetComponent<AudioSource>().Play();
            GameController.gameInstance.UpdatePoints(-5);
            Instantiate(GameController.gameInstance.explo, new Vector3(GameController.gameInstance.almirante.transform.position.x, GameController.gameInstance.almirante.transform.position.y + 1.0f, GameController.gameInstance.almirante.transform.position.z), Quaternion.identity);
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall" ) {
            // Revert collion between vultures and walls
            //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            // Revert collion between vultures and walls
            //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider,false);
        }
    }


}
