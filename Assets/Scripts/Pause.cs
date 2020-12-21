using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    private bool pause = false;
    public GameObject panelPause;
    private Move playerScript;
    private Gun gun;

    public GameObject SonidoClick;

    void Start()
    {

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
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
            playerScript.enabled = false;
            gun.enabled = false;
            panelPause.SetActive(true);
            
        }
        else if (!pause)
        {
            Time.timeScale = 1;
            playerScript.enabled = true;
            gun.enabled = true;
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
