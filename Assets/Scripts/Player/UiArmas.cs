using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiArmas : MonoBehaviour
{ 
    public Sprite[] panel;

    void Start()
    {
        CambioPanel(5);
     
    }


    void Update()
    {
    }

    public void CambioPanel(int pos)
    {

        if (pos <= 0)
        {
            this.GetComponent<Image>().sprite = panel[0];

        }
        else
        {
            GetComponent<Image>().sprite = panel[pos];
        }
    }
}
