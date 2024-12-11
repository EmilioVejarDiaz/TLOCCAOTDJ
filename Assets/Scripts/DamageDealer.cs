using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 10; // Daño que inflige
    public string[] targetTags; // Lista de etiquetas de los objetivos (Player, Enemy, Boss)

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in targetTags)
        {
            if (other.CompareTag(tag))
            {
                if (tag == "Player")
                {
                    // Infligir daño al jugador
                    PlayerController player = other.GetComponent<PlayerController>();
                    if (player != null)
                    {
                        player.TakeDamage(damageAmount);
                    }
                }
                else if (tag == "Enemy")
                {
                    // Infligir daño al enemigo
                    EnemyController enemy = other.GetComponent<EnemyController>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damageAmount);
                    }
                }
                else if (tag == "Boss")
                {
                    // Infligir daño al jefe
                    BossController boss = other.GetComponent<BossController>();
                    if (boss != null)
                    {
                        boss.TakeDamage(damageAmount);
                    }
                }
                break; // Salir del bucle después de procesar el daño
            }
        }
    }
}
