using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetPos;

    public float smoothin;

    //detectar el mause
    private Vector2 directionFromMouse;
    private float normalization;
    private Vector2 normalizedOrientation;

    void Start()
    {
        
    }

    
    void Update()
    {
        DetectarMause();

        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        targetPos = new Vector3(targetPos.x + directionFromMouse.x * 30, targetPos.y + directionFromMouse.y * 30, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothin * Time.deltaTime);
    }



    public void DetectarMause()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionFromMouse = mousePosition - (Vector2)transform.position;
        directionFromMouse.Normalize();
    }

}
