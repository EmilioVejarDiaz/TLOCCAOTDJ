using UnityEngine;

public class BossZone : MonoBehaviour
{
    public BossController bossController; // Referencia al controlador del Boss
    public DoorController doorController; // Referencia al controlador de la puerta

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llama al Boss para que se active
            bossController.SetTrigger("detectar");

            // Bloquea la puerta para evitar que el jugador salga
            if (doorController != null)
            {
                doorController.LockDoor();
            }
        }
    }
}
