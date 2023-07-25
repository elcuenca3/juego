using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerups : MonoBehaviour
{
    public float speedBoost = 2f; // Multiplicador de velocidad para la mejora
    public float powerUpDuration = 4f; // Duración de la mejora en segundos

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisionó es un vehículo
        PlayerController vehicle = other.GetComponent<PlayerController>();
        if (vehicle != null)
        {
            // Aplicar la mejora de velocidad al vehículo
            vehicle.ApplySpeedBoost(speedBoost);

            // Desactivar el objeto de power-up
            gameObject.SetActive(false);

            // Iniciar una corrutina para restaurar la velocidad normal después de un tiempo
            StartCoroutine(RestoreSpeed(vehicle));
        }
    }

    private System.Collections.IEnumerator RestoreSpeed(PlayerController vehicle)
    {
        // Esperar durante la duración de la mejora
        yield return new WaitForSeconds(powerUpDuration);

        // Restaurar la velocidad normal del vehículo
        vehicle.RestoreSpeed();

        // Activar nuevamente el objeto de power-up
        gameObject.SetActive(true);
    }
}
