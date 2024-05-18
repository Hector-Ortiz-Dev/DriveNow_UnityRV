using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisionarduino : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
            // Imprime el nombre del objeto con el que colisionamos en la consola
            Debug.Log("Colisión detectada con: " + collision.gameObject.name);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
