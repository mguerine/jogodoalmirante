/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almirante : MonoBehaviour
{

    public float posicaox, posicaoy, walk;
    public bool isFacingRight;
    public static Almirante instanciaAlmirante = null;


    private void Awake() {
        if (instanciaAlmirante == null) {
            instanciaAlmirante = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        posicaox = -6.2f;
        posicaoy = -3.0f;
        walk = 0.2f;
        transform.position = new Vector3(posicaox, posicaoy, 0.0f);
        
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

        if (Input.GetKey(KeyCode.A))
        {
            posicaox -= walk;
            transform.position = new Vector3(posicaox, posicaoy, 0.0f);
            if (isFacingRight){
                Flip();
            }

        }

        if (Input.GetKey(KeyCode.D))
        {
            posicaox += walk;
            transform.position = new Vector3(posicaox, posicaoy, 0.0f);
            if (!isFacingRight)
            {
                Flip();
            }
        }

    }
}
