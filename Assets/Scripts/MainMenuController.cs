using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Primera_Zona_Roma"); // Carga la escena principal
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Salir del juego
    }
}
