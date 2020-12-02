using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   
    public float playerDashSpeed;
    public float starDashTime;
   
    Rigidbody2D rb2d;
    Vector2 mov;

    private float speed = 4000;
    private float dashTimeCounter;
    private Vector2 directionFromMouse;
    private bool dashing;
    private bool dashing2 = false;

    public Transform mira; 

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dashing = false;
        dashTimeCounter = starDashTime; 
    }

    void Update()
    {
        //movimiento del personaje
        if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("w"))
        {
            gameObject.GetComponent<Animator>().SetBool("Move", false);
        }

        if (Input.GetKey("a"))
        {
            rb2d.AddForce(new Vector2(-speed * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("Move", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            //dash en movimiento
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(EnableMovementAfter(0.35f));

                if (dashing2)
                {
                    rb2d.AddForce(new Vector2(-35000 * Time.deltaTime, 0));
                    
                }
            }

        }

        if (Input.GetKey("d"))
        {
            rb2d.AddForce(new Vector2(speed * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("Move", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

            //dash en movimiento
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(EnableMovementAfter(0.35f));

                if (dashing2)
                {
                    rb2d.AddForce(new Vector2(35000 * Time.deltaTime, 0));

                }
            }
        }

        if (Input.GetKey("w"))
        {
            rb2d.AddForce(new Vector2(0, speed * Time.deltaTime));
            gameObject.GetComponent<Animator>().SetBool("Move", true);
           
            //dash en movimiento
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(EnableMovementAfter(0.35f));

                if (dashing2)
                {
                    rb2d.AddForce(new Vector2(0, 35000 * Time.deltaTime));

                }
            }
        }
        
        if (Input.GetKey("s"))
        {
            rb2d.AddForce(new Vector2(0, -speed * Time.deltaTime));
            gameObject.GetComponent<Animator>().SetBool("Move", true);

            //dash en movimiento
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(EnableMovementAfter(0.35f));

                if (dashing2)
                {
                    rb2d.AddForce(new Vector2(0, -35000 * Time.deltaTime));

                }
            }
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
        if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("w"))
        {

            if (!dashing)
            {
                if (Input.GetMouseButtonDown(1))
                {

                    PrepareDash();
                    gameObject.GetComponent<Animator>().SetBool("dash", true);
                    

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
        }

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
    }

}



