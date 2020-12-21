using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    private int indi = 0;
    private bool entro = false;
    private Collider2D tag;
    
    //variables para el ataque
    public Transform shotpos;

    public GameObject defaultGun;
    public GameObject pistol;
    public GameObject shotGun;
    public GameObject machinegun;
    public GameObject bazooka;

    public GameObject Gun;
    public Sprite[] armas;

    //variables para la vida
    public int maxHp = 5;
    private int hp;
    public Vida vida;

    //ui de las armas
    public UiArmas uiarmas;

    //script para controlar la muerte
    public Move move;
    private bool variable = true;

    private bool espera = true;

    //detectar el mause
    private Vector2 directionFromMouse;
    private float normalization;
    private Vector2 normalizedOrientation;

    //detecta el dash
    private bool dash;
    private bool dash2;

    //sonido
    public GameObject[] SonidoPlayer;
    
    public GameObject[] SonidoItems;

    //variable para los puntos
    public GameControler gameControler;

    //variable para el confetti
    public GameObject confetti;
    private bool confettiBool = true;

    //variable para el tp
    private bool TP = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hp = maxHp;
        vida = GameObject.FindObjectOfType<Vida>();
    }

    
    void Update()
    {

        dash = move.EstadoDash();
        dash2 = move.EstadoDash2();

        //logica de agarrar el arma
        if (tag.gameObject.CompareTag("default") && entro)
        {
            if (Input.GetKey("e") && espera)
            {
                espera = false;
                DropearArma();
                indi = 0;
                Instantiate(SonidoPlayer[2], shotpos.transform.position, Quaternion.identity); 


                Uiarma(indi);
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
                Instantiate(SonidoPlayer[2], shotpos.transform.position, Quaternion.identity);
                Uiarma(indi);
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
                Instantiate(SonidoPlayer[2], shotpos.transform.position, Quaternion.identity);
                Uiarma(indi);
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
                Instantiate(SonidoPlayer[2], shotpos.transform.position, Quaternion.identity);
                Uiarma(indi);
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
                Instantiate(SonidoPlayer[2], shotpos.transform.position, Quaternion.identity);
                Uiarma(indi);
                Destroy(tag.gameObject);

                StartCoroutine(EnableMovementAfter(0.5f));
            }

        }

        if (tag.gameObject.CompareTag("Portal") && entro)
        {
            if (confettiBool)
            {               
                StartCoroutine(Confetti(0.4f));
                confettiBool = false;
            }
            
            if (Input.GetKey("e") && TP)
            {

                StartCoroutine(Teleport(0.15f));
                TP = false;
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
    
        DetectarMause();

        rb2d.velocity = -directionFromMouse * patada * Time.fixedDeltaTime;

    }


    public void DetectarMause()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionFromMouse = mousePosition - (Vector2)transform.position;
        directionFromMouse.Normalize();
    }

    public void SonidoDash()
    {
        Instantiate(SonidoPlayer[0], shotpos.transform.position, Quaternion.identity);
    }
    public void SonidoSteps()
    {
        Instantiate(SonidoPlayer[4], shotpos.transform.position, Quaternion.identity);
    }
    public void SonidoDashListo()
    {
        Instantiate(SonidoPlayer[7], shotpos.transform.position, Quaternion.identity);
    }



    //detecta cuando entraste al arma del piso
    public void OnTriggerEnter2D(Collider2D c)
    {
        entro = true;
        tag = c;

        if (!dash && !dash2)
        {
            if (c.gameObject.tag == "BulletsEnemy")
            {
                hp = hp - 1;
                if (hp <= 0)
                {
                    gameObject.GetComponent<Animator>().SetBool("muerte", true);
                    StartCoroutine(Tiempo(1.7f));
                    Gun.gameObject.SetActive(false);
                    rb2d.velocity = Vector2.zero;
                    move.CambioMuerto();

                }


                Instantiate(SonidoPlayer[3], shotpos.transform.position, Quaternion.identity);
                vida.CambioVida(hp);
            }

            if (c.gameObject.tag == "Explocion")
            {
                hp = hp - 3;
                if (hp <= 0)
                {
                    gameObject.GetComponent<Animator>().SetBool("muerte", true);
                    StartCoroutine(Tiempo(2.3f));
                    Gun.gameObject.SetActive(false);
                    rb2d.velocity = Vector2.zero;
                    move.CambioMuerto();

                }

                Instantiate(SonidoPlayer[3], shotpos.transform.position, Quaternion.identity);
                vida.CambioVida(hp);
            }
        }
        

        //item de vida full
        if (c.gameObject.tag == "ItemHealth")
        {
            hp = 5;
            vida.CambioVida(hp);
            Instantiate(SonidoItems[1], transform.position, Quaternion.identity);
            Destroy(tag.gameObject);
        }

        //item de vida +1
        if (c.gameObject.tag == "ItemHealth1")
        {
           
            if(hp >= 5)
            {
                hp = 5;
            }
            else
            {
                hp = hp + 1;
            }
            Instantiate(SonidoItems[0], transform.position, Quaternion.identity);
            vida.CambioVida(hp);
            Destroy(tag.gameObject);
        }

        //tornillo que da puntos
        if (c.gameObject.tag == "Tornillo")
        {
            Instantiate(SonidoItems[2], transform.position, Quaternion.identity);
            gameControler.SumarPuntos(150);
            Destroy(tag.gameObject);
        }

    }

    //detecta cuando salis del arma del piso
    public void OnTriggerExit2D(Collider2D collicion)
    {
        entro = false;

        
    }

    public void Uiarma(int pos)
    {
        uiarmas.CambioPanel(pos);

    }

    //corrutina
    IEnumerator EnableMovementAfter(float seconds)
    {
      yield return new WaitForSeconds(seconds);
        espera = true;
    }

    //tiempo para el sonido de la muerte
    IEnumerator Tiempo(float seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        if (variable) 
        {
            Instantiate(SonidoPlayer[1], shotpos.transform.position, Quaternion.identity);
            variable = false;
        } 
    }

    IEnumerator Confetti(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(confetti, transform.position, transform.rotation);
        Instantiate(SonidoPlayer[6], shotpos.transform.position, Quaternion.identity);
    }

    IEnumerator Teleport(float seconds)
    {
        Instantiate(SonidoPlayer[5], shotpos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
