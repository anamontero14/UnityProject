using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    private Vector2 moveInput;
    private Vector3 velocity;
    public float range = 50f; // Alcance del arma
    public float damage = 10f;
    private int EnemyHealth;

    void Update()
    {
        // 1. Resetear gravedad si toca suelo
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // 2. Lectura del Nuevo Input System (Teclado/WASD) y salto
        if (Keyboard.current != null)
        {
            float x = 0;
            float z = 0;

            if (Keyboard.current.wKey.isPressed) z = 1;
            if (Keyboard.current.sKey.isPressed) z = -1;
            if (Keyboard.current.aKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed) x = 1;
            /* Aquí va el código de correr */
            moveInput = new Vector2(x, z);
            /* Aquí va el código del salto */
        }
        // 3. Cálculo de dirección
        // Calculamos la dirección horizontal (WASD)
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Aplicamos la gravedad al valor actual de velocity.y
        velocity.y += gravity * Time.deltaTime;

        // COMBINAMOS TODO: (Movimiento Horizontal * Velocidad) + Dirección Vertical
        Vector3 finalMovement = (move * speed) + (Vector3.up * velocity.y);

        //le decimos al controller que se mueva a esa direccion multiplicado por el time delta time
        controller.Move(finalMovement * Time.deltaTime);

        // Salto continuo
        if (Keyboard.current.spaceKey.isPressed && controller.isGrounded)
            velocity.y = 5f;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Disparar();
        }
    }

    void Disparar()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            // ¿Hemos dado a un enemigo?
            EnemyController enemigo = hit.transform.GetComponent<EnemyController>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(damage);
            }
        }
    }


}