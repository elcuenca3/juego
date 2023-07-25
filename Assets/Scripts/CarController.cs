using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI texto;
    private float freno;
    private float contenerdorMax;
    private float contenerdor;
    private Rigidbody rb;
    public float MoveSpeed = 400;
    public float MaxSpeed = 80;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;

    public float pushForce = 2f;
    private float originalSpeed; // Velocidad original del vehículo

    private Vector3 MoveForce;
    private float tiempoActual;
    public float timepower = 5f;

    private bool counting = false;

    public int vida = 50;
    public Slider healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contenerdor = MoveSpeed;
        contenerdorMax = MaxSpeed;
        freno = 0;
        tiempoActual = timepower;
        texto.text = "" + vida;
        // print("Vida actual : "+vida);
    }

    void Update()
    {
        // texto.text = "" + vida;
        // Si se presiona la barra espaciadora, se detiene el carro
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (counting)
        {
            tiempoActual -= Time.deltaTime;
        }
        if (tiempoActual <= 0)
        {
            counting = false;
            tiempoActual = timepower;
            MaxSpeed = contenerdorMax;
            MoveSpeed = contenerdor;
        }

        // Moving
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;
        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(
            Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime
        );
        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);
        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        MoveForce =
            Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime)
            * MoveForce.magnitude;
        // print(tiempoActual);
        healthBar.value = vida;
    
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("caer"))
        {
            // print("caer ENTRO");
            // Invertir los controles aquí
            MoveForce -= transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position -= MoveForce * Time.deltaTime;
        }
        else if (collision.gameObject.CompareTag("powervelociad"))
        {
            // print("velocidad OBTENIDA ");
            MoveSpeed = 2 * contenerdor;
            MaxSpeed = 2 * contenerdor;
            velociad();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("powervida"))
        {
            print("vida OBTENIDA ");
            vida = vida + 5;
            // print("vida ya curada "+vida);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("pilar"))
        {
            vida = vida - 5;
            print("vida quitada:" + vida);
        }
        else if (collision.gameObject.CompareTag("cuerpo"))
        {
            vida = vida - 5;
            print("vida quitada: npc" + vida);

            if (vida <= 0)
            {
                print("se destruyo" + collision.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }

    void velociad()
    {
        counting = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("caer"))
        {
            // print("caer SALIO");
            // Restaurar los valores originales al salir de la colisión
            MoveSpeed = contenerdor;
            MaxSpeed = contenerdorMax;
        }
        else if (collision.gameObject.CompareTag("powervelociad"))
        {
            // print("velocidad ");

            Destroy(collision.gameObject);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("caer"))
        {
            // print("caer DENTRO");
            // Invertir los controles aquí
            MoveForce -= transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position -= MoveForce * Time.deltaTime;
        }
    }
}
