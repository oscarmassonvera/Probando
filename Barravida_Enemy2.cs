using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barravida_Enemy2 : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }


    public void CambiarVidaActualEnemy2(float cambiarVida)
    {
        slider.value = cambiarVida;
    }

    public void InicializarBarraDVidaEnemy2(float cantidadVida)
    {
        CambiarVidaActualEnemy2(cantidadVida);
    }
}
