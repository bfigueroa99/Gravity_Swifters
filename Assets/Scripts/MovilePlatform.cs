using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimientos;
    [SerializeField] private float velocidadMovimiento;
    private int siguientePlataforma = 1;
    private bool orderPlataformas = true;

    private void Update()
    {
        if (orderPlataformas && siguientePlataforma + 1 >= puntosMovimientos.Length)
        {
            orderPlataformas = false;
        }

        if (!orderPlataformas && siguientePlataforma <= 0)
        {
            orderPlataformas = true;
        }

        if (Vector2.Distance(transform.position, puntosMovimientos[siguientePlataforma].position) < 0.1f)
        {
            siguientePlataforma = orderPlataformas ? siguientePlataforma + 1 : siguientePlataforma - 1;
        }

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimientos[siguientePlataforma].position, velocidadMovimiento * Time.deltaTime);

    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
