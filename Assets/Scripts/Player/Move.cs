using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   
    public float playerDashSpeed;
    public float starDashTime;
   
    Rigidbody2D rb2d;
    Vector2 mov;

    public float speed = 10000;
    private float dashTimeCounter;
    private Vector2 directionFromMouse;
    private bool dashing = false;
    private bool dashing2 = false;

    public Transform mira;

    private bool muerto;

    //para el sonido
    public Player player;

    //para el dash
    private bool tiempo = true;
    private bool SonidoDash = false;
    private bool movimiento = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dashTimeCounter = starDashTime; 

    }

    void Update()
    {
        //movimiento del personaje
        
        if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("w"))
        {
            gameObject.GetComponent<Animator>().SetBool("Move", false);
            movimiento = false;
        }

        if (!muerto)
        {

            if (Input.GetKey("a"))
            {
                rb2d.AddForce(new Vector2(-speed * Time.deltaTime, 0));
                gameObject.GetComponent<Animator>().SetBool("Move", true);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                movimiento = true;

               
            }

            if (Input.GetKey("d"))
            {
                rb2d.AddForce(new Vector2(speed * Time.deltaTime, 0));
                gameObject.GetComponent<Animator>().SetBool("Move", true);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                movimiento = true;

                
            }

            if (Input.GetKey("w"))
            {
                rb2d.AddForce(new Vector2(0, speed * Time.deltaTime));
                gameObject.GetComponent<Animator>().SetBool("Move", true);
                movimiento = true;
                
            }

            if (Input.GetKey("s"))
            {
                rb2d.AddForce(new Vector2(0, -speed * Time.deltaTime));
                gameObject.GetComponent<Animator>().SetBool("Move", true);
                movimiento = true;
                
            }


            //rotacion del personaje
            if (mira.transform.position.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }


            //el uso del dash
            if (Input.GetMouseButtonDown(1) && tiempo)
            {

                if (movimiento)
                {
                    if (Input.GetKey("a"))
                    {
                                                
                            SonidoDash = true;
                            StartCoroutine(EnableMovementAfter(0.35f));

                            if (dashing2)
                            {
                                rb2d.AddForce(new Vector2(-80000 * Time.deltaTime, 0));
                                player.SonidoDash();
                            }
                        

                    }

                    if (Input.GetKey("d"))
                    {
                                               
                            SonidoDash = true;
                            StartCoroutine(EnableMovementAfter(0.35f));

                            if (dashing2)
                            {
                                rb2d.AddForce(new Vector2(80000 * Time.deltaTime, 0));
                                player.SonidoDash();
                            }
                        
                    }

                    if (Input.GetKey("w"))
                    {
                                                
                            SonidoDash = true;
                            StartCoroutine(EnableMovementAfter(0.35f));

                            if (dashing2)
                            {
                                rb2d.AddForce(new Vector2(0, 80000 * Time.deltaTime));
                                player.SonidoDash();
                            }
                        
                    }

                    if (Input.GetKey("s"))
                    {
                        
                            SonidoDash = true;
                            StartCoroutine(EnableMovementAfter(0.35f));

                            if (dashing2)
                            {
                                rb2d.AddForce(new Vector2(0, -80000 * Time.deltaTime));
                                player.SonidoDash();
                            }
                        
                    }


                }
                else
                {
                    if (tiempo)
                    {
                        SonidoDash = true;
                        PrepareDash();
                        gameObject.GetComponent<Animator>().SetBool("dash", true);
                        player.SonidoDash();
                    }
                }

            }
            else
            {

                                           
                 if (dashTimeCounter <= 0f)
                 {
                     dashing = false;
                     dashTimeCounter = starDashTime;
                     gameObject.GetComponent<Animator>().SetBool("dash", false);
                 }
                 else
                 {
                     dashTimeCounter -= Time.deltaTime;
                 }


                

            }

        }


        //detectar el mause
        DetectarMause();

    }

    public bool EstadoDash2()
    {
        
        return (dashing2);
        
    }
    public bool EstadoDash()
    {
        
        return (dashing);
        
    }

    public void DetectarMause()
    {
        mira.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
            ));
    }

    public void FixedUpdate()
    {
        if (dashing)
        {
            rb2d.velocity = directionFromMouse * playerDashSpeed * Time.fixedDeltaTime;
            StartCoroutine(TiempoDash(2.5f));
        }

    }

    //detecta que esta muerto
    public void CambioMuerto()
    {
        muerto = true;
    }

    void PrepareDash()
    {
        dashing = true;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionFromMouse = mousePosition - (Vector2)transform.position;
        directionFromMouse.Normalize();

        //ESTO HACE ROTAR AL PERSONAJE
        //float angleToRotate = Mathf.Atan2(directionFromMouse.y, directionFromMouse.x) * Mathf.Rad2Deg;
        //rb2d.rotation = angleToRotate;
    }

   
    //corrutina
    IEnumerator EnableMovementAfter(float seconds)
    {
        dashing2 = true;
        gameObject.GetComponent<Animator>().SetBool("dash", true);

        yield return new WaitForSeconds(seconds);

        gameObject.GetComponent<Animator>().SetBool("dash", false);
        StartCoroutine(TiempoDash(2.5f));
        dashing2 = false;
    }

    IEnumerator TiempoDash(float seconds)
    {
        tiempo = false;
        
        yield return new WaitForSeconds(seconds);
        
        tiempo = true;

        if (SonidoDash)
        {
            player.SonidoDashListo();
            SonidoDash = false;
        }
    }

}



