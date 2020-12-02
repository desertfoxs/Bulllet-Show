using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed;
    public GameObject player;
    Vector3 target, dir;

    public bool pistol;
    public bool machineGun;
    public bool shotGun;
    public bool Bazooka;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

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
            rb2d.velocity = dir * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 0.5f);
        }

        if (Bazooka)
        {
            rb2d.velocity = dir * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 2f);
        }
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un bullet se borra
        if (col.gameObject.tag == "Player")
        {

            Destroy(gameObject);

        }


    }
}
