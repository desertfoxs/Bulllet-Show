using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public Sprite[] vida;

    void Start()
    {
        CambioVida(5);
    }

    
    void Update()
    {}

    public void CambioVida (int pos)
    {
        
        if(pos <= 0)
        {
            this.GetComponent<Image>().sprite = vida[0];

        }
        else
        {
            this.GetComponent<Image>().sprite = vida[pos];
        }
    }
}
