using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarNPC : MonoBehaviour
{
    public float followDistance = 50f; // Distancia a la cual el NPC comenzará a seguir al jugador
    public float moveSpeed = 50f; // Velocidad de movimiento del NPC
    public float rotationSpeed = 2f; // Velocidad de rotación del NPC

    private Transform currentTarget;
    private Vector3 randomTarget;

    private float tiempoActual;
    public float timepower = 5f;
    private int vida = 1000;
    private int danio = 1;
    private bool counting = false;
    public Slider healthBar;
    public bool dame = false;


    //private CarController npc;
    //public CarController controller;

    private void Start()
    {
        //npc = GetComponent<CarController>();
        randomTarget = GetRandomTarget(); // Obtiene un objetivo aleatorio inicial
        vida = 1000;
        danio = 11;
    }

    private void Update()
    {
        // Busca el carro más cercano con la etiqueta "Player" y lo establece como el objetivo actual
        GameObject[] playerCars = GameObject.FindGameObjectsWithTag("Player");
        // GameObject[] playernpc = GameObject.FindGameObjectsWithTag("npc");

        float nearestDistance = float.MaxValue;

        foreach (GameObject car in playerCars)
        {
            float distanceToCar = Vector3.Distance(transform.position, car.transform.position);

            if (distanceToCar <= followDistance && distanceToCar < nearestDistance)
            {
                currentTarget = car.transform;
                nearestDistance = distanceToCar;
            }
        }

        // foreach (GameObject car in playernpc)
        // {
        //     float distanceToCar = Vector3.Distance(transform.position, car.transform.position);

        //     if (distanceToCar <= followDistance && distanceToCar < nearestDistance)
        //     {
        //         currentTarget = car.transform;
        //         nearestDistance = distanceToCar;
        //     }
        // }
        if (currentTarget != null)
        {
            // El NPC sigue al objetivo
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= followDistance)
            {
                // Movimiento y rotación hacia el objetivo (carro del jugador)
                Vector3 direction = currentTarget.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    rotation,
                    rotationSpeed * Time.deltaTime
                );
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }if (dame)
        {
            healthBar.value = vida;
            // dame = false;
        }
        else
        {
            // Movimiento aleatorio por la arena
            float distanceToRandomTarget = Vector3.Distance(transform.position, randomTarget);

            if (distanceToRandomTarget <= 5f)
            {
                randomTarget = GetRandomTarget();
            }

            Vector3 direction = randomTarget - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                rotation,
                rotationSpeed * Time.deltaTime
            );
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        healthBar.value = vida;

        
    }

    private Vector3 GetRandomTarget()
    {
        // Obtiene una posición aleatoria dentro de un área específica (arena)
        float x = Random.Range(-10f, 20f);
        float z = Random.Range(-10f, 20f);
        return new Vector3(x, 0f, z);
    }

    /*
    public void Die()
    {
        // Realizar las acciones necesarias cuando el NPC muera...

        // Verificar el resultado del juego
      npc.CheckGameResult();
    }
    */
    void damage()
    {
        // danio = 0;
        dame = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("caer"))
        {
            // print("caer ENTRO");
            // Invertir los controles aquí
             transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            vida = vida - danio;
            print("vida quitada:" + vida);
            if (vida <= 0)
            {
                vida = 0;
                //print("se destruyo" + collision.gameObject);
                Destroy(collision.gameObject);
                // SceneManager.LoadScene("game_over");
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            vida = vida - danio;
            // print("vida quitada:" + vida);
            // if (vida <= 0)
            // {
            //     vida = 0;
            //     //print("se destruyo" + collision.gameObject);
                // Destroy(collision.gameObject);
            //     // SceneManager.LoadScene("game_over");
            // }
        }
        else if (collision.gameObject.CompareTag("powervida"))
        {
            // print("vida OBTENIDA ");
            vida = vida + 10;
            // print("vida ya curada "+vida);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("pilar"))
        {
            vida = vida - 1;
            // print("vida quitada:" + vida);
        }
        // else if (collision.gameObject.CompareTag("cuerpo")) { }
        else if (collision.gameObject.CompareTag("trofeo"))
        {
            SceneManager.LoadScene("game_over");
        }
        else if (collision.gameObject.CompareTag("METEORO"))
        {
            vida = 0;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("cuerpo"))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            vida = vida - danio;
            // print("vida quitada:" + vida);
            if (vida <= 0)
            {
                vida = 0;
                //print("se destruyo" + collision.gameObject);
                Destroy(collision.gameObject);
                // SceneManager.LoadScene("game_over");
            }
        }
    }

    void velociad()
    {
        counting = true;
    }

    // void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("caer"))
    //     {
    //         // print("caer SALIO");
    //         // Restaurar los valores originales al salir de la colisión
    //         MoveSpeed = contenerdor;
    //         MaxSpeed = contenerdorMax;
    //     }
    //     else if (collision.gameObject.CompareTag("powervelociad"))
    //     {
    //         // print("velocidad ");

    //         Destroy(collision.gameObject);
    //     }
    // }

    // void OnCollisionStay(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("caer"))
    //     {
    //         // print("caer DENTRO");
    //         // Invertir los controles aquí
    //         MoveForce -= transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
    //         transform.position -= MoveForce * Time.deltaTime;
    //     }
    // }
}
