using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public float attackCooldown = 1.5f; // Cooldown para el ataque especial
    public float blockDuration = 0.5f; // Tiempo que tarda en iniciar el bloqueo
    public Collider rightHandCollider; // Asigna el collider de la mano derecha

    private int currentHealth;
    private bool isBlocking = false;
    private bool canSpecialAttack = true;

    void Start()
    {
        currentHealth = maxHealth;

        // Asegurarse de que el collider está desactivado al inicio
        if (rightHandCollider != null) rightHandCollider.enabled = false;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Ataque básico con la mano derecha
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("golpeDerecho");
            EnableHandCollider();
            Invoke("DisableHandCollider", 0.5f); // Desactivar el collider después del golpe
        }

        // Bloqueo
        if (Input.GetMouseButton(1))
        {
            if (!isBlocking)
            {
                isBlocking = true;
                animator.SetBool("isBlocking", true);
                Invoke("StopBlocking", blockDuration);
            }
        }

        // Ataque especial
        if (Input.GetKeyDown(KeyCode.LeftControl) && canSpecialAttack)
        {
            canSpecialAttack = false;
            animator.SetTrigger("patada");
            EnableHandCollider();
            Invoke("DisableHandCollider", 0.5f); // Desactivar el collider después del golpe especial
            Invoke("ResetSpecialAttack", attackCooldown);
        }
    }

    private void StopBlocking()
    {
        isBlocking = false;
        animator.SetBool("isBlocking", false);
    }

    private void ResetSpecialAttack()
    {
        canSpecialAttack = true;
    }

    public void TakeDamage(int damage)
    {
        if (isBlocking)
        {
            Debug.Log("Ataque bloqueado!");
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetTrigger("muerte");
            Debug.Log("Jugador ha muerto");
            // Detener movimientos o añadir lógica adicional aquí
        }
    }

    // Métodos para activar/desactivar el collider
    public void EnableHandCollider()
    {
        if (rightHandCollider != null)
        {
            rightHandCollider.enabled = true;
            Debug.Log("Collider de la mano activado.");
        }
    }

    public void DisableHandCollider()
    {
        if (rightHandCollider != null)
        {
            rightHandCollider.enabled = false;
            Debug.Log("Collider de la mano desactivado.");
        }
    }

}
