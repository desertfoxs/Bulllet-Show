using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed;
 
    private Vector2 directionFromMouse;

    public bool defaultGun;
    public bool pistol;
    public bool machineGun;
    public bool shotGun;
    public bool Bazooka;


    private float normalization;
    private Vector2 normalizedOrientation;


    void Start()
    {
        
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
            rb2d.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 0.5f);
        }
      
        if (Bazooka)
        {
            rb2d.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
            Destroy(gameObject, 2f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un ataque la borramos
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "muro")
        {
      
            Destroy(gameObject);
            
        }

        
    }


    public void DetectarMause()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionFromMouse = mousePosition - (Vector2)transform.position;
        directionFromMouse.Normalize();
    }


}
