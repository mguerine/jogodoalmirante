/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almirante : MonoBehaviour{
    public bool isFacingRight;

    public static Almirante almiranteInstance = null;

   // [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 10.0f;

   // [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75.0f;

    //[SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30.0f;
//
   // [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 80.0f;
//
    //[SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4.0f;


    private Vector2 velocity;

    private void Awake() {
        if (almiranteInstance == null) {
            almiranteInstance = this;
        }
    }


    // Start is called before the first frame update
    void Start(){
        isFacingRight = true;
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Flip the Almirante horizontally when necessary
    protected void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
       
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0) {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        } else {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);
        if (moveInput < 0 && Time.timeScale > 0){
            //posicaox -= walk;
            //transform.position = new Vector3(posicaox, transform.position.y, 0.0f);
            if (isFacingRight){
                Flip(); 
            }
        }

        if (moveInput > 0 && Time.timeScale > 0) {
            //posicaox += walk;
            //transform.position = new Vector3(posicaox, transform.position.y, 0.0f);
            if (!isFacingRight){
                Flip();
            }
        }

    }
}
