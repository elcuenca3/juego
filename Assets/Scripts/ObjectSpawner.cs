using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public GameObject[] powerUpsToSpawn;
    public float spawnDelay = 30f;
    public float powerUpSpawnDelay = 60f;
    private bool playersHaveObject = false;

    // Lista para almacenar las posiciones de los objetos instanciados
    private System.Collections.Generic.List<Vector3> spawnPositions = new System.Collections.Generic.List<Vector3>();

    private void Start()
    {
        StartCoroutine(SpawnObjects());
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (!playersHaveObject)
            {
                // Elegir un objeto aleatorio para spawnear
                int randomIndex = Random.Range(0, objectsToSpawn.Length);
                GameObject objectToSpawn = objectsToSpawn[randomIndex];

                // Generar una posición aleatoria dentro de un rango
                Vector3 spawnPosition;
                do
                {
                    spawnPosition = new Vector3(Random.Range(-5f, 90f), 10f, Random.Range(-5f, 90f));
                } while (IsTooCloseToOtherSpawns(spawnPosition));

                // Instanciar el objeto en la posición generada
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                // Agregar la posición a la lista de posiciones de spawns
                spawnPositions.Add(spawnPosition);

                // Esperar el tiempo de spawnDelay
                yield return new WaitForSeconds(spawnDelay);
            }
            else
            {
                // Esperar hasta que los jugadores no tengan el objeto
                yield return new WaitUntil(() => !playersHaveObject);
            }
        }
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            // Elegir un power-up aleatorio para spawnear
            int randomIndex = Random.Range(0, powerUpsToSpawn.Length);
            GameObject powerUpToSpawn = powerUpsToSpawn[randomIndex];

            // Generar una posición aleatoria dentro de un rango
            Vector3 spawnPosition;
            do
            {
                spawnPosition = new Vector3(Random.Range(-5f, 90f), 10f, Random.Range(-5f, 90f));
            } while (IsTooCloseToOtherSpawns(spawnPosition));

            // Instanciar el power-up en la posición generada
            Instantiate(powerUpToSpawn, spawnPosition, Quaternion.identity);

            // Agregar la posición a la lista de posiciones de spawns
            spawnPositions.Add(spawnPosition);

            // Esperar el tiempo de powerUpSpawnDelay
            yield return new WaitForSeconds(powerUpSpawnDelay);
        }
    }

    public void SetPlayersHaveObject(bool value)
    {
        playersHaveObject = value;
    }

    private bool IsTooCloseToOtherSpawns(Vector3 position)
    {
        // Verificar si la nueva posición está muy cerca de alguna posición de spawn anterior
        foreach (Vector3 spawnPos in spawnPositions)
        {
            if (Vector3.Distance(spawnPos, position) < 30f) // Puedes ajustar el valor 5f según tu preferencia
                return true;
        }
        return false;
    }
}