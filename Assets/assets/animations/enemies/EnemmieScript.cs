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
            if (distanceToPlayer > attackRange && !isAttacking)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                SetTrigger("correr");
            }
            else if (distanceToPlayer <= attackRange && !isAttacking)
            {
                agent.isStopped = true;
                transform.LookAt(player);
                SetTrigger("atacar");
                isAttacking = true;
                Invoke("ResetAttack", 1.5f);
            }
        }
        else
        {
            if (distanceToPlayer <= detectionRange)
            {
                agent.isStopped = false;
                SetTrigger("correr");
                agent.SetDestination(player.position);
            }
            else
            {
                agent.isStopped = true;
                SetTrigger("reset");
            }
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

        health -= damage;

        if (health > 0)
        {
            SetTrigger(isAttacking ? "danio" : "reaccion");
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
        SetTrigger("muerte");
        Destroy(gameObject, 3f);
    }

    private void ResetAttack()
    {
        isAttacking = false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            SetTrigger("atacar");
            isAttacking = true;
            Invoke("ResetAttack", 1.5f);
        }
        else if (distanceToPlayer <= detectionRange)
        {
            SetTrigger("correr");
            agent.SetDestination(player.position);
        }
        else
        {
            SetTrigger("reset");
            agent.isStopped = true;
        }
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
        animator.ResetTrigger("reaccion");
        animator.ResetTrigger("danio");
        animator.ResetTrigger("muerte");
    }
}
