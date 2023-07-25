using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class tiempo : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI texto;
    private float tiempoActual;
    public float tiemp = 10f;

    void Start()
    {
        tiempoActual = tiemp;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoActual -= Time.deltaTime;
        texto.text = "" + tiempoActual.ToString("F0");
        print(tiempoActual);
        if (tiempoActual <= 0f)
        {
            CambiarEscena();
        }
    }

    private void CambiarEscena()
    {
        // Cambiar a la siguiente escena

        SceneManager.LoadScene("Game_over");
    }
}
