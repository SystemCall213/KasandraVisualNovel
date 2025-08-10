using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameBackToMenu : MonoBehaviour
{
    [SerializeField] private Button backToMenu;

    private void Awake()
    {
        backToMenu.onClick.AddListener(backToMenuClicked);
    }

    public void backToMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
