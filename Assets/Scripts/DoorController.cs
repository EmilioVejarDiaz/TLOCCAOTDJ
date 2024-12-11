using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Collider doorCollider;       // Referencia al Collider de la puerta
    private MeshRenderer doorRenderer;   // Referencia al Renderer de la puerta

    void Start()
    {
        // Obt�n referencias a los componentes
        doorCollider = GetComponent<Collider>();
        doorRenderer = GetComponent<MeshRenderer>();

        // Aseg�rate de que la puerta est� desactivada al inicio
        if (doorCollider != null) doorCollider.enabled = false;
        if (doorRenderer != null) doorRenderer.enabled = false;
    }

    public void LockDoor()
    {
        // Activa el Collider y el Renderer
        if (doorCollider != null) doorCollider.enabled = true;
        if (doorRenderer != null) doorRenderer.enabled = true;

        Debug.Log("La puerta se ha bloqueado.");
    }
}
