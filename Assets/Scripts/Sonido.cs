using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{

    public float Tiempo;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, Tiempo);
    }
}
