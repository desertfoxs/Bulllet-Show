using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigBoy : MonoBehaviour
{
    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float attackRadius;
    public float speed;

    // Variables relacionadas con el ataque
    [Tooltip("Velocidad de ataque (segundos entre ataques)")]
    public float attackSpeed = 2.3f;
    bool attacking;
    public GameObject bazookaBullet;
    public Transform shotpos1;
    public Transform shotpos2;


    ///--- Variables relacionadas con la vida
    [Tooltip("Puntos de vida")]
    public int maxHp = 3;
    [Tooltip("Vida actual")]
    private double hp;

    // Variable para guardar al jugador
    GameObject player;

    // Variable para guardar la posición inicial
    Vector3 initialPosition, target;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    // Variable para que dropee el arma
    public GameObject BazookaGun;

    //variable para que gire la bala
    public Transform contPost1;
    public Transform contPost2;

    void Start()
    {

        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        ///--- Iniciamos la vida
        hp = maxHp;
    }

    void Update()
    {

        // Por defecto nuestro target siempre será nuestra posición inicial
        target = initialPosition;

        // Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadius,
            1 << LayerMask.NameToLayer("Default")
        // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
        // También poner al objeto Attack y al Prefab Slash una Layer Attack 
        // Sino los detectará como entorno y se mueve atrás al hacer ataques
        );

        // Aquí podemos debugear el Raycast
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }

        // Calculamos la distancia y dirección actual hasta el target
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
        if (target != initialPosition && distance < attackRadius)
        {

            gameObject.GetComponent<Animator>().SetBool("disparar", true);


            ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
            if (!attacking) StartCoroutine(Attack(attackSpeed));
        }
        // En caso contrario nos movemos hacia él
        else
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

            //poner la animacion de mover
            gameObject.GetComponent<Animator>().SetBool("disparar", false);
            gameObject.GetComponent<Animator>().SetBool("move", true);
        }

        // Una última comprobación para evitar bugs forzando la posición inicial
        if (target == initialPosition && distance < 0.05f)
        {
            transform.position = initialPosition;
            // Y cambiamos la animación de nuevo a Idle
            gameObject.GetComponent<Animator>().SetBool("disparar", false);
            gameObject.GetComponent<Animator>().SetBool("move", false);
        }

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);

        //para que gire el personaje
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }




    }

    //para que gire la bala
    public void LateUpdate()
    {
        contPost1.up = contPost1.transform.position - player.transform.position;
        contPost2.up = contPost2.transform.position - player.transform.position;
    }


    //ataque del enemigo
    IEnumerator Attack(float seconds)
    {
        attacking = true;
        Instantiate(bazookaBullet, shotpos1.transform.position, shotpos1.transform.rotation);
        Instantiate(bazookaBullet, shotpos2.transform.position, shotpos2.transform.rotation);
        yield return new WaitForSeconds(seconds);
        attacking = false;
    }



    ///--- Gestión del daño de las armas, la vida y el dropeo del arma
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BuDefault")
        {
            hp = hp - 1;
            if (hp == 0)
            {
                Instantiate(BazookaGun, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }


        }

        if (collision.gameObject.tag == "BuPistol")
        {
            hp = hp - 1;
            if (hp <= 0)
            {
                Instantiate(BazookaGun, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }


        }

        if (collision.gameObject.tag == "BuMachine")
        {
            hp = hp - 0.5;
            if (hp <= 0)
            {
                Instantiate(BazookaGun, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }


        }

        if (collision.gameObject.tag == "BuShotgun")
        {
            hp = hp - 0.5;
            if (hp <= 0)
            {
                Instantiate(BazookaGun, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }


        }

        if (collision.gameObject.tag == "BuBazooka")
        {
            hp = hp - 5;
            if (hp <= 0)
            {
                Instantiate(BazookaGun, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }


        }


    }
}
