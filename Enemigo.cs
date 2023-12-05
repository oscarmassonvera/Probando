using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;

    public Rigidbody2D rb;
    public Transform jugador;
    private bool mirandoDerecha = true;

    [Header("Atake")]
    [SerializeField] private Transform controladorAtake;
    [SerializeField] private float radioAtake;
    [SerializeField] private float danoAtake;



    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador",distanciaJugador);
    }

    public void TomarDano(float dano) 
    {
        vida -= dano;
        if (vida > 0) 
        {
            animator.SetTrigger("Hit");       
        }
        if (vida <= 0) 
        {
            Muerte();
        }
    }
    private void Muerte() 
    {
        animator.SetTrigger("Muerte");
        Destroy(gameObject);
    }
    public void MirarJugador() 
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha)) 
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y +180, 0);
        }
    }
    private void Ataque() 
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtake.position,radioAtake);
        foreach (Collider2D collision in objetos) 
        {
            if (collision.CompareTag("Player")) 
            {
                collision.GetComponent<CombateJugador>().TomarDano(danoAtake, jugador.transform.position);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtake.position,radioAtake);
    }
}
