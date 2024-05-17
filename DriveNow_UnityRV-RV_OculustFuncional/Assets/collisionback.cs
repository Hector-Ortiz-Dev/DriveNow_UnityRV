using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionback : MonoBehaviour
{
    // Ejemplo de uso en otro script (ColisionesScript por ejemplo)
    public class ColisionesScript : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            // Enviar datos al Arduino cuando ocurre una colisión
            //SerialManager.Instance.SendData("1\n"); // Enviar '2' al Arduino
            Debug.Log("colision back");
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
