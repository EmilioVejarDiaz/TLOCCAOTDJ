using UnityEngine;

public class BossZone : MonoBehaviour
{
    public BossController bossController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossController.SetTrigger("detectar");
        }
    }
}
