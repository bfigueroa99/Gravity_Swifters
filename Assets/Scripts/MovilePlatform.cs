using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimientos;
    [SerializeField] private float velocidadMovimiento;
    private int siguientePlataforma = 1;
    private bool orderPlataformas = true;

    private List<Rigidbody2D> jugadoresEnContacto = new List<Rigidbody2D>();
    private Vector2 lastPlatformPosition;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }

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

        // Calcular la diferencia entre la posición actual y la posición anterior de la plataforma
        Vector2 platformMovement = (Vector2)transform.position - lastPlatformPosition;

        // Actualizar la posición de todos los jugadores en contacto con la plataforma
        foreach (var jugador in jugadoresEnContacto)
        {
            jugador.position += platformMovement;
        }

        // Guardar la posición actual de la plataforma para el siguiente cuadro
        lastPlatformPosition = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimientos[siguientePlataforma].position, velocidadMovimiento * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D jugadorRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (jugadorRB != null && !jugadoresEnContacto.Contains(jugadorRB))
            {
                jugadoresEnContacto.Add(jugadorRB);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D jugadorRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (jugadorRB != null && jugadoresEnContacto.Contains(jugadorRB))
            {
                jugadoresEnContacto.Remove(jugadorRB);
            }
        }
    }
}