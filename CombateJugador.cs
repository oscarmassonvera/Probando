using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] private BarraVida barraVida;
    [SerializeField] private float vida;
    [SerializeField] private Movement movement;
    [SerializeField] private float tiempoPerdidaControl;
    Animator anin;


    GameObject tryagain;
    // Start is called before the first frame update
    private void Start()
    {
        movement = GetComponent<Movement>();
        anin = GetComponent<Animator>();
        tryagain = GameObject.Find("Tryagain");
        tryagain.SetActive(false);
    }

    public void TomarDano(float dano, Vector2 posicion) 
    {
        vida -= dano;
        barraVida.CambiarVidaActual(vida);
        anin.SetTrigger("Golpe");
        StartCoroutine(PerderControl());
        movement.Rebote(posicion);
        if (vida <= 0)
        {
            StartCoroutine(Death());
            tryagain.SetActive(true);
        }
    }

    private IEnumerator Death()
    {
        movement.sePuedeMover = false;
        movement.puedeHacerDash = false;
        anin.SetTrigger("Death");
        yield return new WaitForSeconds(1.5f);
        movement.sePuedeMover = true;
        movement.puedeHacerDash = true;
        Destroy(gameObject);
    }

    private IEnumerator PerderControl() 
    {
        movement.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movement.sePuedeMover = true;
    }
}
