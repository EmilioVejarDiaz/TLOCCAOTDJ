using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public int health = 100;
    public float dodgeChance = 0.3f;

    private bool isDead = false;
    private bool isAttacking = false;

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Perseguir al jugador si está fuera del rango de ataque
            if (distanceToPlayer > attackRange && !isAttacking)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                SetTrigger("correr"); // Activar animación de correr
            }
            // Atacar al jugador si está en rango
            else if (distanceToPlayer <= attackRange && !isAttacking)
            {
                agent.isStopped = true;
                transform.LookAt(player);
                SetTrigger("atacar");
                isAttacking = true;
                Invoke("ResetAttack", 1.5f); // Delay antes de permitir otro ataque
            }
        }
        else
        {
            // Si el jugador está fuera del rango de detección
            if (distanceToPlayer <= detectionRange)
            {
                // Si el jugador aún está dentro del rango de detección, el enemigo persigue
                agent.isStopped = false;
                SetTrigger("correr"); // Activar animación de correr
                agent.SetDestination(player.position); // Seguir al jugador
            }
            else
            {
                // Si el jugador se aleja mucho, detener al enemigo y ponerlo en estado de espera
                agent.isStopped = true;
                SetTrigger("reset"); // Activar animación de esperar
            }
        }
    }

    // Recibir daño
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        // Intentar esquivar
        if (Random.value <= dodgeChance)
        {
            SetTrigger("esquivar");
            return;
        }

        health -= damage;

        if (health > 0)
        {
            // Reaccionar al daño
            SetTrigger(isAttacking ? "danio" : "reaccion");
        }
        else
        {
            // Morir
            Die();
        }
    }

    // Morir
    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        SetTrigger("muerte");
        Destroy(gameObject, 3f); // Destruir después de la animación
    }

    // Reseteo del ataque
    private void ResetAttack()
    {
        isAttacking = false;

        // Verificar si el enemigo sigue cerca del jugador y reanudar la persecución
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            // Si el jugador sigue en rango de ataque, volver a atacar
            SetTrigger("atacar");
            isAttacking = true;
            Invoke("ResetAttack", 1.5f); // Delay para el siguiente ataque
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Si el jugador sigue dentro del rango de detección, continuar corriendo
            SetTrigger("correr");
            agent.SetDestination(player.position);
        }
        else
        {
            // Si el jugador se aleja demasiado, volver a esperar
            SetTrigger("reset");
            agent.isStopped = true;
        }
    }

    // Activar un trigger en el Animator
    private void SetTrigger(string trigger)
    {
        ResetAllTriggers();
        animator.SetTrigger(trigger);
    }

    // Resetear todos los triggers
    private void ResetAllTriggers()
    {
        animator.ResetTrigger("reset");
        animator.ResetTrigger("correr");
        animator.ResetTrigger("atacar");
        animator.ResetTrigger("esquivar");
        animator.ResetTrigger("reaccion");
        animator.ResetTrigger("danio");
        animator.ResetTrigger("muerte");
    }
}
