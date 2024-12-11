using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameControlsPanel;
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private GameObject victoryScreenPanel;

    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

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
        HideAllPanels();
        victoryScreenPanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        gameControlsPanel.SetActive(false);
        deathScreenPanel.SetActive(false);
        victoryScreenPanel.SetActive(false);
    }
}
