using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Destruirmeteo : MonoBehaviour
{
    private void Start()
    {
        // Llamar a la función Destruir después de 20 segundos
        Invoke("Destruir", 20f);
    }

    private void Destruir()
    {
        // Destruir el objeto
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            Destruir();
        }
    }
}
