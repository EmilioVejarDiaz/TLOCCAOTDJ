using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float detectionRange = 20f;
    public float attackRange = 3f;
    public float specialAttackChance = 0.2f;
    public int health = 500;

    private NavMeshAgent agent;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetTrigger("baile");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                SetTrigger("correr");
            }
            else if (!isAttacking)
            {
                agent.isStopped = true;
                isAttacking = true;

                if (Random.value < specialAttackChance)
                {
                    SetTrigger(Random.value < 0.5f ? "swiping" : "jump_attack");
                }
                else
                {
                    SetTrigger("punch");
                }

                Invoke("ResetAttack", 2f);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            SetTrigger("reaction");
        }
    }

    private void Die()
    {
        animator.SetTrigger("muerte");
        Destroy(gameObject, 3f);
    }

    public void SetTrigger(string trigger)
    {
        ResetAllTriggers();
        animator.SetTrigger(trigger);
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger("baile");
        animator.ResetTrigger("detectar");
        animator.ResetTrigger("correr");
        animator.ResetTrigger("punch");
        animator.ResetTrigger("swiping");
        animator.ResetTrigger("jump_attack");
        animator.ResetTrigger("block");
        animator.ResetTrigger("reaction");
        animator.ResetTrigger("muerte");
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}
