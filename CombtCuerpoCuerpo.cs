using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombtCuerpoCuerpo : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danoGolpe;
    [SerializeField] private float tiempoEntreAtaque;
    [SerializeField] private float tiempoSigAtaque;

    Animator anin;
    private void Start()
    {
        anin = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tiempoSigAtaque > 0) 
        {
            tiempoSigAtaque -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E) && tiempoSigAtaque <= 0) 
        {
            anin.SetTrigger("Atk1");
            Golpe();
            tiempoSigAtaque = tiempoEntreAtaque;
        }
    }

    private void Golpe() 
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos) 
        {
            if (colisionador.CompareTag("Enemigo")) 
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDano(danoGolpe);
            }
            else if (colisionador.CompareTag("Enemigo2"))
            {
                colisionador.transform.GetComponent<vidaEnemy2>().TomarDano(danoGolpe);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position,radioGolpe);
    }
}
