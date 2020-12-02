using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    private int indi = 0;
    private bool entro = false;
    private Collider2D tag;
    
    public Transform shotpos;

    public GameObject defaultGun;
    public GameObject pistol;
    public GameObject shotGun;
    public GameObject machinegun;
    public GameObject bazooka;

    public GameObject Gun;
    public Sprite[] armas;

    public int maxHp = 5;
    private int hp;

    public Vida vida;

    private bool espera = true;

    //detectar el mause
    public Transform mira;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hp = maxHp;
        vida = GameObject.FindObjectOfType<Vida>();
    }

    
    void Update()
    {
        //logica de agarrar el arma
        if (tag.gameObject.CompareTag("default") && entro)
        {
            if (Input.GetKey("e") && espera)
            {
                espera = false;
                DropearArma();
                indi = 0;
                Destroy(tag.gameObject);
                
                StartCoroutine(EnableMovementAfter(0.5f));

            }

        }


        if (tag.gameObject.CompareTag("pistol") && entro)
        {
            if (Input.GetKey("e") && espera)
            {
                espera = false;
                DropearArma();
                indi = 1;
                Destroy(tag.gameObject);

                StartCoroutine(EnableMovementAfter(0.5f));
            }

        }



        if (tag.gameObject.CompareTag("shotgun") && entro)
        {
            if (Input.GetKey("e") && espera)
            {
                espera = false;
                DropearArma();
                indi = 2;
                Destroy(tag.gameObject);

                StartCoroutine(EnableMovementAfter(0.5f));

            }

        }
       
        
        if (tag.gameObject.CompareTag("machinegun") && entro)
        {
            if (Input.GetKey("e") && espera)
            {
                espera = false;
                DropearArma();
                indi = 3;
                Destroy(tag.gameObject);

                StartCoroutine(EnableMovementAfter(0.5f));
            }

        }


        if (tag.gameObject.CompareTag("bazooka") && entro)
        {
            if (Input.GetKey("e") && espera)
            {
                espera = false;
                DropearArma();
                indi = 4;
                Destroy(tag.gameObject);

                StartCoroutine(EnableMovementAfter(0.5f));
            }

        }


        Gun.gameObject.GetComponent<SpriteRenderer>().sprite = armas[indi];

    }

    public int IndiceDelArreglo()
    {       
        return (indi);
    }


    //dropea el arma que ya tenias
    public void DropearArma()
    {
        if (indi == 0)
        {
            Instantiate(defaultGun, shotpos.transform.position, Quaternion.identity);
        }

        if (indi == 1)
        {
            Instantiate(pistol, shotpos.transform.position, Quaternion.identity);
        }

        if (indi == 2)
        {
            Instantiate(shotGun, shotpos.transform.position, Quaternion.identity);
        }
        
        if (indi == 3)
        {
            Instantiate(machinegun, shotpos.transform.position, Quaternion.identity);
        }
       
        if (indi == 4)
        {
            Instantiate(bazooka, shotpos.transform.position, Quaternion.identity);
        }
    }

    //retroceso del arma
    public void Retroceso(float patada)
    {
        //trabando todavia en esto xd

        DetectarMause();

        if (mira.transform.position.x < mira.transform.position.y)
        {

            if (mira.transform.position.x < transform.position.x)
            {
                rb2d.AddForce(new Vector2(patada * Time.deltaTime, 0));
            }
            else
            {
                rb2d.AddForce(new Vector2(0, -patada * Time.deltaTime));
               
            }
        }
        else
        {

            if (mira.transform.position.y > transform.position.y)
            {
                rb2d.AddForce(new Vector2(0, patada * Time.deltaTime));
            }
            else
            {
                rb2d.AddForce(new Vector2(-patada * Time.deltaTime, 0));
              
            }
        }

    }


    public void DetectarMause()
    {
        mira.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
            ));
    }


    //detecta cuando entraste al arma del piso
    public void OnTriggerEnter2D(Collider2D c)
    {
        entro = true;
        tag = c;
    }

    //detecta cuando salis del arma del piso
    public void OnTriggerExit2D(Collider2D collicion)
    {
        entro = false;

        if (collicion.gameObject.tag == "BulletsEnemy")
        {
            hp = hp - 1;
            if (hp == 0)
            {
                gameObject.GetComponent<Animator>().SetBool("muerte", true);
                Gun.gameObject.SetActive(false);
                //gameObject.GetComponent<Rigidbody2D>().Constraints.FreezePositionX = true;
                //gameObject.GetComponent<Rigidbody2D>().Constraints.FreezePositiony = true;
            }

            vida.CambioVida(hp);
        }
    }


    //corrutina
    IEnumerator EnableMovementAfter(float seconds)
    {
      yield return new WaitForSeconds(seconds);
        espera = true;
    }
}
