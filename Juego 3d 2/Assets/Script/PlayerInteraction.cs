using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI monedas;
    public TextMeshProUGUI mensajeInteraccion;

    private int cantidadMonedas = 0;
    private bool cercaDelCofre = false;
    private CofreController cofreActual = null;

    void Start()
    {
        if (monedas != null)
            monedas.text = "Souls collected: " + cantidadMonedas;

        if (mensajeInteraccion != null)
            mensajeInteraccion.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.localScale = new Vector3(1f, (Keyboard.current != null && Keyboard.current.fKey.isPressed) ? 0.5f : 1f, 1f);

        if (cercaDelCofre && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AbrirCofre();
        }
    }

    public void DoInteraction(GameObject go)
    {
        if (go.CompareTag("Alma"))
        {
            cantidadMonedas++;
            actualizarTexto();

            MonedasController mc = go.GetComponent<MonedasController>();
            if (mc != null)
            {
                mc.DoInteraction();
            }
        }
    }

    void AbrirCofre()
    {
        if (cofreActual != null)
        {
            cofreActual.AbrirPanelCofre(this);
        }
    }

    public int ObtenerCantidadMonedas()
    {
        return cantidadMonedas;
    }

    public void VaciarMonedas()
    {
        cantidadMonedas = 0;
        actualizarTexto();
    }

    void actualizarTexto()
    {
        if (monedas != null)
            monedas.text = "Souls collected: " + cantidadMonedas;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cofre"))
        {
            cercaDelCofre = true;
            cofreActual = other.GetComponent<CofreController>();

            if (mensajeInteraccion != null)
            {
                mensajeInteraccion.gameObject.SetActive(true);
                mensajeInteraccion.text = "Presiona [E] para abrir el cofre";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cofre"))
        {
            cercaDelCofre = false;
            cofreActual = null;

            if (mensajeInteraccion != null)
            {
                mensajeInteraccion.gameObject.SetActive(false);
            }
        }
    }

    public void PerderAlma()
    {
        if (cantidadMonedas > 0)
        {
            cantidadMonedas--;
            actualizarTexto();

            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TriggerHitAnimation();
            }
        }
    }
}