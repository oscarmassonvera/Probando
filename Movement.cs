using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velMov;
    [Range(0,0.3f)][SerializeField] private float suavizadoMv;
    private Vector3 vel = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Animator")]
    private Animator anin;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;

    private bool salto = false;


    [Header("Dash")]
    [SerializeField] private float velDash;
    [SerializeField] private float tiempDash;
    private float gravInicial;
    public bool puedeHacerDash = true;
    public bool sePuedeMover = true;

    [SerializeField] private float tiempoEntreDash;
    [SerializeField] private float timerDash = 0f;

    [Header("Hit")]
    [SerializeField] private Vector2 velocidadRebote;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anin = GetComponent<Animator>();
        gravInicial = rb.gravityScale;
    }

    private void Update()
    {

            movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velMov;
            if (movimientoHorizontal != 0)
            {
                anin.SetBool("Run", true);
            }
            else
            {
                anin.SetBool("Run", false);
            }
            if (Input.GetButtonDown("Jump"))
            {
                salto = true;
            }
            if (enSuelo)
            {
                anin.SetBool("Jump", false);
            }

            
                timerDash -= Time.deltaTime;
            

            if (Input.GetKeyDown(KeyCode.X) && puedeHacerDash && timerDash <= 0)
            {
                    StartCoroutine(Dash());   
            }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position,dimensionesCaja,0f,queEsSuelo);
        if (sePuedeMover) 
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        }
        salto = false;
    }

    private void Mover(float mover, bool saltar) 
    {
        Vector3 velocidadObjetivo = new Vector3(mover,rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref vel, suavizadoMv);
        if (mover >0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha) 
        {
            Girar();            
        }
        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb.AddForce(new Vector2(0f, fuerzaSalto));
            anin.SetBool("Jump", true);
        }
    }

    private IEnumerator Dash() 
    {

        timerDash = tiempoEntreDash;


        sePuedeMover = false;
        puedeHacerDash = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2 ( velDash * transform.localScale.x, 0f);
        anin.SetTrigger("Dash");
        yield return new WaitForSeconds(tiempDash);
        sePuedeMover = true;
        puedeHacerDash = true;
        rb.gravityScale = gravInicial;
    }

    public void Rebote(Vector2 puntoGolpe) 
    {
        rb.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }

    private void Girar() 
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position,dimensionesCaja);
    }
}
