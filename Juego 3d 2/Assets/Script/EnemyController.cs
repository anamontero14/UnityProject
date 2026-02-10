using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float enemigoVidas = 5f;

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, player.position);
        Vector3 direccion = (player.position - transform.position).normalized;

        if (distancia < 15f)
        { // Rango de visión
            if (Physics.Raycast(transform.position, direccion, out RaycastHit hit, 15f))
            {
                /*if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("¡Jugador detectado!");
                    // Aquí activaremos el movimiento
                }*/
            }
        }

        PerseguirJugador();
    }

    void PerseguirJugador()
    {
        // El agente calcula automáticamente la ruta más corta
        agent.SetDestination(player.position);
    }

    public void RecibirDaño(float damage)
    {
        enemigoVidas -= damage;
    }


}
