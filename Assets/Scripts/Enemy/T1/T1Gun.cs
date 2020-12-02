using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1Gun : MonoBehaviour
{
    public Transform ContArma;
    public Transform shotpos;
    public Transform player;
    public GameObject pistolBullet;


    void Start()
    {
        
    }

    
    void Update()
    {
        //para rotar el personaje
        if (player.transform.position.x < ContArma.transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (player.transform.position.y > ContArma.transform.position.y)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }


    //para que gire el arma
    public void LateUpdate()
    {
        ContArma.up = ContArma.position - player.transform.position;
    }


    public void Disparar()
    {
        Instantiate(pistolBullet, shotpos.transform.position, Quaternion.identity);

    }
}
