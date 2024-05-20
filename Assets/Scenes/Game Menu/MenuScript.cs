using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //
    LogitechGSDK.LogiControllerPropertiesData properties;

    public Transform steeringWheelTransform;
    public float xAxes, GasInput, BreakInput;
    public bool BreakStatus = false, isBackWard = false, isWheelConnected = false;
    //

    public void Comenzar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        // Si estamos en el editor, simplemente detenemos el modo de juego
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //CONTROLAR MENU CON VOLANTE
    public void Start()
    {
        //Configuraciones VOLANTE
        // Use this for initialization 
        //not ignoring xinput in this example 
        LogitechGSDK.LogiSteeringInitialize(false);
        LogitechGSDK.LogiStopSpringForce(0);
        LogitechGSDK.LogiStopConstantForce(0);
        LogitechGSDK.LogiStopDamperForce(0);
        LogitechGSDK.LogiStopDirtRoadEffect(0);

        LogitechGSDK.LogiPlaySpringForce(0, 5, 5, 5);
        LogitechGSDK.LogiGenerateNonLinearValues(0, -100);

    }

    // Update is called once per frame 
    void Update()
    {

        //Recibir inputs
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            isWheelConnected = true;

            //Vibracion volante (APAGAR)
            if (LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_SPRING))
            {
                LogitechGSDK.LogiStopSpringForce(0);

                //LogitechGSDK.LogiPlaySpringForce(0, 5, 5, 5);
            }
            LogitechGSDK.LogiPlaySpringForce(0, 5, 5, 5);
            LogitechGSDK.LogiStopSpringForce(0);

            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);


           

            //Presion Acelerador (Comenzar)
            if (rec.lY < 0)
            {
                Comenzar();
            }


            //Presion Freno (Salir)
            if (rec.lRz < 32600) //Despues de x Valor ya es freno
            {
                Salir();
            }
         
        }
        else
        {
            print("No steering Wheel connected");
            isWheelConnected = false;
        }


    }
    // Use this for shutdown 
    void Stop()
    {
        //LogitechGSDK.LogiSteeringShutdown();
    }

}
