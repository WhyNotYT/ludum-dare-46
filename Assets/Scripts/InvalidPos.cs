using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvalidPos : MonoBehaviour
{

    private Controller controller;


    void Start()
    {
        controller = FindObjectOfType<Controller>();
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Babe")
        {
            controller.invalidBabyPos = true;
            controller.LastValidPos = collision.transform.position;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Babe")
        {
            controller.invalidBabyPos = false;
        }
    }
}
