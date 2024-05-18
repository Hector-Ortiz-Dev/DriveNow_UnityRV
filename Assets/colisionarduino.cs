using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class colisionarduino : MonoBehaviour
{
    SerialPort serialPort;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisi�n detectada con: " + collision.gameObject.name);

        // Env�a una se�al a Arduino
        try
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.WriteLine("1");
                Debug.Log("Se�al enviada a Arduino.");
            }
            else
            {
                Debug.LogError("Puerto serial no est� abierto. No se pudo enviar la se�al.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al enviar la se�al: " + e.Message);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        // Configura el puerto serial para comunicarse con Arduino
        serialPort = new SerialPort("COM4", 9600);

        try
        {
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                Debug.Log("Puerto serial abierto correctamente.");
            }
            else
            {
                Debug.LogError("No se pudo abrir el puerto serial.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al abrir el puerto serial: " + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        // Cierra el puerto serial cuando el objeto se destruye
        serialPort.Close();
    }
}
