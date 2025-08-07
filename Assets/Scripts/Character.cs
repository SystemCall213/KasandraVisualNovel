using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Image image;
    private string currentSpriteName;

    private void Awake()
    {
        currentSpriteName = string.Empty;
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void setSprite(Sprite sprite)
    {
        gameObject.SetActive(true);
        image.sprite = sprite;
        image.SetNativeSize();
    }

    public void setSpriteName(string name)
    {
        currentSpriteName = name;
    }

    public string getSpriteName()
    {
        return currentSpriteName;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
