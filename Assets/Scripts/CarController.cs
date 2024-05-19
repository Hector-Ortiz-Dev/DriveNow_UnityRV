using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class CarController : MonoBehaviour
{
    #region VOLANTE
    LogitechGSDK.LogiControllerPropertiesData properties;

    public Transform steeringWheelTransform;
    public float xAxes, GasInput, BreakInput;
    public bool BreakStatus = false, isBackWard = false, isWheelConnected = false;

    public int CurrentGear;
    #endregion

    #region CONTROLES VEHICULO
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;
    #endregion


    #region VICTORIA/DERROTA Y VOLUMEN
    public TextMeshProUGUI textoHUD;
    public BoxCollider volumeCar;
    public BoxCollider volumeFirstLevel;
    public BoxCollider volumeFirstLevelC;
    public BoxCollider volumeFirstLevelC2;
    public BoxCollider volumeSecondLevel;
    public BoxCollider volumeSecondLevelC;
    public BoxCollider volumeSecondLevelC2;
    public BoxCollider volumeThirdLevel;
    public Transform carroTransform;
    public Image imagenGeneral;
    public Image imagenGeneral2;
    public TextMeshProUGUI textoMenu;
    public TextMeshProUGUI textoMenu2;
    private int conteoChoques = 0;
    public bool volumen1 = false;
    public bool volumen1c = false;
    public bool volumen1c2 = false;
    public bool volumen2c = false;
    public bool volumen2c2 = false;
    public bool volumen2 = false;
    public bool volumen3 = false;
    public bool victoria1 = false;
    public bool victoria2 = false;
    public bool victoria3 = false;
    public bool conteoActivado1 = false;
    public bool conteoActivado2 = false;
    public bool conteoActivado3 = false;
    private bool volverAlMenu = false;
    private float tiempoConteo = 0f;
    private float duracionConteo = 5f;
    private float duracionConteonegativo = 5f;
    public bool derrota = false;
    private float duracionConteonegativo2 = 5f;


    public Button btn_victoria1;
    public Button btn_victoria2;
    public Button btn_victoria3;





    #endregion


    #region AUDIOS
    public AudioSource audioInicial;
    public AudioSource audioSource;
    public AudioClip audioInicialC;
    public AudioClip audioVictorial;
    public AudioClip audioCollision;
    public AudioClip audioCarroMovimiento;
    public bool audioVictory = false;
    #endregion



    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();


        if (conteoActivado1)
        {

            Debug.Log("Contando..." + tiempoConteo);

            int tiempoConteoInt = Mathf.FloorToInt(tiempoConteo);

            textoHUD.text = "Manten el carro en esa posición 5 segundos, segundos contados: " + tiempoConteoInt;


            tiempoConteo += Time.deltaTime;
            if (tiempoConteo >= duracionConteo)
            {

                conteoActivado1 = false;
                tiempoConteo = 0f;
                victoria1 = true;

                if (victoria1)
                {
                    audioInicial.clip = audioInicialC;
                    audioInicial.Play();
                    btn_victoria1.gameObject.SetActive(true);
                    textoHUD.text = "";
                    conteoActivado1 = false;
                    volumeFirstLevel.enabled = false;
                }
               
            }
        }



        if (conteoActivado2)
        {

            Debug.Log("Contando..." + tiempoConteo);

            int tiempoConteoInt = Mathf.FloorToInt(tiempoConteo);

            textoHUD.text = "Manten el carro en esa posición 5 segundos, segundos contados: " + tiempoConteoInt;

            tiempoConteo += Time.deltaTime;
            if (tiempoConteo >= duracionConteo)
            {

                conteoActivado2 = false;
                tiempoConteo = 0f;
                victoria2 = true;

                if (victoria2)
                {
                    audioInicial.clip = audioInicialC;
                    audioInicial.Play();
                    textoHUD.text = "";
                    btn_victoria2.gameObject.SetActive(true);
                    conteoActivado2 = false;
                    volumeSecondLevel.enabled = false;
                }

            }
        }

        if (conteoActivado3)
        {

            Debug.Log("Contando..." + tiempoConteo);

            int tiempoConteoInt = Mathf.FloorToInt(tiempoConteo);

            textoHUD.text = "Manten el carro en esa posición 5 segundos, segundos contados: " + tiempoConteoInt;

            tiempoConteo += Time.deltaTime;
            if (tiempoConteo >= duracionConteo)
            {

                conteoActivado3 = false;
                tiempoConteo = 0f;
                victoria3 = true;

                if (victoria3)
                {

                    audioInicial.clip = audioInicialC;
                    audioInicial.Play();
                    textoHUD.text = "";
                    btn_victoria3.gameObject.SetActive(true);
                    conteoActivado1 = false;
                    volumeThirdLevel.enabled = false;
                }

            }
        }


        if (volumen1 == true && victoria1 == false)
        {
            float porcentajeEnVolume = CalcularPorcentajeEnVolume(volumeCar, volumeFirstLevel);
            Debug.Log("Porcentaje en el volumen del nivel 1: " + porcentajeEnVolume + "%");


            Vector3 angulos = carroTransform.eulerAngles;


          if ((!volumen1c && !volumen1c2))
            {
                if (porcentajeEnVolume >= 95f && porcentajeEnVolume <= 130f)
                {
                    conteoActivado1 = true;
                }
                else
                {
                    conteoActivado1 = false;
                    tiempoConteo = 0f;
                }
            }
        }


        if (volumen2 == true && victoria2 == false)
        {

            Vector3 angulos = carroTransform.eulerAngles;

            float porcentajeEnVolume = CalcularPorcentajeEnVolume(volumeCar, volumeSecondLevel);
            Debug.Log("Porcentaje en el volumen del nivel 2: " + porcentajeEnVolume + "%");

           if (!volumen2c && !volumen2c2)
            {

                if (porcentajeEnVolume >= 95f && porcentajeEnVolume <= 130f)
                {
                    conteoActivado2 = true;
                }
                else
                {
                    conteoActivado2 = false;
                    tiempoConteo = 0f;
                }
            }

        }

        if (volumen3 == true && victoria3 == false)
        {
            float porcentajeEnVolume = CalcularPorcentajeEnVolume(volumeCar, volumeThirdLevel);
            Debug.Log("Porcentaje en el volumen del nivel 3: " + porcentajeEnVolume + "%");


            Vector3 angulos = carroTransform.eulerAngles;

         //  if (angulos.y >= -5 && angulos.y <= 5)
            {

                if (porcentajeEnVolume >= 95f && porcentajeEnVolume <= 130f)
                {
                    conteoActivado3 = true;
                }
                else
                {
                    conteoActivado3 = false;
                    tiempoConteo = 0f;
                }
            }

        }


        if (volverAlMenu)
        {
            string sceneName = "GameMenuScene";

            SceneManager.LoadScene(sceneName);
        }

        if (victoria1 && victoria2 && victoria3)
        {

            if (!audioInicial.isPlaying && !audioVictory)
            {
                audioInicial.clip = audioVictorial;
                audioInicial.Play();
                audioVictory = true;
            }
       

            imagenGeneral.gameObject.SetActive(true);

            int tiempoConteoInt = Mathf.FloorToInt(duracionConteonegativo);

            textoHUD.text = "";
            textoMenu.text = "Se volvera al menú principal en : " + tiempoConteoInt + " segundos";

            btn_victoria1.gameObject.SetActive(false);
            btn_victoria2.gameObject.SetActive(false);
            btn_victoria3.gameObject.SetActive(false);

            duracionConteonegativo -= Time.deltaTime;

            if (duracionConteonegativo <= 0 || volverAlMenu)
            {
                volverAlMenu = true;
            }

        }

        if (conteoChoques >= 10)
        {
            imagenGeneral2.gameObject.SetActive(true);

            int tiempoConteoInt = Mathf.FloorToInt(duracionConteonegativo2);

            textoHUD.text = "";
            textoMenu2.text = "Se volvera al menú principal en : " + tiempoConteoInt + " segundos";

            btn_victoria1.gameObject.SetActive(false);
            btn_victoria2.gameObject.SetActive(false);
            btn_victoria3.gameObject.SetActive(false);

            duracionConteonegativo2 -= Time.deltaTime;

            if (duracionConteonegativo2 <= 0 || volverAlMenu)
            {
                volverAlMenu = true;
            }
        }



    }


    private float CalcularPorcentajeEnVolume(BoxCollider carroCollider, BoxCollider volumenCollider)
    {
        // Calculamos el volumen del área de intersección entre los colliders
        float volumenInterseccion = VolumenInterseccion(carroCollider, volumenCollider);

        // Calculamos el volumen total del box collider del carro
        float volumenCarro = carroCollider.size.x * carroCollider.size.y * carroCollider.size.z;

        // Calculamos el porcentaje del carro dentro del volumen
        float porcentajeEnVolume = (volumenInterseccion / volumenCarro) * 100f;


            return porcentajeEnVolume;
    }


  


    private float VolumenInterseccion(BoxCollider collider1, BoxCollider collider2)
    {
        // Obtenemos la intersección de los bounds de los colliders
        Bounds intersectionBounds = InterseccionBounds(collider1.bounds, collider2.bounds);

        // Calculamos el volumen del área de intersección
        float volumenInterseccion = intersectionBounds.size.x * intersectionBounds.size.y * intersectionBounds.size.z;

        return volumenInterseccion;
    }

    private Bounds InterseccionBounds(Bounds bounds1, Bounds bounds2)
    {
        // Calculamos los límites de la intersección
        float minX = Mathf.Max(bounds1.min.x, bounds2.min.x);
        float minY = Mathf.Max(bounds1.min.y, bounds2.min.y);
        float minZ = Mathf.Max(bounds1.min.z, bounds2.min.z);
        float maxX = Mathf.Min(bounds1.max.x, bounds2.max.x);
        float maxY = Mathf.Min(bounds1.max.y, bounds2.max.y);
        float maxZ = Mathf.Min(bounds1.max.z, bounds2.max.z);

        // Creamos y devolvemos los bounds de la intersección
        return new Bounds(new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2),
                          new Vector3(maxX - minX, maxY - minY, maxZ - minZ));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == volumeFirstLevel)
        {
            volumen1 = true;

        }

        if (other == volumeSecondLevel && victoria2 == false)
        {
            volumen2 = true;

        }

        if (other == volumeThirdLevel && victoria3 == false)
        {
            volumen3 = true;
        }

        if (other == volumeFirstLevelC)
        {
            volumen1c = true;
        }


        if (other == volumeFirstLevelC)
        {
            volumen1c2 = true;
        }

        if (other == volumeSecondLevelC)
        {
            volumen2c = true;
        }


        if (other == volumeSecondLevelC2)
        {
            volumen2c2 = true;
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
            volumen2 = false;
        }

        if (other == volumeThirdLevel)
        {
            volumen3 = false;
        }


        if (other == volumeFirstLevelC)
        {
            volumen1c = false;
        }


        if (other == volumeFirstLevelC)
        {
            volumen1c2 = false;
        }


        if (other == volumeSecondLevelC)
        {
            volumen2c = false;
        }


        if (other == volumeSecondLevelC2)
        {
            volumen2c2 = false;
        }


    }


    private void GetInput()
    {

        //Volante
        if (isWheelConnected == true)
        {
            //Giros de Volante
            horizontalInput = xAxes;
            //Freno
            isBreaking = BreakStatus;

            //Avanzar o retroceder
            if (isBackWard == false)
            {
                verticalInput = GasInput;
            }
            else if (isBackWard == true)
            {
                verticalInput = GasInput * -1;
            }
        }
        else if (isWheelConnected == false) //TECLADO
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            isBreaking = Input.GetKey(KeyCode.Space);
        }



        if ((horizontalInput != 0 || verticalInput != 0) && !audioSource.isPlaying)
        {
            audioSource.clip = audioCarroMovimiento;
            audioSource.Play();
        }

        if (isBreaking && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

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

        UpdateSteeringWheel();
    }

    private void UpdateSteeringWheel()
    {
        if (steeringWheelTransform != null)
        {
            steeringWheelTransform.localRotation = Quaternion.Euler(-65, 0, 0) * Quaternion.Euler(0, steerAngle, 0);
        }
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }


    void OnCollisionEnter(Collision collision)
    {
        // Reproduce el audio de colisión
        if (audioCollision != null)
        {
            conteoChoques++;
            Debug.Log("Choques: " + conteoChoques);
            audioInicial.clip = audioCollision;
            audioInicial.Play();
        }
    }


    public void Start()
    {
        btn_victoria1.gameObject.SetActive(false);
        imagenGeneral.gameObject.SetActive(false);
        imagenGeneral2.gameObject.SetActive(false);
        btn_victoria2.gameObject.SetActive(false);
        btn_victoria3.gameObject.SetActive(false);

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

            //Vibracion volante
            if (LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_SPRING))
            {
                LogitechGSDK.LogiStopSpringForce(0);

                //LogitechGSDK.LogiPlaySpringForce(0, 5, 5, 5);
            }
            LogitechGSDK.LogiPlaySpringForce(0, 5, 5, 5);
            LogitechGSDK.LogiStopSpringForce(0);

            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);


            //Giro Volante
            xAxes = rec.lX / 32560f; //-1 0 1
            xAxes = xAxes * 4;

            //Presion Acelerador (Simple)
            if (rec.lY > 0)
            {
                GasInput = 0;
            }
            else if (rec.lY < 0)
            {
                GasInput = rec.lY / -32768f;
            }


            //Presion Freno
            //32767 a -32768
            if (rec.lRz > 32600) //Antes de X valor no es freno
            {
                BreakInput = 0;
                BreakStatus = false;
            }
            else if (rec.lRz < 32600) //Despues de x Valor ya es freno
            {
                // x - factor y Entre mas chico el numero dividio entre el factor sera 1
                BreakInput = rec.lRz / -32768f;
                BreakStatus = true;
            }
            //BreakInput = rec.lRz;

        }
        else
        {
            print("No steering Wheel connected");
            isWheelConnected = false;
        }

        //Cambio de direccion
        if (LogitechGSDK.LogiButtonTriggered(0, 4))
        {
            isBackWard = false;
        }
        else if (LogitechGSDK.LogiButtonTriggered(0, 5))
        {
            isBackWard = true;
        }


    }
    // Use this for shutdown 
    void Stop()
    {
        LogitechGSDK.LogiSteeringShutdown();
    }
    
}
