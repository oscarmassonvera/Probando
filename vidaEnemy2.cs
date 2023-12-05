using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaEnemy2 : MonoBehaviour
{
    [SerializeField] private Barravida_Enemy2 barraVida;
    [SerializeField] private float vida;
    Animator anin;

    private void Start()
    {
        anin = GetComponent<Animator>();
    }
    public void TomarDano(float dano) 
    {
        vida -= dano;
        barraVida.CambiarVidaActualEnemy2(vida);
        anin.SetTrigger("hit");
        if (vida < 0) 
        {
            dano = 0;
            anin.SetTrigger("dead");
            Destroy(gameObject);
        }
    }
}
