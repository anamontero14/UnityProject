using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI monedas;

    private int cantidadMonedas = 0;
    private bool aviso = false;
    private bool cercaDelCofre = false;

    void Start()
    {
        monedas.text = "Souls collected: " + cantidadMonedas;
    }

    void Update()
    {
        // Agacharse
        transform.localScale = new Vector3(1f, (Keyboard.current.fKey.isPressed) ? 0.5f : 1f, 1f);

        // Depositar en cofre al pulsar E (solo si estás cerca)
        if (cercaDelCofre && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            DepositarAlmas();
        }

        if (cantidadMonedas == 10 && !aviso)
        {
            Debug.Log("Tienes que depositar las almas en el cofre");
            aviso = true;
        }
    }

    // Llamado desde MonedasController cuando el jugador toca un alma
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

    void DepositarAlmas()
    {
        if (cantidadMonedas == 10)
        {
            cantidadMonedas = 0;
            actualizarTexto();
            aviso = false;
            Debug.Log("¡Almas depositadas en el cofre!");
        }
        else
        {
            Debug.Log($"Necesitas 10 almas. Tienes: {cantidadMonedas}");
        }
    }

    void actualizarTexto()
    {
        if (monedas != null)
            monedas.text = "Souls collected: " + cantidadMonedas;
    }

    // Detectar si el jugador entra/sale del área del cofre
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cofre"))
        {
            cercaDelCofre = true;
            Debug.Log("Cerca del cofre. Pulsa E para depositar.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cofre"))
        {
            cercaDelCofre = false;
        }
    }
}