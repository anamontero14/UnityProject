using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPausa : MonoBehaviour
{
    public GameObject panelPausa;
    private bool estaPausado = false;

    void Start()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (estaPausado)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        estaPausado = true;
        if (panelPausa != null)
            panelPausa.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Reanudar()
    {
        estaPausado = false;
        if (panelPausa != null)
            panelPausa.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("PantallaInicio");
    }
}