using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hongo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje")) 
        {
            collision.gameObject.GetComponent<CombateJugador>().TomarDano(20, collision.GetContact(0).normal);
        }
    }
}
