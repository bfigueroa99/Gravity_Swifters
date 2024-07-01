using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform jugador;
    private bool mirandoDerecha = true;
    public static bool bossDied = false;
    public static bool triggeredBossFight = false;
    public static bool bossReceivedDamage = false;
    public Camera_moon cameraScript;  
    public ItemSpawner itemSpawner;
    [SerializeField] private GameObject Malachi;

    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] private BarradeVida barraDeVida;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private int dañoAtaque;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        barraDeVida.InicializarBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraScript = Camera.main.GetComponent<Camera_moon>();  
        triggeredBossFight = true;
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
        StartCoroutine(ResetTriggeredBossFight());
    }

    public void RecibirDaño(float daño)
    {
        vida -= daño;
        bossReceivedDamage = true;
        StartCoroutine(ResetBossReceivedDamage());
        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            StartCoroutine(ActivarMuerte());
        }
    }

    private IEnumerator ActivarMuerte()
    {
        animator.SetTrigger("Muerte");
        cameraScript.FocusOnTarget(transform, 5f);  
        yield return new WaitForSeconds(5f);
        Muerte();
    }

    public void Muerte()
    {
        itemSpawner.StopSpawning();
        Malachi.SetActive(true);
        bossDied = true;
        Destroy(gameObject);
        cameraScript.FocusOnTarget(jugador, 0f);

    }

    public void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha)|| (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Atacar()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);
        foreach (Collider2D colisions in objetos)
        {
            if (colisions.CompareTag("Player"))
            {
                colisions.GetComponent<PlayerController>().TakeDamage(dañoAtaque);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }

    IEnumerator ResetTriggeredBossFight()
    {
        yield return new WaitForSeconds(3f); 
        triggeredBossFight = false;
    }

    IEnumerator ResetBossReceivedDamage()
    {
        yield return new WaitForSeconds(0.1f); 
        bossReceivedDamage = false;
    }
}
