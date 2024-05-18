using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionesAuto : MonoBehaviour
{
    // Este método se llama cuando otro Collider entra en contacto con el Collider de este objeto
    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Colisión1");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("hola start");

        // Puedes realizar inicializaciones aquí si es necesario
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("hola update");

        // Puedes realizar actualizaciones de lógica de juego aquí si es necesario
    }
}
