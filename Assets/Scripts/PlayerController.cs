using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;

    private float originalSpeed; // Velocidad original del vehículo

    private Vector3 MoveForce;

    void Update()
    {
        // Moving
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;
        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(
            Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime
        );
        // Drag and max speed linit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);
        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        MoveForce =
            Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime)
            * MoveForce.magnitude;
    }

    public void ApplySpeedBoost(float speedBoost)
    {
        MoveSpeed *= speedBoost; // Aplicar el incremento de velocidad
    }

    public void RestoreSpeed()
    {
        MoveSpeed = originalSpeed; // Restaurar la velocidad original
    }

    private void Start()
    {
        originalSpeed = MoveSpeed; // Guardar la velocidad original
    }
    /*
        public float speed = 20;
        public float turnSpeed = 30;
        public float horizontalInput;
        public float forwardInput;
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            horizontalInput=Input.GetAxis("Horizontal");
            forwardInput=Input.GetAxis("Vertical");
    
    
    
        //transform.Translate(0,0,1);
        //transform.Translate(Vector3.forward);
        //Bajarle la velocidad al vehiculo
        //transform.Translate(Vector3.forward*Time.deltaTime*10);
        transform.Translate(Vector3.forward*Time.deltaTime * speed * forwardInput );
        //transform.Translate(Vector3.right*Time.deltaTime * turnSpeed * horizontalInput);
    
        transform.Rotate(Vector3.up*Time.deltaTime * turnSpeed * horizontalInput);
    
        }
        */
}
