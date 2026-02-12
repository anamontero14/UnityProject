using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float enemigoVidas = 5f;

    [Header("Comportamiento")]
    public float rangoDeteccion = 10f;  // Distancia a la que detecta al jugador
    public float rangoAtaque = 1.5f;    // Distancia para atacar

    private float cooldownAtaque = 2f;
    private float timerAtaque = 0f;
    private bool jugadorDetectado = false;

    void Update()
    {
        if (timerAtaque > 0f)
        {
            timerAtaque -= Time.deltaTime;
        }

        if (player == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        // Detectar si el jugador está en rango
        if (distancia <= rangoDeteccion)
        {
            jugadorDetectado = true;

            // Si está muy cerca, atacar
            if (distancia < rangoAtaque && timerAtaque <= 0f)
            {
                Atacar();
            }

            // Perseguir mientras esté en rango
            PerseguirJugador();
        }
        else
        {
            // Si sale del rango, dejar de perseguir
            jugadorDetectado = false;
            if (agent != null && agent.hasPath)
            {
                agent.ResetPath(); // Detener el movimiento
            }
        }
    }

    void PerseguirJugador()
    {
        if (agent != null && player != null && jugadorDetectado)
        {
            agent.SetDestination(player.position);
        }
    }

    void Atacar()
    {
        PlayerInteraction pi = player.GetComponent<PlayerInteraction>();
        if (pi != null)
        {
            if (pi.ObtenerCantidadMonedas() > 0)
            {
                pi.PerderAlma();
            }
            else
            {
                PlayerController pc = player.GetComponent<PlayerController>();
                if (pc != null)
                {
                    pc.MorirPorAlmas();
                }
            }
            timerAtaque = cooldownAtaque;
        }
    }

    public void RecibirDaño(float damage)
    {
        enemigoVidas -= damage;

        if (enemigoVidas <= 0f)
        {
            Morir();
        }
    }

    void Morir()
    {
        if (agent != null) agent.enabled = false;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && timerAtaque <= 0f)
        {
            Atacar();
        }
    }

    // Para visualizar el rango de detección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}