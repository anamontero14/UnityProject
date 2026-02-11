using System;
using UnityEngine;
using UnityEngine.AI;

public class MonedasController : MonoBehaviour
{
    private Renderer monedasRenderer;
    public CapsuleCollider colliderMoneda;
    private float cooldownMonedas = 5f;
    private Boolean isCollected = false;

    public Transform player;
    public float distanciaHuida = 5f;
    public float velocidadHuida = 4f;
    private NavMeshAgent agent;

    void Start()
    {
        monedasRenderer = GetComponentInChildren<Renderer>();

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.speed = velocidadHuida;
    }

    void Update()
    {
        if (isCollected)
        {
            cooldownMonedas -= Time.deltaTime;
            if (cooldownMonedas <= 0f)
            {
                isCollected = false;
                cooldownMonedas = 5f;
                monedasRenderer.enabled = true;
                colliderMoneda.enabled = true;
                if (agent != null) agent.enabled = true;
            }
            return;
        }

        if (agent != null && agent.enabled && player != null)
        {
            float distancia = Vector3.Distance(transform.position, player.position);

            if (distancia < distanciaHuida)
            {
                Vector3 direccionHuida = (transform.position - player.position).normalized;
                Vector3 puntoHuida = transform.position + direccionHuida * distanciaHuida;

                if (NavMesh.SamplePosition(puntoHuida, out NavMeshHit hit, distanciaHuida, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
            }
            else
            {
                agent.ResetPath();
            }
        }
    }

    public void DoInteraction()
    {
        isCollected = true;
        monedasRenderer.enabled = false;
        colliderMoneda.enabled = false;
        if (agent != null) agent.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.DoInteraction(gameObject);
            }
        }
    }
}