using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPantallas : MonoBehaviour
{
    [Header("Panel de Controles (solo en PantallaInicio)")]
    public GameObject panelControles;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        if (panelControles != null)
            panelControles.SetActive(false);
    }

    // Ir a la pantalla de historia (cursor visible)
    public void MostrarHistoria()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("PantallaHistoria");
    }

    // Ir a la pantalla de controles (cursor visible)
    public void MostrarControles()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("PantallaControles");
    }

    // Desde la pantalla de historia, continuar al juego
    public void Jugar()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Scene2");
    }

    // Reintentar desde pantallas de Perder/Ganar
    public void JugarDeNuevo()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Scene2");
    }

    // Volver al menú principal desde cualquier pantalla
    public void VolverAlMenu()
    {
        // Si hay un panel de controles abierto, cerrarlo primero
        if (panelControles != null && panelControles.activeSelf)
        {
            panelControles.SetActive(false);
            return;
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("PantallaInicio");
    }

    // Salir del juego
    public void Salir()
    {
        Application.Quit();
    }
}