using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newnpc : MonoBehaviour
{
    public float speed = 10f; // Velocidad de movimiento del NPC
    public Transform player; // Referencia al transform del jugador

    private void Update()
    {
        // Calculamos la dirección hacia el jugador
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Movemos al NPC hacia el jugador
        transform.position += directionToPlayer * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si la colisión es con el jugador (u otro objeto específico, si lo deseas)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aquí puedes agregar el comportamiento que deseas que suceda cuando el NPC colisiona con el jugador.
            // Por ejemplo, mostrar un mensaje, reproducir un sonido, quitar vida al jugador, etc.
            Debug.Log("¡El NPC chocó con el jugador!");
        }
    }
}