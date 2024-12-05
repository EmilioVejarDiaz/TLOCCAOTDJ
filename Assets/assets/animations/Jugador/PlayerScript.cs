using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isBlocking = false;
    private int currentHealth = 100;

    [SerializeField] private float blockDuration = 2f; // Duración máxima de bloqueo
    [SerializeField] private Collider rightHandCollider; // Asigna el collider de la mano derecha

    void Start()
    {
        animator = GetComponent<Animator>();

        // Asegurar que el collider de la mano derecha esté desactivado al inicio
        if (rightHandCollider != null)
        {
            rightHandCollider.enabled = false;
        }
    }

    void Update()
    {
        // Bloquear entradas si está en acción
        if (animator.GetBool("InAction")) return;

        HandleInput();
    }

    private void HandleInput()
    {
        // Ataque básico
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("InAction", true);
            animator.SetTrigger("golpeDerecho");
        }

        // Bloqueo
        if (Input.GetMouseButton(1) && !isBlocking)
        {
            isBlocking = true;
            animator.SetBool("isBlocking", true);
            Invoke("StopBlocking", blockDuration); // Salir de bloqueo después de blockDuration
        }
    }

    private void StopBlocking()
    {
        isBlocking = false;
        animator.SetBool("isBlocking", false);
    }

    public void EndAction() // Llamado desde Animation Event al final de cada animación
    {
        animator.SetBool("InAction", false);
    }

    public void TakeDamage(int damage)
    {
        if (isBlocking) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("muerte");
            Debug.Log("Jugador ha muerto");
        }
    }

    // Métodos para habilitar/deshabilitar el collider de la mano derecha
    public void EnableHandCollider() // Llamar desde un Animation Event
    {
        if (rightHandCollider != null)
        {
            rightHandCollider.enabled = true;
            Debug.Log("Collider de la mano activado.");
        }
    }

    public void DisableHandCollider() // Llamar desde un Animation Event
    {
        if (rightHandCollider != null)
        {
            rightHandCollider.enabled = false;
            Debug.Log("Collider de la mano desactivado.");
        }
    }
}
