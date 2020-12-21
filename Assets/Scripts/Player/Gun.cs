using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform mira;
    public Transform ContArma;

    public Transform shotpos;

    public Player player;

    public GameObject defaultBullet;
    public GameObject pistolBullet;
    public GameObject shotgunBullet;
    public GameObject machinegunBullet;
    public GameObject bazookaBullet;

    private int indice;

    private bool disparo = true;

    public int cantidadBalas;
 
    public float retroceso = 10000f;

    //sonido
    public GameObject[] SonidoGun;

    void Start()
    {

    }


    void Update()
    {
        DetectarMause();

        //para que rote el sprite
        if (mira.transform.position.x < ContArma.transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (mira.transform.position.y > ContArma.transform.position.y)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }

        if (Input.GetMouseButton(0))
        {
            indice = player.IndiceDelArreglo();

            Disparo();
        }




    }

    //diparar
    public void Disparo()
    {
        if (disparo && indice == 0)
        {
            Instantiate(defaultBullet, shotpos.transform.position, Quaternion.identity);
            Instantiate(SonidoGun[0], shotpos.transform.position, Quaternion.identity);
            StartCoroutine(EnableMovementAfter(1.3f));

        }

        if (disparo && indice == 1)
        {
            Instantiate(pistolBullet, shotpos.transform.position, Quaternion.identity);
            Instantiate(SonidoGun[1], shotpos.transform.position, Quaternion.identity);
            StartCoroutine(EnableMovementAfter(1f));

        }

        if (disparo && indice == 2)
        {
            ShotGun();
            Instantiate(SonidoGun[2], shotpos.transform.position, Quaternion.identity);
            StartCoroutine(EnableMovementAfter(1.5f));

        }

        if (disparo && indice == 3)
        {
            Instantiate(machinegunBullet, shotpos.transform.position, transform.rotation);
            Instantiate(SonidoGun[3], shotpos.transform.position, Quaternion.identity);
            StartCoroutine(EnableMovementAfter(0.2f));

        }

        if (disparo && indice == 4)
        {
            player.Retroceso(retroceso);
            Instantiate(bazookaBullet, shotpos.transform.position, transform.rotation);
            Instantiate(SonidoGun[4], shotpos.transform.position, Quaternion.identity);
            StartCoroutine(EnableMovementAfter(2.3f));

        }
    }


    //para que gire el arma
    public void LateUpdate()
    {
        ContArma.up = ContArma.position - mira.position;
    }


    //detectar el mause
    public void DetectarMause()
    {
        mira.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
            ));
    }

    public void ShotGun()
    {

        for (int i = 0; i < cantidadBalas; i++)
        {

            Instantiate(shotgunBullet, shotpos.transform.position, transform.rotation);

        }

    }

    //corrutina
    IEnumerator EnableMovementAfter(float seconds)
    {
        disparo = false;
        yield return new WaitForSeconds(seconds);
        disparo = true;
    }


   
}