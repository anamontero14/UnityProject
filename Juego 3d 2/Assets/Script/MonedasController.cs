using System;
using TMPro;
using UnityEngine;

public class MonedasController : MonoBehaviour
{
    private Renderer monedasRenderer;
    public CapsuleCollider colliderMoneda;
    private float cooldownMonedas = 5f;
    private Boolean isCollected = false;

    void Start()
    {
        monedasRenderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //contador para el cooldown de monedas
        if (isCollected) {
            cooldownMonedas -= Time.deltaTime;
            if (cooldownMonedas <= 0f) {
                isCollected = false;
                cooldownMonedas = 5f;
                monedasRenderer.enabled = true;
                colliderMoneda.enabled = true;
            }
        }
    }

    public void DoInteraction()
    {
        isCollected = true;
        monedasRenderer.enabled = false;
        colliderMoneda.enabled = false;
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
