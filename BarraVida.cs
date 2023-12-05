using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarraVida : MonoBehaviour
{
    public Slider sli;

    // Start is called before the first frame update
    void Start()
    {
        sli = GetComponent<Slider>();
    }

    public void CambiarVidaActual( float cambiarVida) 
    {
        sli.value = cambiarVida;
    }

    public void InicializarBarraDVida( float cantidadVida)
    {
        CambiarVidaActual(cantidadVida);
    }

}
