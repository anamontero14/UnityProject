using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CofreController : MonoBehaviour
{
    private int almasDepositadas = 0;
    public int almasParaGanar = 20;

    public GameObject panelCofre;
    public TextMeshProUGUI textoAlmasDepositadas;
    public TextMeshProUGUI textoAlmasJugador;
    public Slider barraProgreso;
    public Button botonCerrar;
    public Button botonDepositar;

    void Start()
    {
        if (panelCofre != null)
            panelCofre.SetActive(false);

        if (botonCerrar != null)
            botonCerrar.onClick.AddListener(CerrarPanelCofre);

        if (botonDepositar != null)
            botonDepositar.onClick.AddListener(DepositarDesdePanel);

        ActualizarUI();
    }

    public void AbrirPanelCofre(PlayerInteraction jugador)
    {
        if (panelCofre != null)
        {
            panelCofre.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ActualizarUIConJugador(jugador);
        }
    }

    void DepositarDesdePanel()
    {
        PlayerInteraction jugador = FindObjectOfType<PlayerInteraction>();
        if (jugador != null)
        {
            DepositarAlmas(jugador.ObtenerCantidadMonedas());
            jugador.VaciarMonedas();
            ActualizarUIConJugador(jugador);
        }
    }

    public void CerrarPanelCofre()
    {
        if (panelCofre != null)
            panelCofre.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DepositarAlmas(int cantidad)
    {
        almasDepositadas += cantidad;
        ActualizarUI();

        if (almasDepositadas >= almasParaGanar)
        {
            SceneManager.LoadScene("PantallaGanar");
        }
    }

    void ActualizarUI()
    {
        if (textoAlmasDepositadas != null)
            textoAlmasDepositadas.text = $"{almasDepositadas} / {almasParaGanar}";

        if (barraProgreso != null)
        {
            barraProgreso.maxValue = almasParaGanar;
            barraProgreso.value = almasDepositadas;
        }
    }

    void ActualizarUIConJugador(PlayerInteraction jugador)
    {
        ActualizarUI();

        if (textoAlmasJugador != null && jugador != null)
            textoAlmasJugador.text = $"Tienes: {jugador.ObtenerCantidadMonedas()} almas";
    }
}