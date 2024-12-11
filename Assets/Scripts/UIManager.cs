using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameControlsPanel;
    [SerializeField] private GameObject deathScreenPanel;


    public void ShowGameControls()
    {
        HideAllPanels();
        gameControlsPanel.SetActive(true);
    }

    public void ShowDeathScreen()
    {
        HideAllPanels();
        deathScreenPanel.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        SceneManager.LoadScene("VistaFin");
    }

    private void HideAllPanels()
    {
        gameControlsPanel.SetActive(false);
        deathScreenPanel.SetActive(false);
    }
}
