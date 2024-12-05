using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public Collider attackCollider; // Collider del golpe
    public int maxHealth = 100;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float dodgeChance = 0.3f;

    private int currentHealth;
    private bool isDead = false;
    private bool isAttacking = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (attackCollider != null) attackCollider.enabled = false; // Collider desactivado al inicio
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > attackRange && !isAttacking)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                SetTrigger("correr"); // No necesita reset
            }
            else if (distanceToPlayer <= attackRange && !isAttacking)
            {
                agent.isStopped = true;
                transform.LookAt(player);
                SetTrigger("atacar");
                isAttacking = true;
                EnableAttackCollider();
                Invoke("DisableAttackCollider", 0.5f);
                Invoke("ResetAttack", 1.5f);
            }
        }
        else
        {
            agent.isStopped = true;
            SetTrigger("reset"); // Este reset es válido porque detiene cualquier acción.
        }
    }



    public void TakeDamage(int damage)
    {
        if (isDead) return;

        if (Random.value <= dodgeChance)
        {
            SetTrigger("esquivar");
            return;
        }

        currentHealth -= damage;
        Debug.Log("El enemigo ha recibido " + damage + " de daño. Salud restante: " + currentHealth);

        if (currentHealth > 0)
        {
            SetTrigger("danio");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        agent.enabled = false;

        SetTrigger("muerte");
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject, 3f);
    }


    private void ResetAttack()
    {
        isAttacking = false;
    }

    private void SetTrigger(string trigger)
    {
        ResetAllTriggers();
        animator.SetTrigger(trigger);
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger("reset");
        animator.ResetTrigger("correr");
        animator.ResetTrigger("atacar");
        animator.ResetTrigger("esquivar");
        animator.ResetTrigger("danio");
        animator.ResetTrigger("muerte");
    }

    // Métodos para activar/desactivar el collider durante el ataque
    public void EnableAttackCollider()
    {
        if (attackCollider != null)
            attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        if (attackCollider != null)
            attackCollider.enabled = false;
    }
}
