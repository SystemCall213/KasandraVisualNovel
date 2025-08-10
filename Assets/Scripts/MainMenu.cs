using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    [SerializeField] private Button settings;
    [SerializeField] private Button credits;
    [SerializeField] private Button quit;

    [SerializeField] private SettingsManager settingsManager;

    private string savePath;
    private string flagsPath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        flagsPath = Path.Combine(Application.persistentDataPath, "Flags.json");

        // Make sure buttons trigger the correct method
        newGame.onClick.AddListener(newGameClick);
        loadGame.onClick.AddListener(loadGameClick);
        settings.onClick.AddListener(settingsClick);
        credits.onClick.AddListener(creditsClick);
        quit.onClick.AddListener(quitClick);

        // Disable Load Game button if no save file exists
        loadGame.interactable = File.Exists(savePath);
    }

    public void newGameClick()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);

        if (File.Exists(flagsPath))
            File.Delete(flagsPath);

        SceneManager.LoadScene("SampleScene"); // change later
    }

    public void loadGameClick()
    {
        if (!File.Exists(savePath))
            return;

        // Load saved data
        string json = File.ReadAllText(savePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        // Store save data somewhere accessible for the Game scene
        TempLoadData.Data = saveData;

        // Load Game scene
        SceneManager.LoadScene("SampleScene"); // change later
    }

    public void settingsClick()
    {
        settingsManager.OpenSettings();
    }

    public void creditsClick()
    {
        // TO DO
    }
    
    public void quitClick()
    {
        Application.Quit();
    }
}
