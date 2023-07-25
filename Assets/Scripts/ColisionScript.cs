using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ColisionScript : MonoBehaviour
{
    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI dineroText;
    
    private float vida=99;
    private int dinero=0;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pilar"))
        {
          vida--;
          vidaText.text = vida.ToString();
        } else if(collision.gameObject.CompareTag("Moneda"))
        {
          dinero++;
          dineroText.text = dinero.ToString();
          //para que desaparezca despues de cogerla
          Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("caer"))
        {
          SceneManager.LoadScene("Interfaz");


        }
    }
}
