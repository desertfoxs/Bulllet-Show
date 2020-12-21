using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed;
    public GameObject player;
    Vector3 target, dir, direction;

    public bool pistol;
    public bool machineGun;
    public bool shotGun;
    public bool Bazooka;

    //para el bazooka
    public float tiempo = 2f;
    private float momentoExplocion;
    public GameObject explocion;

   

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        momentoExplocion = Time.time + tiempo;

        if (player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }

        //para que gire la bala
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    void Update()
    {
        
        if (pistol)
        {
            rb2d.velocity = dir * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 1f);
        }

        if (machineGun)
        {
            rb2d.velocity = dir * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 1f);
        }

        if (shotGun)
        {
            ShotGunDisparo();
            Destroy(gameObject, 1f);
        }

        if (Bazooka)
        {
            rb2d.velocity = dir * speed * Time.fixedDeltaTime;

            if (Time.time > momentoExplocion)
            {
                BazookaExplocion();
            }
        }


    }


    public void BazookaExplocion()
    {               
        Destroy(gameObject);
        
        Instantiate(explocion, transform.position, transform.rotation);


    }

    public void ShotGunDisparo()
    {
        float dispercionH = Random.Range(-1f, 1f);
        float dispercionHY = Random.Range(-1f, 1f);

        Vector3 offset = new Vector3(dispercionH, dispercionHY, 0f);

        direction = dir + offset;

        rb2d.velocity = direction * speed * Time.fixedDeltaTime;

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un bullet se borra
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "muro")
        {

            Destroy(gameObject);

        }

        if (Bazooka)
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "muro")
            {

                BazookaExplocion();

            }
        }
    }

  
}
