using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class colisionfrente : MonoBehaviour
{
    SerialPort serialPort;
    bool colisionDetectada = false;

    void Start()
    {
        // Configura el puerto serial para comunicarse con Arduino
        serialPort = new SerialPort("COM4", 9600);
        serialPort.Open();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si detecta una colisi�n con un objeto
       
            Debug.Log("Colisi�n detectada con: " + collision.gameObject.name);

            // Env�a una se�al a Arduino
            serialPort.WriteLine("1");

                    
        
    }

    void Update()
    {

    }

    void OnDestroy()
    {
        // Cierra el puerto serial cuando el objeto se destruye
        serialPort.Close();
    }
}
