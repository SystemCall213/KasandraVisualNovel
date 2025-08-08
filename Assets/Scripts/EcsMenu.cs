using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EcsMenu : MonoBehaviour
{
    [SerializeField] private Button backToGame;
    [SerializeField] private Button backToMenu;
    [SerializeField] private Button settings;
    [SerializeField] private Button quit;

    [SerializeField] private GameObject settingsWrapper;
    [SerializeField] private GameObject EcsMenuWrapper;

    [SerializeField] private Dialogue dialogue;

    private void Awake()
    {
        // Make sure buttons trigger the correct method
        backToGame.onClick.AddListener(backToGameClick);
        backToMenu.onClick.AddListener(backToMenuClick);
        settings.onClick.AddListener(settingsClick);
        quit.onClick.AddListener(quitClick);
    }

    public void backToGameClick()
    {
        EcsMenuWrapper.SetActive(false);
    }

    public void backToMenuClick()
    {
        dialogue.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void settingsClick()
    {
        settingsWrapper.SetActive(true);
    }
    
    public void quitClick()
    {
        Application.Quit();
    }
}
