using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;
    

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public BoxCollider volumeCar;
    public BoxCollider volumeFirstLevel;
    public BoxCollider volumeSecondLevel;
    public BoxCollider volumeThirdLevel;
    public bool volumen1 = false;
    public bool volumen2 = false;
    public bool volumen3 = false;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();


        if (volumen1 == true)
        {
            float porcentajeEnVolume = CalcularPorcentajeEnVolume(volumeCar, volumeFirstLevel);
            Debug.Log("Porcentaje en el volumen del nivel 1: " + porcentajeEnVolume + "%");
        }


        if (volumen2 == true)
        {
            float porcentajeEnVolume = CalcularPorcentajeEnVolume(volumeCar, volumeSecondLevel);
            Debug.Log("Porcentaje en el volumen del nivel 2: " + porcentajeEnVolume + "%");
        }

        if (volumen3 == true)
        {
            float porcentajeEnVolume = CalcularPorcentajeEnVolume(volumeCar, volumeThirdLevel);
            Debug.Log("Porcentaje en el volumen del nivel 3: " + porcentajeEnVolume + "%");
        }



    }


    private float CalcularPorcentajeEnVolume(BoxCollider carroCollider, BoxCollider volumenCollider)
    {
        // Calculamos el volumen del �rea de intersecci�n entre los colliders
        float volumenInterseccion = VolumenInterseccion(carroCollider, volumenCollider);

        // Calculamos el volumen total del box collider del carro
        float volumenCarro = carroCollider.size.x * carroCollider.size.y * carroCollider.size.z;

        // Calculamos el porcentaje del carro dentro del volumen
        float porcentajeEnVolume = (volumenInterseccion / volumenCarro) * 100f;

        return porcentajeEnVolume;
    }

    private float VolumenInterseccion(BoxCollider collider1, BoxCollider collider2)
    {
        // Obtenemos la intersecci�n de los bounds de los colliders
        Bounds intersectionBounds = InterseccionBounds(collider1.bounds, collider2.bounds);

        // Calculamos el volumen del �rea de intersecci�n
        float volumenInterseccion = intersectionBounds.size.x * intersectionBounds.size.y * intersectionBounds.size.z;

        return volumenInterseccion;
    }

    private Bounds InterseccionBounds(Bounds bounds1, Bounds bounds2)
    {
        // Calculamos los l�mites de la intersecci�n
        float minX = Mathf.Max(bounds1.min.x, bounds2.min.x);
        float minY = Mathf.Max(bounds1.min.y, bounds2.min.y);
        float minZ = Mathf.Max(bounds1.min.z, bounds2.min.z);
        float maxX = Mathf.Min(bounds1.max.x, bounds2.max.x);
        float maxY = Mathf.Min(bounds1.max.y, bounds2.max.y);
        float maxZ = Mathf.Min(bounds1.max.z, bounds2.max.z);

        // Creamos y devolvemos los bounds de la intersecci�n
        return new Bounds(new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2),
                          new Vector3(maxX - minX, maxY - minY, maxZ - minZ));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == volumeFirstLevel)
        {
            volumen1 = true;
        }

        if (other == volumeSecondLevel)
        {
            volumen2 = true;
        }

        if (other == volumeThirdLevel)
        {
            volumen3 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == volumeFirstLevel)
        {
            volumen1 = false;
        }

        if (other == volumeSecondLevel)
        {
            volumen2 = true;
        }

        if (other == volumeThirdLevel)
        {
            volumen3 = true;
        }

    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

}
