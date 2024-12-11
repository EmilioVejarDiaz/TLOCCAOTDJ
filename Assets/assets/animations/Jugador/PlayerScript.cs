using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isBlocking = false;
    private int currentHealth = 300;

    [SerializeField] private float blockDuration = 2f;
    [SerializeField] private Collider rightHandCollider;
    [SerializeField] private AudioSource audioSource; // AudioSource del jugador
    [SerializeField] private AudioClip golpeSound; // Clip de sonido del golpe
    private CharacterController characterController;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        if (rightHandCollider != null)
        {
            rightHandCollider.enabled = false;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource no asignado en el PlayerController");
        }
    }

    void Update()
    {
        if (animator.GetBool("InAction") || isBlocking) return;

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("InAction", true);
            animator.SetTrigger("golpeDerecho");

            PlayGolpeSound(); // Reproducir sonido del golpe
            Invoke("ResetInAction", 0.5f);
        }

        if (Input.GetMouseButton(1) && !isBlocking)
        {
            isBlocking = true;
            animator.SetBool("isBlocking", true);

            DisableMovement();

            Invoke("StopBlocking", blockDuration);
        }
    }

    private void PlayGolpeSound()
    {
        if (audioSource != null && golpeSound != null)
        {
            audioSource.PlayOneShot(golpeSound);
        }
    }

    private void StopBlocking()
    {
        isBlocking = false;
        animator.SetBool("isBlocking", false);

        EnableMovement();
    }

    private void DisableMovement()
    {
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }

    private void EnableMovement()
    {
        if (characterController != null)
        {
            characterController.enabled = true;
        }

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void ResetInAction()
    {
        animator.SetBool("InAction", false);
    }

    public void TakeDamage(int damage)
    {
        if (isBlocking) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    [SerializeField] private Transform respawnPoint; // Punto de reaparición
    [SerializeField] private GameObject playerModel; // Modelo visible del jugador

    [SerializeField] private GameObject deathScreen; // Pantalla de muerte

    private void TriggerDeath()
    {
        Debug.Log("Jugador ha muerto");
        deathScreen.SetActive(true); // Mostrar la pantalla de muerte
    }


    private void Respawn()
    {
        transform.position = respawnPoint.position; // Mover al punto de reaparición
        playerModel.SetActive(true); // Mostrar al jugador
        currentHealth = 500; // Restablecer la salud del jugador
        Debug.Log("Jugador ha reaparecido");
    }


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
