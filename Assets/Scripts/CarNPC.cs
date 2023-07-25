using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CarNPC : MonoBehaviour
{
    public float followDistance = 50f; // Distancia a la cual el NPC comenzará a seguir al jugador
    public float moveSpeed = 50f; // Velocidad de movimiento del NPC
    public float rotationSpeed = 2f; // Velocidad de rotación del NPC

    private Transform currentTarget;
    private Vector3 randomTarget;

    private float tiempoActual;
    public float timepower = 5f;
    private int vida = 100;
    private bool counting = false;

    private void Start()
    {
        randomTarget = GetRandomTarget(); // Obtiene un objetivo aleatorio inicial
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
    }

    private Vector3 GetRandomTarget()
    {
        // Obtiene una posición aleatoria dentro de un área específica (arena)
        float x = Random.Range(-10f, 20f);
        float z = Random.Range(-10f, 20f);
        return new Vector3(x, 0f, z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("caer"))
        {
            // print("caer ENTRO");
            // Invertir los controles aquí
        }
        else if (collision.gameObject.CompareTag("powervida"))
        {
            print("vida OBTENIDA ");
            vida = vida + 10;
            // print("vida ya curada "+vida);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("pilar"))
        {
            vida = vida - 10;
            print("vida quitada:" + vida);
        }
                else if (collision.gameObject.CompareTag("cuerpo"))
        {
            vida = vida - 10;
            print("vida quitada:" + vida);
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
