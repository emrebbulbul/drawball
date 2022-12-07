using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class BallScripts : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            gameObject.SetActive(false);
            gameManager.GameOver();

            VibrationScripts.Vibrate(100);


        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            gameObject.SetActive(false);
            gameManager.GoOn(transform.position);
           
           
            
        }
    }
   
}
