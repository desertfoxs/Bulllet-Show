using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{
   
    void Start()
    {
        
    }

  
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<Animator>().SetBool("disparo", true);
            
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("disparo", false);
        }
    }

 

}
