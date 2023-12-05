using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;

public class enemy2 : MonoBehaviour
{
    [SerializeField] Transform rayoTrans;
    [SerializeField] private float distanciaDeteccion;
    [SerializeField] Animator anin;
    [SerializeField] private float tmAtk1;
    [SerializeField] private float tiempoEntreAtake;
    [SerializeField] Transform playerTransform;
    [Header("Persecucion")]
    private bool flip;
    [SerializeField] private float speed;
    [SerializeField] private float suavizado;
    [SerializeField] private Transform detector;
    [SerializeField] private float radioDetector;
    private bool mirandoDerecha = true;
    [Header("Atakes")]
    [SerializeField] private Transform personaje;
    [SerializeField] private Transform Atk1;
    [SerializeField] private Transform Atk2;
    [SerializeField] private Transform Atk3;
    [SerializeField] private Transform AtkSp;
    [SerializeField] private float RAtk1;
    [SerializeField] private float RAtk2;
    [SerializeField] private float RAtk3;
    [SerializeField] private float RAtkSp;
    [SerializeField] private float danoAtake1;
    [SerializeField] private float danoAtake2;
    [SerializeField] private float danoAtake3;
    [SerializeField] private float danoAtakeSp;

   

    private void Start()
    {
        anin = GetComponent<Animator>();
        tiempoEntreAtake = 0f;
    }

    public void Update()
    {
        //RaycastHit2D vistaDerecha = Physics2D.Raycast(rayoTransDere.transform.position, rayoTrans.transform.right, 5f);
        //RaycastHit2D vistaIzquierda = Physics2D.Raycast(rayoTransIzq.transform.position, - rayoTrans.transform.right, 5f
        //Debug.DrawRay(rayoTransDere.transform.position, rayoTrans.transform.right * 5f, Color.red);
        //Debug.DrawRay(rayoTransIzq.transform.position, -rayoTrans.transform.right * 5f, Color.red);
        tiempoEntreAtake -= Time.deltaTime;
        //if (vistaDerecha.collider != CompareTag("Personaje") || vistaIzquierda.collider != CompareTag("Personaje"))
        //{
        //}
        //If something was hit, the RaycastHit2D.collider will not be null.
        //if (hit.collider != CompareTag("Personaje"))
        //{
        //    Debug.DrawRay(transform.position, transform.right * distanciaDeteccion, Color.green);
        //}
        //if (hit.collider != null)
        //{
        //    Debug.Log("Distancia :" + hit.distance);
        //    Debug.Log("Punto de impacto :" + hit.point);
        //    Debug.Log("collider :" + hit.collider);
        //    Debug.Log("centroid :" + hit.centroid);
        //    Debug.Log("transform :" + hit.transform)
        //}
  
            Detectar();
    }
    private void Detectar()
    {
        anin.SetBool("Correr", false);
        Collider2D[] objetos = Physics2D.OverlapCircleAll(detector.position, radioDetector);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Personaje"))
            {

                if (playerTransform.position.x < transform.position.x && tiempoEntreAtake <= 0f)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    anin.SetBool("Correr",true);
                    float newPosition = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x + 1, ref suavizado, speed);
                    transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
                    
                    aleatorio();
                }
                else if (playerTransform.position.x > transform.position.x && tiempoEntreAtake <= 0f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    anin.SetBool("Correr", true);
                    float newPosition = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x - 1, ref suavizado, speed);
                    transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);

                    aleatorio();
                }
            }
        }
    }


    private void aleatorio()
    {
        int randomNumber = Random.Range(0, 4);
        switch (randomNumber)
        {
            case 0:
                Atake1();
                break;
            case 1:
                Atake2();
                break;
            case 2:
                Atake3();

                break;
            case 3:
                AtakeEspecial();
                break;
            default:
                break;
        }
    }


    private void tomarDaño1() 
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll( Atk1.position, RAtk1);
        foreach (Collider2D colision in objetos) 
        {
            if (colision.CompareTag("Personaje")) 
            {
                colision.GetComponent<CombateJugador>().TomarDano(danoAtake1, colision.transform.position);
            }
        }
    }


    private void tomarDaño2()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(Atk2.position, RAtk2);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Personaje"))
            {
                colision.GetComponent<CombateJugador>().TomarDano(danoAtake2, colision.transform.position);
            }
        }
    }
    private void tomarDaño3()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(Atk3.position, RAtk3);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Personaje"))
            {
                colision.GetComponent<CombateJugador>().TomarDano(danoAtake3, colision.transform.position);
            }
        }
    }
    private void tomarDañoSp()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(AtkSp.position, RAtkSp);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Personaje"))
            {
                colision.GetComponent<CombateJugador>().TomarDano(danoAtakeSp, colision.transform.position);
            }
        }
    }




    private void Atake1() 
    {
        RaycastHit2D hit = Physics2D.Raycast(rayoTrans.transform.position, rayoTrans.transform.right, distanciaDeteccion);
        Debug.DrawRay(rayoTrans.transform.position, rayoTrans.transform.right * distanciaDeteccion, Color.green);
        if (hit.collider != CompareTag("Personaje") && tiempoEntreAtake <= 0f)
        {
            tiempoEntreAtake = tmAtk1;
            anin.SetTrigger("Atk1");
            //Debug.Log("collider :" + hit.collider
        }

    }
    private void Atake2()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayoTrans.transform.position, rayoTrans.transform.right, distanciaDeteccion);
        Debug.DrawRay(rayoTrans.transform.position, rayoTrans.transform.right * distanciaDeteccion, Color.green);
        if (hit.collider != CompareTag("Personaje") && tiempoEntreAtake <= 0f)
        {
            tiempoEntreAtake = tmAtk1;
            anin.SetTrigger("Atk2");
            //Debug.Log("collider :" + hit.collider);
        }

    }
    private void Atake3()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayoTrans.transform.position, rayoTrans.transform.right, distanciaDeteccion);
        Debug.DrawRay(rayoTrans.transform.position, rayoTrans.transform.right * distanciaDeteccion, Color.green);
        if (hit.collider != CompareTag("Personaje") && tiempoEntreAtake <= 0f)
        {
            tiempoEntreAtake = tmAtk1;
            anin.SetTrigger("Atk3");
            //Debug.Log("collider :" + hit.collider);
        }

    }
    private void AtakeEspecial()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayoTrans.transform.position, rayoTrans.transform.right, distanciaDeteccion);
        Debug.DrawRay(rayoTrans.transform.position, rayoTrans.transform.right * distanciaDeteccion, Color.green);
        if (hit.collider != CompareTag("Personaje") && tiempoEntreAtake <= 0f)
        {
            tiempoEntreAtake = tmAtk1;
            anin.SetTrigger("AtkSp");
            //Debug.Log("collider :" + hit.collider);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detector.position, radioDetector);
        Gizmos.DrawWireSphere(Atk1.position, RAtk1);
        Gizmos.DrawWireSphere(Atk2.position, RAtk2);
        Gizmos.DrawWireSphere(Atk3.position, RAtk3);
        Gizmos.DrawWireSphere(AtkSp.position, RAtkSp);
    }
}
