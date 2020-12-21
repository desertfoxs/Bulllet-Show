using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed;
 
    private Vector2 directionFromMouse;
    private Vector2 directionFromMouseShotGun;

    public Transform player;

    public bool defaultGun;
    public bool pistol;
    public bool machineGun;
    public bool shotGun;
    public bool Bazooka;


    private float normalization;
    private Vector2 normalizedOrientation;

    //para el bazooka
    public float tiempo = 2f;
    private float momentoExplocion;
    public GameObject explocion;



    void Start()
    {
        momentoExplocion = Time.time + tiempo;

        rb2d = GetComponent<Rigidbody2D>();
        DetectarMause();
    }

    
    void Update()
    {
        if (defaultGun)
        {
            rb2d.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 0.7f);
        }
        
        if (pistol)
        {
            rb2d.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 1f);
        }

        if (machineGun)
        {
            rb2d.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 1f);
        }

        if (shotGun)
        {
            rb2d.velocity = directionFromMouseShotGun * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 0.5f);
        }
      
        if (Bazooka)
        {
            rb2d.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
            
            if (Time.time > momentoExplocion)
            {
                BazookaExplocion();
            }
        }
    }


    public void DetectarMause()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionFromMouse = mousePosition - (Vector2)transform.position;
        directionFromMouse.Normalize();


        float dispercionH = Random.Range(-0.3f, 0.3f);
        float dispercionHY = Random.Range(-0.3f, 0.3f);

        Vector2 offset = new Vector2(dispercionH, dispercionHY);

        directionFromMouseShotGun = directionFromMouse + offset;
    }

    public void BazookaExplocion()
    {       

        Instantiate(explocion, transform.position, transform.rotation);
       
        Destroy(gameObject);

    }



    void OnTriggerEnter2D(Collider2D col)
    {
      

            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "muro")
            {

                Destroy(gameObject);

            }

            if (Bazooka)
            {
                if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "muro")
                {

                    BazookaExplocion();

                }
            }
        
    }

    //corrutina
    IEnumerator Destruir(float seconds)
    {       
        yield return new WaitForSeconds(seconds);
        Destroy(explocion.gameObject);
        Debug.Log("se activo destruir");
    }
}
