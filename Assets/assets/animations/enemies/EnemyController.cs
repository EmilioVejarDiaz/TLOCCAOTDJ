using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public Collider attackCollider;
    public int maxHealth = 50;
    public float detectionRange = 10f;
    public float attackRange = 2.5f;
    public float attackCooldown = 1.5f;

    private int currentHealth;
    private bool isDead = false;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        if (attackCollider != null) attackCollider.enabled = false;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > attackRange)
            {
                // Persigue al jugador
                agent.isStopped = false;
                agent.SetDestination(player.position);
                SetTrigger("correr");
            }
            else
            {
                // Detenerse y atacar
                agent.isStopped = true;
                transform.LookAt(player);

                if (Time.time >= nextAttackTime)
                {
                    StartCoroutine(AttackPlayer());
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        else
        {
            // Fuera del rango de detección
            agent.isStopped = true;
            SetTrigger("reset");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("El enemigo ha recibido " + damage + " de daño. Salud restante: " + currentHealth);

        if (currentHealth > 0)
        {
            if (isAttacking)
            {
                // Si está atacando, interrumpe con la animación de "daño"
                StopAllCoroutines();
                isAttacking = false;
                DisableAttackCollider();
                SetTrigger("danio");

                // Volver a atacar después de recibir daño
                StartCoroutine(ReturnToAttack());
            }
            else
            {
                SetTrigger("reaccion");
            }
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

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        SetTrigger("atacar");
        EnableAttackCollider();

        yield return new WaitForSeconds(0.5f); // Duración del golpe
        DisableAttackCollider();

        yield return new WaitForSeconds(attackCooldown - 0.5f);
        isAttacking = false;
    }

    private IEnumerator ReturnToAttack()
    {
        yield return new WaitForSeconds(0.5f); // Tiempo de reacción al daño
        if (!isDead && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            SetTrigger("retomar_ataque");
            isAttacking = true;
        }
    }

    private void SetTrigger(string trigger)
    {
        animator.ResetTrigger("reset");
        animator.ResetTrigger("correr");
        animator.ResetTrigger("atacar");
        animator.ResetTrigger("danio");
        animator.ResetTrigger("muerte");
        animator.ResetTrigger("reaccion");
        animator.ResetTrigger("retomar_ataque");

        animator.SetTrigger(trigger);
    }

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
