using UnityEngine.UI;
using UnityEngine;

public class BackgroundDisplayer : MonoBehaviour
{
    private Image background;
    private string currentBackgroundName;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void setBackground(string backName)
    {
        if (background == null) background = GetComponent<Image>();

        Sprite sprite = Resources.Load<Sprite>($"Backgrounds/{backName}");

        if (!sprite) return;

        background.sprite = sprite;
        currentBackgroundName = backName;
    }

    public void setBackgroundName(string backName)
    {
        currentBackgroundName = backName;
    }

    public string getBackgroundName()
    {
        return currentBackgroundName;
    }
}
