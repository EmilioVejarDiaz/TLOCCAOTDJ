using UnityEngine;

public class BossZone : MonoBehaviour
{
    public BossController bossController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado en la zona del Boss."); // Para confirmar
            bossController.SetTrigger("detectar");
        }
    }
}
