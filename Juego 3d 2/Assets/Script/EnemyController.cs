using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float enemigoVidas = 5f;
    private float cooldownAtaque = 2f;
    private float timerAtaque = 0f;

    void Update()
    {
        if (timerAtaque > 0f)
        {
            timerAtaque -= Time.deltaTime;
        }

        if (player == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia < 1.5f && timerAtaque <= 0f)
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

        PerseguirJugador();
    }

    void PerseguirJugador()
    {
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
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
            PlayerInteraction pi = other.GetComponent<PlayerInteraction>();
            if (pi != null)
            {
                if (pi.ObtenerCantidadMonedas() > 0)
                {
                    pi.PerderAlma();
                }
                else
                {
                    PlayerController pc = other.GetComponent<PlayerController>();
                    if (pc != null)
                    {
                        pc.MorirPorAlmas();
                    }
                }
                timerAtaque = cooldownAtaque;
            }
        }
    }
}