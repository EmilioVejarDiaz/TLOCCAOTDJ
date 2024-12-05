using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 10; // Da�o que inflige
    public string targetTag; // Etiqueta del objetivo (Player o Enemy)

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto colisionado tiene el tag correcto
        if (other.CompareTag(targetTag))
        {
            if (targetTag == "Player")
            {
                // Infligir da�o al jugador
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damageAmount);
                }
            }
            else if (targetTag == "Enemy")
            {
                // Infligir da�o al enemigo
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
        }
    }
}
