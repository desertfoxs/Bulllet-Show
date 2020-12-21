using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject SonidoClick;

    public void Jugar()
    {
        Instantiate(SonidoClick, transform.position, Quaternion.identity);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Instantiate(SonidoClick, transform.position, Quaternion.identity);
        Application.Quit();
        
    }
}
