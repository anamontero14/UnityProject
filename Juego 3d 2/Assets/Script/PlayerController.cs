using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI sprintEstado;
    public CharacterController controller;
    public float speed = 5f;
    public float sprintMultiplier = 2f;
    public float gravity = -9.81f;
    private Vector2 moveInput;
    private Vector3 velocity;

    private Animator anim;
    public float maxHP = 100f;
    private float currentHP;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
    }

    void Update()
    {
        if (isDead) return;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Keyboard.current != null)
        {
            float x = 0;
            float z = 0;

            if (Keyboard.current.wKey.isPressed) z = 1;
            if (Keyboard.current.sKey.isPressed) z = -1;
            if (Keyboard.current.aKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed) x = 1;

            moveInput = new Vector2(x, z);

            if (Keyboard.current.spaceKey.isPressed && controller.isGrounded)
                velocity.y = 5f;
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        velocity.y += gravity * Time.deltaTime;

        float currentSpeed = speed;
        if (Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = speed * sprintMultiplier;
        }

        Vector3 finalMovement = (move * currentSpeed) + (Vector3.up * velocity.y);
        controller.Move(finalMovement * Time.deltaTime);

        ActualizarShift(currentSpeed);

        bool isMoving = moveInput.magnitude > 0;
        if (anim != null)
        {
            anim.SetBool("Run", isMoving);
        }

        if (currentHP <= 0 && !isDead)
        {
            MorirPorAlmas();
        }
    }

    void ActualizarShift(float currentSpeed)
    {
        sprintEstado.text = currentSpeed > speed ? "Sprint Shift: ON" : "Sprint Shift: OFF";
    }

    public void RecibirDaño(float dmg)
    {
        if (isDead) return;

        currentHP -= dmg;
        if (anim != null) anim.SetTrigger("Hit");

        if (currentHP <= 0f)
        {
            MorirPorAlmas();
        }
    }

    public void MorirPorAlmas()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null)
        {
            anim.SetBool("Dead", true);
        }

        if (controller != null)
            controller.enabled = false;

        this.enabled = false;
        SceneManager.LoadScene("PantallaPerder");
    }

    public void TriggerHitAnimation()
    {
        if (anim != null && !isDead)
        {
            anim.SetTrigger("Hit");
        }
    }
}