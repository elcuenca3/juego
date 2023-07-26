using UnityEngine;

public class DestruirPlataforma : MonoBehaviour
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
}