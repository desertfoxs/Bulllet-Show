using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameControler : MonoBehaviour
{
    public static int Score;
    public TextMeshProUGUI TextScore;
    public static GameControler gameControler;
    public string nombreEscena;

    void Awake()
    {
        gameControler = this;
        DontDestroyOnLoad (gameObject);
    }

    void Start()
    { 
        TextScore.text = Score.ToString(); 
    }

   
    void Update()
    {
        if (Input.GetKey("r"))
        {
            RestarGame();
        }

        
    }

    public void SumarPuntos(int puntos)
    {
        Score = Score + puntos;
        TextScore.text = Score.ToString();
        
    }

    public void RestarGame()
    {
        SceneManager.LoadScene(nombreEscena);
        Score = 0;
    }
}
