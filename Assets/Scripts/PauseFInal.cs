using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFInal : MonoBehaviour
{

    private bool pause = false;
    public GameObject panelPause;

    public GameObject SonidoClick;

    void Start()
    {

   
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        if (pause)
        {
            Time.timeScale = 0;           
            panelPause.SetActive(true);

        }
        else if (!pause)
        {
            Time.timeScale = 1;            
            panelPause.SetActive(false);

        }
    }

    public void ApplicationPause()
    {
        Instantiate(SonidoClick, transform.position, Quaternion.identity);
        pause = !pause;
    }

    public void DirigirAlMenu()
    {
        Instantiate(SonidoClick, transform.position, Quaternion.identity);
        SceneManager.LoadScene("menu");

    }
}
